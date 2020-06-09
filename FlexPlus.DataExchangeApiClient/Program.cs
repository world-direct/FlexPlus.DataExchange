// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using CommandLine;
    using FlexPlus.DataExchangeApiClient.Model;

    /// <summary>
    /// Represents the client for interacting with the FlexPlus DataExchange API.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments that are provided through the command line interface.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Method does not return a value. Similar to a void method.")]
        public static async Task Main(string[] args)
        {
            // Parses arguments from commandline.
            var parser = new Parser(with =>
            {
                with.EnableDashDash = true;
                with.AutoVersion = true;
                with.AutoHelp = true;
                with.HelpWriter = Console.Out;
            });

            var arguments = parser.ParseArguments<UploadArguments, DownloadArguments>(args);

            // Upload files to API.
            await arguments.WithParsedAsync<UploadArguments>(UploadAsync).ConfigureAwait(false);

            // Download files from API.
            await arguments.WithParsedAsync<DownloadArguments>(DownloadAsync).ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads a CSV file from the FlexPlus DataExchange API.
        /// </summary>
        /// <param name="arguments">The <see cref="DownloadArguments"/> that contains all arguments that are needed for downloading a file from the API.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Method does not return a value. Similar to a void method.")]
        public static async Task DownloadAsync(DownloadArguments arguments)
        {
            var process = Enumeration.FromDisplayName<Process>(arguments.ProcessName);
            if (process.Type != Process.ProcessType.Download)
            {
                throw new ArgumentException($"Process {arguments.ProcessName} can not be downloaded.", nameof(process));
            }

            // Configure client to access the API.
            var apiEndpoint = new Uri(arguments.ApiEndpoint);
            using var client = GetClient(apiEndpoint, arguments.Token);

            var response = await client.GetAsync(process.Name, CancellationToken.None).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var filename = response.Content.Headers.ContentDisposition.FileName;

                await using (var fileStream = File.Create(filename))
                await using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    await stream.CopyToAsync(fileStream, CancellationToken.None).ConfigureAwait(false);
                    Console.WriteLine($"File {filename} wrote to folder {Directory.GetCurrentDirectory()}.");
                }

                Console.WriteLine(
                    $"Download file {filename} from API endpoint {new Uri(apiEndpoint, arguments.ProcessName)} was successfully.");
            }
            else
            {
                await PrintErrorAsync(response, new Uri(apiEndpoint, arguments.ProcessName), process.Type, CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Uploads a given file to the FlexPlus DataExchange API.
        /// </summary>
        /// <param name="arguments">The <see cref="UploadArguments"/> that contains all arguments that are needed for uploading the given file to the API.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Method does not return a value. Similar to a void method.")]
        public static async Task UploadAsync(UploadArguments arguments)
        {
            var process = Enumeration.FromDisplayName<Process>(arguments.ProcessName);
            if (process.Type != Process.ProcessType.Upload)
            {
                throw new ArgumentException($"Process {arguments.ProcessName} can not be uploaded.", nameof(process));
            }

            var apiEndpoint = new Uri(arguments.ApiEndpoint);
            await using (var fileStream = File.OpenRead(arguments.Filename))
            using (var client = GetClient(apiEndpoint, arguments.Token))
            using (var streamContent = new StreamContent(fileStream))
            using (var content = new MultipartFormDataContent())
            {
                content.Add(streamContent, "file", arguments.Filename);
                var response = await client.PostAsync(process.Name, content, CancellationToken.None).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Upload of file {arguments.Filename} to API endpoint {new Uri(apiEndpoint, arguments.ProcessName)}");
                }
                else
                {
                    await PrintErrorAsync(response, new Uri(apiEndpoint, arguments.ProcessName), process.Type, CancellationToken.None).ConfigureAwait(false);
                }
            }
        }

        private static HttpClient GetClient(Uri baseAddress, string token)
        {
            return new HttpClient()
            {
                BaseAddress = baseAddress,
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token),
                },
            };
        }

        private static async Task PrintErrorAsync(HttpResponseMessage response, Uri apiEndpoint, Process.ProcessType type, CancellationToken cancellationToken)
        {
            await using var errorWriter = Console.Error;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var jsonStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var jsonOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                var problemDetails = await JsonSerializer
                    .DeserializeAsync<ProblemDetails>(jsonStream, jsonOptions, cancellationToken)
                    .ConfigureAwait(false);

                await errorWriter.WriteLineAsync($"Title: {problemDetails.Title}").ConfigureAwait(false);
                await errorWriter.WriteLineAsync($"Status: {problemDetails.Status} ({(int)problemDetails.Status})").ConfigureAwait(false);
                await errorWriter.WriteLineAsync($"Detail: {problemDetails.Detail}").ConfigureAwait(false);

                return;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                await errorWriter.WriteLineAsync($"[{(int)response.StatusCode}]The given endpoint {apiEndpoint} was not found.").ConfigureAwait(false);
                return;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await errorWriter.WriteLineAsync($"[{(int)response.StatusCode}]You are not authenticated for endpoint {apiEndpoint}.").ConfigureAwait(false);
                return;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                if (type == Process.ProcessType.Download)
                {
                    await errorWriter.WriteLineAsync($"[{(int)response.StatusCode}]You are not allowed to download file from {apiEndpoint}.").ConfigureAwait(false);
                }

                if (type == Process.ProcessType.Upload)
                {
                    await errorWriter.WriteLineAsync($"[{(int)response.StatusCode}]You are not allowed to upload file from {apiEndpoint}.").ConfigureAwait(false);
                }

                return;
            }

            throw new ArgumentOutOfRangeException(nameof(response.StatusCode), $"[{(int)response.StatusCode}]Message: {response.ReasonPhrase}\nEndpoint: {apiEndpoint}.");
        }
    }
}

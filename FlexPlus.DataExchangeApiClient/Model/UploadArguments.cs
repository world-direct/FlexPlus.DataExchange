// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient.Model
{
    using CommandLine;

    /// <summary>
    /// Represents the available commandline arguments for uploading a file to the API.
    /// </summary>
    [Verb("upload", HelpText = "Uploads a file to the api.")]
    public class UploadArguments
    {
        private string processName;

        /// <summary>
        /// Gets or sets the name of process.
        /// </summary>
        /// <value>
        /// The name of process.
        /// </value>
        [Option('p', "process", HelpText = "The name of the process to calling.", Required = true)]
        public string ProcessName {
            get => this.processName.ToUpperInvariant();
            set => this.processName = value;
        }

        /// <summary>
        /// Gets or sets the name of the uploading file.
        /// </summary>
        /// <value>
        /// The name of the uploading file.
        /// </value>
        [Option('f', "file", HelpText = "The name of the file you want to upload.", Required = true)]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the token that authenticates you at the API.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Option('t', "token", HelpText = "The token to authenticate yourself.", Required = true)]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        [Option('e', "endpoint", HelpText = "The endpoint of the FlexPlus DataExchange API.", Required = true)]
        public string ApiEndpoint { get; set; }
    }
}

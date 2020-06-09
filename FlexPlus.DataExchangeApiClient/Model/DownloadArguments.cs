// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient.Model
{
    using CommandLine;

    /// <summary>
    /// Represents the available commandline arguments for downloading a file from the API.
    /// </summary>
    [Verb("download", HelpText = "Downloads af file from the api.")]
    public class DownloadArguments
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

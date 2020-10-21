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

        /// <summary>
        /// Gets or sets the value for the If-Modified-Since-Header.
        /// </summary>
        /// <value>
        /// The modified since.
        /// </value>
        [Option("modified-since", HelpText = "Sets the timestamp for the If-Modified-Since-Header. Only available for process PV004 and PE004.")]
        public string ModifiedSince { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the If-Modified-Since-Header should be send or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [set if modified since header]; otherwise, <c>false</c>.
        /// </value>
        [Option("send-if-modified-header", Default = false, HelpText = "If set to true, a timestamp for the If-Modified-Since-Header must be set through option --modified-since <value>. Only available for process PV004 and PE004.")]
        public bool SendIfModifiedSinceHeader { get; set; }
    }
}

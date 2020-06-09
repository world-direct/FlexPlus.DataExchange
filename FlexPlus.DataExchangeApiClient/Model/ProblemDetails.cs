// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient.Model
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents the response of the FlexPlus API, if an error occurs.
    /// </summary>
    public class ProblemDetails
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="HttpStatusCode"/> that has been returned.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the detail. It contains more information about the error.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public string Instance { get; set; }

        /// <summary>
        /// Gets or sets the additional prop1.
        /// </summary>
        /// <value>
        /// The additional prop1.
        /// </value>
        public IEnumerable<string> AdditionalProp1 { get; set; }

        /// <summary>
        /// Gets or sets the additional prop2.
        /// </summary>
        /// <value>
        /// The additional prop2.
        /// </value>
        public IEnumerable<string> AdditionalProp2 { get; set; }

        /// <summary>
        /// Gets or sets the additional prop3.
        /// </summary>
        /// <value>
        /// The additional prop3.
        /// </value>
        public IEnumerable<string> AdditionalProp3 { get; set; }
    }
}

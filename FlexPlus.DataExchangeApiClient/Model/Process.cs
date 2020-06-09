// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient.Model
{

    /// <summary>
    /// Represents the available processes of the FlexPlus DataExchange API as <see cref="Enumeration"/>.
    /// </summary>
    /// <seealso cref="Enumeration" />
    public class Process : Enumeration
    {
        /// <summary>
        /// The PV001 process.
        /// </summary>
        public static readonly Process Pv001 = new Process(1, "PV001", ProcessType.Upload);

        /// <summary>
        /// The PV002 process.
        /// </summary>
        public static readonly Process Pv002 = new Process(2, "PV002", ProcessType.Upload);

        /// <summary>
        /// The PV003 process.
        /// </summary>
        public static readonly Process Pv003 = new Process(3, "PV003", ProcessType.Download);

        /// <summary>
        /// The PV004 process.
        /// </summary>
        public static readonly Process Pv004 = new Process(4, "PV004", ProcessType.Download);

        /// <summary>
        /// The PV011 process.
        /// </summary>
        public static readonly Process Pv011 = new Process(6, "PV011", ProcessType.Upload);

        /// <summary>
        /// The PV012 process.
        /// </summary>
        public static readonly Process Pv012 = new Process(7, "PV012", ProcessType.Download);

        /// <summary>
        /// The PE003 process.
        /// </summary>
        public static readonly Process Pe003 = new Process(8, "PE003", ProcessType.Upload);

        /// <summary>
        /// The PE004 process.
        /// </summary>
        public static readonly Process Pe004 = new Process(9, "PE004", ProcessType.Download);

        /// <summary>
        /// The PE005 process.
        /// </summary>
        public static readonly Process Pe005 = new Process(10, "PE005", ProcessType.Upload);

        /// <summary>
        /// The PE006 process.
        /// </summary>
        public static readonly Process Pe006 = new Process(11, "PE006", ProcessType.Download);

        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public Process(int id, string name, ProcessType type)
            : base(id, name)
        {
            this.Type = type;
        }

        /// <summary>
        /// Represents the available types that a process can have.
        /// </summary>
        public enum ProcessType
        {
            /// <summary>
            /// The process can upload a file to the FlexPlus API.
            /// </summary>
            Upload,

            /// <summary>
            /// The process can download a file from the FlexPlus API.
            /// </summary>
            Download,
        }

        /// <summary>
        /// Gets the possible types that a process can have.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ProcessType Type { get; }
    }
}

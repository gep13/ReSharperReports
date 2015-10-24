using System;

namespace ReSharperReports
{
    /// <summary>
    /// A simple logging class for returning information to both command line and file
    /// </summary>
    public static class Logger
    {
        static Logger()
        {
            Reset();
        }

        /// <summary>
        /// Gets or sets the action to be performed when writing information log messages
        /// </summary>
        public static Action<string> WriteInfo { get; set; }

        /// <summary>
        /// Gets or sets the action to be performed when writing warning log messages
        /// </summary>
        public static Action<string> WriteWarning { get; set; }

        /// <summary>
        /// Gets or sets the action to be performed when writing error log messages
        /// </summary>
        public static Action<string> WriteError { get; set; }

        private static void Reset()
        {
            WriteInfo = s => { throw new Exception("Logger not defined."); };
            WriteWarning = s => { throw new Exception("Logger not defined."); };
            WriteError = s => { throw new Exception("Logger not defined."); };
        }
    }
}
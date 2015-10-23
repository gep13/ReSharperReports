﻿using System;

namespace ReSharperReports
{
    public static class Logger
    {
        static Logger()
        {
            Reset();
        }

        public static Action<string> WriteInfo { get; set; }

        public static Action<string> WriteWarning { get; set; }

        public static Action<string> WriteError { get; set; }

        private static void Reset()
        {
            WriteInfo = s => { throw new Exception("Logger not defined."); };
            WriteWarning = s => { throw new Exception("Logger not defined."); };
            WriteError = s => { throw new Exception("Logger not defined."); };
        }
    }
}
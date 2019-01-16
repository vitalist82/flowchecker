using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    public enum LogLevel
    {
        Error,
        Warning,
        Info,
        Debug
    }

    public delegate void LogDelegate(string message);

    public class Logger
    {
        private static Logger logger;

        private LogLevel logLevel;
        private LogDelegate log;

        private Logger(LogLevel logLevel, LogDelegate logDelegate)
        {
            this.logLevel = logLevel;
            log = logDelegate;
        }

        public static void Init(LogDelegate logDelegate, LogLevel logLevel = LogLevel.Info)
        {
            if (logger == null)
                logger = new Logger(logLevel, logDelegate);
        }

        public static void Log(LogLevel logLevel, string message)
        {
            if (logLevel <= logger.logLevel)
                logger.log.Invoke(message);
        }
    }
}

using System;
using log4net;

namespace Utils.Profiler
{
    public class Profiler : Profiler<Profiler> { }

    public class Profiler<TLoggerName>
    {
        static volatile int errorCount;
        static volatile int warnCount;

        public static int ErrorCount { get { return errorCount; } }
        public static int WarnCount { get { return warnCount; } }

        private static readonly ILog log = LogManager.GetLogger(typeof(TLoggerName).Name);

        class MyProfilerLog : IProfilerLog
        {
            private readonly int level;
            public MyProfilerLog(int level)
            {
                this.level = level;
            }
            
            public bool Enabled() 
            { 
                if (ColorConsole.Enabled)
                    return true;

                switch(level)
                {
                    case 0: return log.IsFatalEnabled;
                    case 1: return log.IsErrorEnabled;
                    case 2: return log.IsWarnEnabled;
                    case 3: return log.IsInfoEnabled;
                    case 4: return log.IsDebugEnabled;
                }
                return false; 
            }

            public void Write(string msg) 
            {
                // Do not log to console, as control-M sees this and is not allowed to receive confidential data. Only log to file.
                if (ColorConsole.Enabled)
                    Console.WriteLine(msg); 
                
                switch (level)
                {
                    case 0: log.Fatal(msg); break;
                    case 1: log.Error(msg); break;
                    case 2: log.Warn(msg);  break;
                    case 3: log.Info(msg);  break;
                    case 4: log.Debug(msg); break;
                }
            }
        }

        static readonly IProfilerLog m_logFatal = new MyProfilerLog(0);
        static readonly IProfilerLog m_logError = new MyProfilerLog(1);
        static readonly IProfilerLog m_logWarn  = new MyProfilerLog(2);
        static readonly IProfilerLog m_logTrace = new MyProfilerLog(3);
        static readonly IProfilerLog m_logDebug = new MyProfilerLog(4);

        public static bool IsCheckPointProfilerEnabled()
        {
            return m_logTrace.Enabled();
        }

        public static IDisposable CheckPoint(string name, params object[] args)
        {
            return SimpleProfiler.CheckPoint(m_logTrace, name, args);
        }

        public static void Trace(string format, params object[] args)
        {
            IProfilerLog log = m_logTrace;
            if (!log.Enabled()) return;
            using (ColorConsole.UseColors(ConsoleColor.Green, ConsoleColor.Gray))
            {
                SimpleProfiler.Trace("INF", log, 0, " " + format, args);
            }
        }

        public static void Error(string format, params object[] args)
        {
            IProfilerLog log = m_logError;
            if (!log.Enabled()) return;
            using (ColorConsole.UseColors(ConsoleColor.Red, ConsoleColor.Gray))
            {
                errorCount++;
                SimpleProfiler.Trace(">>ERR", log, 0, format, args);
            }
        }

        public static void Fatal(string format, params object[] args)
        {
            IProfilerLog log = m_logFatal;
            if (!log.Enabled()) return;
            using (ColorConsole.UseColors(ConsoleColor.Red, ConsoleColor.Gray))
            {
                errorCount++;
                SimpleProfiler.Trace(">>ERR", log, 0, format, args);
            }
        }

        public static void Debug(string format, params object[] args)
        {
            IProfilerLog log = m_logDebug;
            if (!log.Enabled()) return;
            using (ColorConsole.UseColors(ConsoleColor.Blue, ConsoleColor.Gray))
            {
                SimpleProfiler.Trace("DBG", log, 0, " " + format, args);
            }
        }

        public static void Warn(string format, params object[] args)
        {
            IProfilerLog log = m_logWarn;
            if (!log.Enabled()) return;
            using (ColorConsole.UseColors(ConsoleColor.Yellow, ConsoleColor.Gray))
            {
                warnCount++;
                SimpleProfiler.Trace("WRN", log, 0, " " + format, args);
            }
        }
    }
}

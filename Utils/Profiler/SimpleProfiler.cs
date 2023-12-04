#define PROFILER
#define DRAW_TREE

using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace Utils.Profiler
{
    public interface IProfilerLog
    {
        bool Enabled();
        void Write(string message);
    }
    
    internal class DummyProfiler : IDisposable
    {
        public static IDisposable Instance = new DummyProfiler();
        private DummyProfiler() { }
        void IDisposable.Dispose() { }
    }

#if !PROFILER
    internal class SimpleProfiler
    {
        public static IDisposable Function(IProfilerLog log, params object[] args)
        {
            // use single instance of dummy profiler to avoid unnecessary
            // calls to multithreaded new operator.
            return DummyProfiler.Instance;
        }

        public static IDisposable CheckPoint(IProfilerLog log, string name, params object[] args)
        {
            return DummyProfiler.Instance;
        }

        internal static void Trace(string prefix, IProfilerLog log, long time, string format, params object[] args)
        {
        }
    }
#else

    internal class SimpleProfiler 
	{
		public static IDisposable CheckPoint(IProfilerLog log, string name, params object[] args)
		{
			if(!log.Enabled())
				return DummyProfiler.Instance;
			
			return new CheckPointProfiler(log, name, args);
		}

		const int INDENT_DX = 3;
		[ThreadStatic] static int m_indent = 0;				

		internal static void Indent()
		{
			m_indent++;	
		}

		internal static void Unindent()
		{
			m_indent--;	
		}

        private static long m_previousMem;
		
		internal static void Trace(string prefix, IProfilerLog log, long time, string format, params object[] args)
		{
			if(!log.Enabled())
				return;

            long now = TimerUtils.Now();
			
			int indent = m_indent;
			
			long gcMem = GC.GetTotalMemory(false)/1024;

		    long prevMem = Interlocked.Exchange(ref m_previousMem, gcMem);

		    bool gcOccurred = gcMem < prevMem;
			
			if(prefix==null)
				prefix = "PRF";
			StringBuilder sb = new StringBuilder();
		    sb.AppendFormat(CultureInfo.InvariantCulture, "{0,-5} {1,10:#,##0} {2,-2} {3,10:#,##0.00} ms {4,2} "
		        , prefix
		        , gcMem
                , gcOccurred ? "K@" : "K"
		        , ((float)time)/1000
		        , indent);
            
#if DRAW_TREE
			
            //char[] indentChars = new char[16];
            //for(int i = 0; i<indentChars.Length; i++)
            //{
            //    if(i < indent)
            //        sb.Append("o");
            //    else	
            //        sb.Append(' ');
            //}
			int maxdepth = indent > 16 ? 16 : indent; 
            sb.Append(' ', 2*maxdepth);
#endif			

            if(args.Length > 0)
				sb.AppendFormat(format, args);	
			else
				sb.Append(format);	

			log.Write(sb.ToString());	
		}
	}
	
	class CheckPointProfiler : IDisposable
	{
		readonly IProfilerLog m_log;
		string m_traceString;
		long m_time;

		public CheckPointProfiler(IProfilerLog log, string name, params object[] args)
		{
			m_log = log;
			Enter(name, args);
		}

		void IDisposable.Dispose()
		{
			Exit();
		}
		
		[Conditional("PROFILER")]
		void Enter(string name, params object[] args)
		{
			// check if profiler is disabled via log4net config file
			if(!m_log.Enabled())
				return;

            m_time = TimerUtils.Now();
			
			StringBuilder argList = new StringBuilder(); 
			for(int i=0; i<args.Length; i++)
			{
				if(i > 0)
					argList.Append(", ");
				argList.Append(args[i]);
			}
			
			m_traceString = string.Format("{0}({1})", name, argList);

			SimpleProfiler.Trace(null, m_log, 0, "+{0}()", name);
			SimpleProfiler.Indent();
		}

		[Conditional("PROFILER")]
		void Exit()
		{
			// check if profiler is disabled via log4net config file
			if(!m_log.Enabled())
				return;

            long time = TimerUtils.GetDeltaMks(TimerUtils.Now(), m_time);

			SimpleProfiler.Unindent();
			SimpleProfiler.Trace(null, m_log, time, "-{0}", m_traceString);
		}
	}
#endif
} // namespace
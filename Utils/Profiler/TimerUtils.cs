using System;

namespace Utils.Profiler
{
    public static class TimerUtils
    {
        public static long Now()
        {
            return DateTime.UtcNow.Ticks;
        }

        public static long GetDeltaMks(long t1, long t2)
        {
            long d = t1 - t2;
            d = d / 10;
            return d;
        }
    }
}

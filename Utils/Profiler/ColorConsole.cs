using System;

namespace Utils.Profiler
{
    /// Static class for console colour manipulation.
    public class ColorConsole
    {
        public static bool Enabled { get; set; }

        public static void SetForeGroundColor(ConsoleColor color)
        {
            Console.ForegroundColor =  color;
        }

        public static IDisposable UseColors(ConsoleColor color, ConsoleColor restoreColor)
        {
            if (!Enabled)
                return DummyProfiler.Instance;

            return new ColorKeeper(color, restoreColor);
        }

        class ColorKeeper : IDisposable
        {
            ConsoleColor restoreColor;

            public ColorKeeper(ConsoleColor color, ConsoleColor restoreColor)
            {
                this.restoreColor = restoreColor;
                SetForeGroundColor(color);
            }

            void IDisposable.Dispose()
            {
                SetForeGroundColor(this.restoreColor);
            }
        }

        private ColorConsole() { }
    }
}

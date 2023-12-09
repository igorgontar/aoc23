
namespace Utils
{
    public static class AocMath
    {
        /// <summary>
        /// Algo described here: https://www.calculator.net/lcm-calculator.html
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long LCM(long[] val)
        {
            var gcd = GCD(val);
            long lcm = 1;
            for (var i = 0; i < val.Length; i++)
            {
                var m = val[i] / gcd;
                lcm *= m;        
            }
            return lcm*gcd;
        }

        /// <summary>
        /// Algo taken from here: https://extensionmethod.net/csharp/int32/lcm
        /// There was a bug, this fuction was called LCM, while in fact it calculates 
        /// GCD for a multiple numbers
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long GCD(long[] val)
        {
            var val1 = val[0];
            for (var i = 1; i < val.Length; i++)
            {
                var val2 = val[i];
                val1 = GCD(val1, val2);
            }
            return val1;
        }

        public static long GCD(long val1, long val2) 
        {
            while (val1 != 0 && val2 != 0)
            {
                if (val1 > val2)
                    val1 %= val2;
                else
                    val2 %= val1;
            }
            return Math.Max(val1, val2);
        }
    }
}
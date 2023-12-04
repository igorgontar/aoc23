using System;
using System.Collections.Generic;

namespace Utils
{
    public static class EnumHelper<T>
        where T : struct
    {
        private static readonly IReadOnlyDictionary<string, T> mapOrdinalIgnoreCase;
        private static readonly IReadOnlyDictionary<string, T> mapOrdinal;

        static EnumHelper()
        {
            var values = Enum.GetValues(typeof(T));

            var map1 = new Dictionary<string, T>(values.Length, StringComparer.Ordinal);
            mapOrdinal = map1;

            var map2 = new Dictionary<string, T>(values.Length, StringComparer.OrdinalIgnoreCase);
            mapOrdinalIgnoreCase = map2;

            foreach (object enumValue in values)
            {
                var key = enumValue.ToString();
                var val = (T)enumValue;
                map1.Add(key, val);
                map2.Add(key, val);
            }
        }

        public static T ParseSingle(string enumValueName, bool ignoreCase = false)
        {
            var map = ignoreCase ? mapOrdinalIgnoreCase : mapOrdinal;
            T val;
            if (!mapOrdinal.TryGetValue(enumValueName, out val))
            { 
                throw new ArgumentException($"Enum type {typeof(T).Name} does not containg value '{enumValueName}'");
            }
            return val;
        }
    }
}

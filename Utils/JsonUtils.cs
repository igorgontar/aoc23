using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Utils
{
    public sealed class JsonUtils
    {
        class PrintSerializer : JsonSerializer
        {
            public static readonly JsonSerializer NonIndented = new PrintSerializer();
            public static readonly JsonSerializer Indented = new PrintSerializer(true);

            private PrintSerializer(bool indented = false)
            {
                base.NullValueHandling = NullValueHandling.Ignore;
                base.DefaultValueHandling = DefaultValueHandling.Ignore;

                base.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                base.Converters.Add(new StringEnumConverter());

                base.Formatting = indented ? Formatting.Indented : Formatting.None;
            }
        }

        public static string Stringify(object o, bool indented = false)
        {
            var json = indented ? PrintSerializer.Indented : PrintSerializer.NonIndented;  
            var w = new StringWriter();
            json.Serialize(w, o);
            return w.ToString();
        }

    }
}
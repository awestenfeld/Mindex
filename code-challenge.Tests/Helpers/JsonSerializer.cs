using System;
using System.IO;
using Newtonsoft.Json;

namespace code_challenge.Tests.Integration.Helpers
{
    public class JsonSerialization
    {
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();

        public string ToJson<T>(T obj)
        {
            string json = null;
            if (obj != null)
            {
                using (var sw = new StringWriter())
                using (var jtw = new JsonTextWriter(sw))
                {
                    _serializer.Serialize(jtw, obj);
                    json = sw.ToString();
                }
            }
            return json;
        }
    }
}

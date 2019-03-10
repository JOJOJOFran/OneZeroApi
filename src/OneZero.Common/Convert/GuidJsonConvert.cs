using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Convert
{
    public class GuidJsonConvert : JsonConverter<Guid>
    {
        public override Guid ReadJson(JsonReader reader, Type objectType, Guid existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Guid guid;
            if (!Guid.TryParse((string)reader.Value, out guid))
            {
                return default(Guid);
            }
            else
            {
                return guid;
            }
        }

        public override void WriteJson(JsonWriter writer, Guid value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

using System;
using System.Globalization;
using Newtonsoft.Json;

namespace QuantConnect.DesktopServer.JsonConverters
{
    class DecimalJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var val = Convert.ToDecimal(reader.Value);
            return val;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((decimal)value).ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}

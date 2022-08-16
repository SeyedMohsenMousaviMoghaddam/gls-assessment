using DAL.Infra;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WebAPI.Infrastructure
{
    public class PersianDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime)) || (objectType == typeof(DateTime?));

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteValue("ثبت نشده");
            else
            {
                DateTime date = (DateTime)value;
                writer.WriteValue(date);
            }
        }
    }

    public class EnumToPersianConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var type = value.GetType();
                var result = type.GetMember(value.ToString())?.First()?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "";
                writer.WriteValue(result);
            }
            catch (Exception)
            {

                writer.WriteValue("-");

            }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

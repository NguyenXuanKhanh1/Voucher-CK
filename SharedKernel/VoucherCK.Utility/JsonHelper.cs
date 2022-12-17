using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Text.Json;
using VoucherCK.SharedKernel.Helpers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VoucherCK.Utility
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions DefaultOption = new() { WriteIndented = true };
        private static readonly string[] ULowerCase = new[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",  
            "đ",  
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",  
            "í","ì","ỉ","ĩ","ị",  
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",  
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",  
            "ý","ỳ","ỷ","ỹ","ỵ",};  
        private static readonly string[] LowerCase = new[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",  
            "d",  
            "e","e","e","e","e","e","e","e","e","e","e",  
            "i","i","i","i","i",  
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",  
            "u","u","u","u","u","u","u","u","u","u","u",  
            "y","y","y","y","y",};  
        public static byte[] Serialize<T>(T value, JsonSerializerOptions options)
        {
            var json = JsonSerializer.Serialize(value, value.GetType(), options);
            return Encoding.UTF8.GetBytes(json);
        }

        public static T Deserialize<T>(byte[] binary, JsonSerializerOptions options)
        {
            if (binary == null || binary.Length == 0)
            {
                return default;
            }

            try
            {
                var json = Encoding.UTF8.GetString(binary);
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch
            {
                return default;
            }
        }

        public static T Deserialize<T>(byte[] binary)
        {
            if (binary == null || binary.Length == 0)
            {
                return default;
            }

            try
            {
                var json = Encoding.UTF8.GetString(binary);
                return JsonSerializer.Deserialize<T>(json, DefaultOption);
            }
            catch
            {
                return default;
            }
        }


        public static string ToJson(this object obj, bool needConvertDateTimeToUtc = true)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (needConvertDateTimeToUtc)
                {
                    serializerSettings.Converters.Add(new DateTimeConverter());
                }

                return JsonConvert.SerializeObject(obj, serializerSettings);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public static string NonUnicode(this string text)  
        {
            for (var i = 0; i < ULowerCase.Length; i++)  
            {  
                text = text.Replace(ULowerCase[i], LowerCase[i]);  
                text = text.Replace(ULowerCase[i].ToUpper(), LowerCase[i].ToUpper());  
            }  
            return text;  
        }  

        public static T? Deserialize<T>(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return default;
            }
            try
            {
                return JsonSerializer.Deserialize<T>(inputString, DefaultOption);
            }
            catch
            {
                return default;
            }
        }
        
        public static T? NewtonSoftJsonDeserialize<T>(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return default;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(inputString);
            }
            catch
            {
                return default;
            }
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        public override void WriteJson(JsonWriter writer, DateTime? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(DateTimeHelper.ConvertFromTimeZoneInfo(value.Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffZ"));
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

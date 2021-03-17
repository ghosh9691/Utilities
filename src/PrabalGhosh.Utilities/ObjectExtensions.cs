using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace PrabalGhosh.Utilities
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Clone an object. The object has to be Json serializable
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="source">The object to be cloned</param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static string ToJson(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool IsEqual(this object obj, object compareTo)
        {
            var objJson = obj.ToJson().GetHash();
            var compareToJson = compareTo.ToJson().GetHash();
            return objJson.Equals(compareToJson);
        }

        public static byte[] ToByteArray(this object obj)
        {
            if (obj.IsNull())
                return null;
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray.IsNull())
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(byteArray, 0, byteArray.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (T) bf.Deserialize(ms);
            }
        }
    }
}
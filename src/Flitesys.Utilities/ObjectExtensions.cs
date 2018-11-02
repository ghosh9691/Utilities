using Newtonsoft.Json;

namespace Flitesys.Utilities
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

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
    }
}
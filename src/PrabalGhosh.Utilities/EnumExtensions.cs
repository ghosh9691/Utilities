using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PrabalGhosh.Utilities
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";
            return DisplayNameCache.GetOrAdd(key, k =>
            {
                var fieldInfo = value.GetType().GetTypeInfo().GetField(value.ToString());
                var descriptionAttributes = fieldInfo?.GetCustomAttributes<DescriptionAttribute>(false);
                return descriptionAttributes?.FirstOrDefault()?.Description ?? value.ToString();
            });
        }
    }
}
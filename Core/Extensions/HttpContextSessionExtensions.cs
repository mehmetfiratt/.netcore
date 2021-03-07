using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using static System.String;

namespace Core.Extensions
{
    public static class HttpContextSessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);

            session.SetString(key, objectString);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            var objectString = session.GetString(key);
            if (IsNullOrEmpty(objectString))
            {
                return null;
            }

            T value = JsonConvert.DeserializeObject<T>(objectString);

            return value;
        }
    }
}

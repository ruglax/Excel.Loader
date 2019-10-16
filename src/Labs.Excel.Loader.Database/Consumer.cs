using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using Labs.Excel.Loader.Model;

namespace Labs.Excel.Loader.Database
{
    public class Consumer : IConsumer
    {
        private static readonly ConcurrentDictionary<string, Type> ConcurrentBag =
            new ConcurrentDictionary<string, Type>();

        public T Transform<T>(Message message) where T : class
        {
            try
            {
                Type type = typeof(T);
                JObject jobject = message.JToken as JObject;
                return (T)jobject?.ToObject(type);
            }
            catch (Exception e)
            {
                //Console.WriteLine("--------------------");
                //Console.WriteLine(message.JToken);
                //Console.WriteLine(e);
                //Console.WriteLine("--------------------");
                return null;
            }
        }
    }
}

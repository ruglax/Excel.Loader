using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using Labs.Excel.Loader.Model;

namespace Labs.Excel.Loader.Database
{
    public class Consumer : IConsumer
    {
        public T Transform<T>(Message message) where T : class
        {
            try
            {
                Type type = typeof(T);
                JObject jobject = message.JToken as JObject;
                T entity = (T) jobject?.ToObject(type);
                if (entity != null)
                {
                    return entity;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("--------------------");
                //Console.WriteLine(message.JToken);
                //Console.WriteLine(e);
                //Console.WriteLine("--------------------");
            }

            return null;
        }
    }
}

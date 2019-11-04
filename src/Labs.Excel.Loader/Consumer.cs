using System;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader
{
    public class Consumer : IConsumer
    {
        private readonly ILogger<Consumer> _logger;

        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
        }

        public T Transform<T>(Message message) where T : class
        {
            try
            {
                Type type = typeof(T);
                _logger.LogDebug( $"Processing entity: {type.Name}", message);
                JObject jobject = message.JToken as JObject;
                T entity = (T) jobject?.ToObject(type);
                if (entity != null)
                {
                    return entity;
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, $"Error deserializing object type: {typeof(T).Name}");
            }

            return null;
        }
    }
}

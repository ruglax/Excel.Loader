using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;

namespace Labs.Excel.Loader.Database
{
    public class Consumer : IConsumer
    {
        public void Transform(Message message)
        {
            Type type = Type.GetType($"Labs.Excel.Loader.Database.{message.Type}");
            JObject jobject = message.JToken as JObject;
            try
            {
                if (type != null)
                {
                    try
                    {
                        var entity = jobject?.ToObject(type);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("--------------------");
                        Console.WriteLine(message.JToken);
                        Console.WriteLine(e);
                        Console.WriteLine("--------------------");
                    }
                    
                    //Console.WriteLine(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}

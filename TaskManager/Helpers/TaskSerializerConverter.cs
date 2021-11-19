using System;
using Newtonsoft.Json;
using TaskManager.Models;

namespace TaskManager.Helpers
{
    
    //helper class needed to serialize tree object
    public class TaskSerializerConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            //we currently support only writing of JSON
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Task).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = value as Task;
            serializer.Serialize(writer, node.Children);
        }
}
}
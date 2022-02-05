using System.IO;
using Newtonsoft.Json;

namespace Serialization {
    
    public static class Serializator {

        public static void SerializeData(object data, string savePath) {
            string serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(savePath, serializedData);
        }

        public static T DeserializeData<T>(string savePath) {
            string deserializeData = "{}";
            try {
                deserializeData = File.ReadAllText(savePath);
            } catch {
                SerializeData(null, savePath);
            }
            return JsonConvert.DeserializeObject<T>(deserializeData);
        }
        
    }
    
}
using System.Text.Json;

namespace Testiny.Helpers
{
    public static class JsonHelper<T>
    {
        public static T FromJson(string path, FileMode open)
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            return JsonSerializer.Deserialize<T>(fs);
        }

        public static T FromJson(string path)
        {
            return JsonSerializer.Deserialize<T>(path);
        }

        public static string ToJson(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
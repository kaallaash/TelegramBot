using Newtonsoft.Json;
using System.IO;

namespace TelegramBot
{
    public class Config
    {
        public static string GetToken(string path)
        {
            return JsonConvert.DeserializeObject<string>(File.ReadAllText(path));
        }
    }
}

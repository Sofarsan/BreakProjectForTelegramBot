using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Telegram
{
    public static class BaseSerialize
    {
        private const string userDictionaryJson = @"Garry.json";
        public static string UserDictionarySerialize(Dictionary<long, string> dic)
        {
            return JsonSerializer.Serialize<Dictionary<long, string>>(dic);
        }
        public static Dictionary<long, string> UserDictionaryDecerialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("Garry.json");
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<long, string>>(json);
            }
        }
        public static void SaveUserDictionary(Dictionary<long, string> dic)
        {
            string json = UserDictionarySerialize(dic);

            using (StreamWriter sw = new StreamWriter(userDictionaryJson, false))
            {
                sw.WriteLine(json);
            }
        }

        public static void LoadUserDictionary()
        {
            if (File.Exists(userDictionaryJson))
            {
                using (StreamReader sr = new StreamReader(userDictionaryJson))
                {
                    string json = sr.ReadLine();

                    BaseBot.NameBase = UserDictionaryDecerialize(json);

                }
            }
        }
    }
}

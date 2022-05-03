using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

        private const string testsDictionaryJson = @"Tests.json";
        public static string testsDictionarySerialize(ObservableCollection<Test>tests)
        {
            return JsonSerializer.Serialize<ObservableCollection< Test >> (tests);
        }

        public static ObservableCollection<Test> TestsDictionaryDecerialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(testsDictionaryJson);
            }
            else
            {
                return JsonSerializer.Deserialize <ObservableCollection< Test >> (json);
            }
        }

        public static void SaveTestsDictionary(ObservableCollection<Test> tests)
        {
            string json = testsDictionarySerialize(tests);

            using (StreamWriter sw = new StreamWriter(testsDictionaryJson, false))
            {
                sw.WriteLine(json);
            }
        }

        public static ObservableCollection<Test> LoadTestsDictionary()
        {
            if (File.Exists(userDictionaryJson))
            {
                using (StreamReader sr = new StreamReader(testsDictionaryJson))
                {
                    string json = sr.ReadLine();

                    return TestsDictionaryDecerialize(json);

                }
            }
            else
            {
                return new ObservableCollection<Test>();
            }
        }
    }
}

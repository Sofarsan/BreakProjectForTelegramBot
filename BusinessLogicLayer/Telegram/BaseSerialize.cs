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
        public static string UserDictionarySerialize(Dictionary<long, List<string>> dic)
        {
            return JsonSerializer.Serialize<Dictionary<long, List<string>>>(dic);
        }
        public static Dictionary<long, List<string>> UserDictionaryDecerialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(userDictionaryJson);
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<long, List<string>>>(json);
            }
        }
        public static void SaveUserDictionary(Dictionary<long, List<string>> dic)
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
        public static string TestsObservableCollectionSerialize(ObservableCollection<Test>tests)
        {
            return JsonSerializer.Serialize<ObservableCollection< Test >> (tests);
        }

        public static ObservableCollection<Test> TestsObservableCollectionDecerialize(string json)
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

        public static void SaveTestsObservableCollection(ObservableCollection<Test> tests)
        {
            string json = TestsObservableCollectionSerialize(tests);

            using (StreamWriter sw = new StreamWriter(testsDictionaryJson, false))
            {
                sw.WriteLine(json);
            }
        }

        public static ObservableCollection<Test> LoadTestsDictionary()
        {
            if (File.Exists(testsDictionaryJson))
            {
                using (StreamReader sr = new StreamReader(testsDictionaryJson))
                {
                    string json = sr.ReadLine();

                    return TestsObservableCollectionDecerialize(json);

                }
            }
            else
            {
                return new ObservableCollection<Test>();
            }
        }
    }
}

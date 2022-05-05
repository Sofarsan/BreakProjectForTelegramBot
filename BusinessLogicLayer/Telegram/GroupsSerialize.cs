using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer.GroupsSerialize
{
    public static class GroupsSerialize
    {
        private const string groupsDictionaryJson = @"Groups.json";
        public static string GroupsObservableCollectionSerialize(ObservableCollection<UserGroup> groups)
        {
            return JsonSerializer.Serialize<ObservableCollection<UserGroup>>(groups);
        }

        public static ObservableCollection<UserGroup> GroupsObservableCollectionDecerialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(groupsDictionaryJson);
            }
            else
            {
                return JsonSerializer.Deserialize<ObservableCollection<UserGroup>>(json);
            }
        }

        public static void SaveGroupsObservableCollection(ObservableCollection<UserGroup> groups)
        {
            string json = GroupsObservableCollectionSerialize(groups);

            using (StreamWriter sw = new StreamWriter(groupsDictionaryJson, false))
            {
                sw.WriteLine(json);
            }
        }

        public static ObservableCollection<UserGroup> LoadGroupsObservableCollectionDecerialize()
        {
            if (File.Exists(groupsDictionaryJson))
            {
                using (StreamReader sr = new StreamReader(groupsDictionaryJson))
                {
                    string json = sr.ReadLine();

                    return GroupsObservableCollectionDecerialize(json);

                }
            }
            else
            {
                return new ObservableCollection<UserGroup>();
            }
        }
    }
}


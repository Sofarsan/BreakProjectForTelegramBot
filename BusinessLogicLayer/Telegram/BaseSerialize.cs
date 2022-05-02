﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Telegram
{
    public static class BaseSerialize
    {
        private const string userDictionaryJson = @"Garry.json";
        public static string UserDictionarySerialize(Dictionary<long,string> dic)
        {
            return JsonSerializer.Serialize<Dictionary<long, string>>(dic);
        }
        public static Dictionary<long, string> UserDictionaryDecerialize(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<long, string>>(json);
            }
        }
        public static void Save(Dictionary<long, string> dic)
        {
            string json = UserDictionarySerialize(dic);

            using (StreamWriter sw = new StreamWriter(userDictionaryJson, false))
            {
                sw.WriteLine(json);
            }
        }

        public static Dictionary<long, string> Load()
        {
            using (StreamReader sr = new StreamReader(userDictionaryJson))
            {
                string json = sr.ReadLine();
                return UserDictionaryDecerialize(json);
            }
        }
    }
}

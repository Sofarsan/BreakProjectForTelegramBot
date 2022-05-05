using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Telegram
{
    public static class BaseBot
    {
        public static Dictionary<long, User> NameBase { get; set; } = new Dictionary<long, User>();
    }
   
}

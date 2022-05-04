using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer

{
    public class ReplyStorage
    {
        public List<Reply> Reports { get; private set; }

        private static ReplyStorage _instance;

        private ReplyStorage()
        {
            Reports = new List<Reply>();
        }

        public static ReplyStorage GetInstance()
        {
            return _instance;
        }     
    }
}

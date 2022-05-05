using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Reply
    {
        public string Name { get; set; }
        public List<string> Questions { get; set; } = new List<string>();
        public List<string> UserAnswer { get; set; } = new List<string>();

        

    }
}

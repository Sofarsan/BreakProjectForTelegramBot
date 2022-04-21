using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class User
    {

        public string? LastName { get; set; }
        public User(string nameUser)
        {
            LastName = nameUser;
        }
        public override string ToString()
        {
            return LastName;
        }

    }
}

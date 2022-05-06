using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserGroup
    {
        public string NameGroup { get; set; }
        public List<User> Users { get; set; }
        
        public UserGroup(string nameGroup)
        {
            NameGroup = nameGroup;
            Users = new List<User>();
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public override string ToString()
        {
            return NameGroup;
        }
    }
}

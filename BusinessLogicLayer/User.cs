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
        public string? FirstName { get; set; }
        public long Id { get; set; }
        public User(string lastNameUser, string firstNameUser, long ids)
        {
            LastName = lastNameUser;
            FirstName = firstNameUser;
            Id = ids;
        }
        public override string ToString()
        {
            return FirstName + ", " + LastName;
        }
        
       

    }
}

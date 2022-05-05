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
        public OngoingTest? ongoingTest { get; set; }
        public User(string lastName, string firstName, long id)
        {
            LastName = lastName;
            FirstName = firstName;
            Id = id;
        }
        public override string ToString()
        {
            return FirstName + ", " + LastName;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            User other = (User)obj;

            return other.Id == this.Id;
        }
    }
}

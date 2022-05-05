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
        public OngoingTest? ongoingTest { get; set; } = null;
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
    }
}

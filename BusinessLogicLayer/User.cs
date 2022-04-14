using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class User
    {
        string _lastName;
        string _name;
        int _age;

        public User (string lastName, string name, int age)
        {
            _lastName=lastName;
            _name=name;
            _age=age;
        }
    }
}

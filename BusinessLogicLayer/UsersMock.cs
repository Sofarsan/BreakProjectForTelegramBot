using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UsersMock
    {
        public static UserGroup GetGroupNumberOne()
        {
            User user1 = new User("Собянин Валерий");
            User user2 = new User("Сысоев Сергей");
            User user3 = new User("Голубев Григорий");
            UserGroup groupOne = new UserGroup("Всегда правы");
            groupOne.AddUser(user1);
            groupOne.AddUser(user2);
            groupOne.AddUser(user3);
            return groupOne;
        }

        public static UserGroup GetGroupNumberTwo()
        {
            User user1 = new User("Гуси");
            User user2 = new User("Петухи");
            User user3 = new User("Голуби");
            User user4 = new User("Цапли");
            UserGroup groupTwo = new UserGroup("Птицы");
            groupTwo.AddUser(user1);
            groupTwo.AddUser(user2);
            groupTwo.AddUser(user3);
            groupTwo.AddUser(user4);
            return groupTwo;
        }
        public static UserGroup GetGroupNumberTree()
        {
            User user1 = new User("Черное ");
            User user2 = new User("Белое");
            User user3 = new User("Красное");
            UserGroup groupTree = new UserGroup("Моря");
            groupTree.AddUser(user1);
            groupTree.AddUser(user2);
            groupTree.AddUser(user3);
            return groupTree;
        }
    }
}

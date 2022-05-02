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
            User user1 = new User("Собянин","Валерий", 1);
            User user2 = new User("Сысоев"," Сергей",2);
            User user3 = new User("Голубев"," Григорий",3);
            UserGroup groupOne = new UserGroup("Всегда правы");
            groupOne.AddUser(user1);
            groupOne.AddUser(user2);
            groupOne.AddUser(user3);
            return groupOne;
        }

        public static UserGroup GetGroupNumberTwo()
        {
            User user1 = new User("Гуси", "Пернатые",4);
            User user2 = new User("Петухи","Пернатые",5);
            User user3 = new User("Голуби", "Пернатые",6);
            User user4 = new User("Цапли", "Пернатые",7);
            UserGroup groupTwo = new UserGroup("Птицы");
            groupTwo.AddUser(user1);
            groupTwo.AddUser(user2);
            groupTwo.AddUser(user3);
            groupTwo.AddUser(user4);
            return groupTwo;
        }
        public static UserGroup GetGroupNumberTree()
        {
            User user1 = new User("Черное", "Чистое", 8);
            User user2 = new User("Белое", "Соленое", 9);
            User user3 = new User("Красное", "Теплое", 10);
            UserGroup groupTree = new UserGroup("Моря");
            groupTree.AddUser(user1);
            groupTree.AddUser(user2);
            groupTree.AddUser(user3);
            return groupTree;
        }
    }
}

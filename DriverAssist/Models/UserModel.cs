using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriverAssist.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class ActualUser
    {
        public string GetUserName
        {
            get { return "test"; }
        }
    }

    public class UserTable
    {
      

        public static IList<UserModel> GetUserTable()
        {
            List<UserModel> table = new List<UserModel>();

            table.Add(new UserModel()
            {
                ID = 1,
                Nickname = "klimajster",
                Name = "Rafał",
                Surname = "Klimas"
            });

            table.Add(new UserModel()
            {
                ID = 2,
                Nickname = "fafaello",
                Name = "Fafał",
                Surname = "Klimas"
            });

            table.Add(new UserModel()
            {
                ID = 3,
                Nickname = "alealealeksandra",
                Name = "Aleksandra",
                Surname = "Kardas"
            });

            table.Add(new UserModel()
            {
                ID = 4,
                Nickname = "robertson",
                Name = "Robert",
                Surname = "Nawias"
            });
            table.Add(new UserModel()
            {
                ID = 5,
                Nickname = "Janusz",
                Name = "Rafał",
                Surname = "Klimas"
            });

            table.Add(new UserModel()
            {
                ID = 6,
                Nickname = "Rocky",
                Name = "Ronnie",
                Surname = "O'Sullivan"
            });
            table.Add(new UserModel()
            {
                ID = 7,
                Nickname = "Janusz",
                Name = "Rafał",
                Surname = "Klimas"
            });

            table.Add(new UserModel()
            {
                ID = 8,
                Nickname = "robertson",
                Name = "Robert",
                Surname = "Nawias"
            });
            table.Add(new UserModel()
            {
                ID = 9,
                Nickname = "Janusz",
                Name = "Rafał",
                Surname = "Klimas"
            });

            table.Add(new UserModel()
            {
                ID = 10,
                Nickname = "Rocky",
                Name = "Ronnie",
                Surname = "O'Sullivan"
            });
            table.Add(new UserModel()
            {
                ID = 11,
                Nickname = "Janusz",
                Name = "Rafał",
                Surname = "Klimas"
            });
            return table;
        }


    }
}
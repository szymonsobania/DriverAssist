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
        public string Car { get; set; }
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
                Car = "Toyota Yaris"
            });

            table.Add(new UserModel()
            {
                ID = 2,
                Nickname = "fafaello",
                Car = "Renault Clio"
            });

            table.Add(new UserModel()
            {
                ID = 3,
                Nickname = "alealealeksandra",
                Car = "Kia Rio"
            });

            table.Add(new UserModel()
            {
                ID = 4,
                Nickname = "robertson",
                Car = "Toyota Auris"
            });
            table.Add(new UserModel()
            {
                ID = 5,
                Nickname = "Janusz",
                Car = "Fiat Cinquecento"
            });

            return table;
        }


    }
}
using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public class UserSaver
    {
        public static User currentUser = null;
        private static AppDBContext _db;

        public static User GetUser(User user = null)
        {
            if (user is not null)
            {
                currentUser = user;
            }
            return currentUser;
        }

        public static AppDBContext GetDB()
        {
            if (_db is null)
                _db = new();
            return _db;
        }
    }
}

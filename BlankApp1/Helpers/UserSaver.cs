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

        public static User GetUser(User user = null)
        {
            if (user is not null)
                currentUser = user;
            return currentUser;
        }

    }
}

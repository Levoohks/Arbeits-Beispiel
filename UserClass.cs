using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Alt_Gen
{
    class UserClass
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserClass(string user, string pass)
        {
            UserName = user;
            Password = pass;
        }
        public UserClass()
        {
        }

        private static string _error;

        public static bool IsEqual(UserClass user1, UserClass user2)
        {
            if (user1 == null || user2 == null) { return false; }

            if (user1.UserName != user2.UserName)
            {
                _error = "Wrong Username / Password!";
                return false;
            }

            else if (user1.Password != user2.Password)
            {
                _error = "Wrong Username / Password!";
                return false;
            }

            return true;
        }
    }
}

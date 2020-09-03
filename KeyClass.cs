using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Alt_Gen
{
    class KeyClass
    {
        public string Key { get; set; }
        public int amount { get; set; }
        public string used { get; set; }

        public KeyClass(string key)
        {
            Key = key;
        }
        public KeyClass()
        {
        }

        private static string _error;

        public static bool IsEqual(KeyClass user1, KeyClass user2)
        {
            if (user1 == null || user2 == null) { return false; }

            if (user1.Key != user2.Key)
            {
                _error = "OUT: Wrong Key or used";
                return false;
            }
            return true;
        }
    }
}

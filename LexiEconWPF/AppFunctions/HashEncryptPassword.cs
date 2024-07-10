using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF.AppFunctions
{
    class HashEncryptPassword
    {
        public static string ApplyHash(string password)
        {
			MD5 md5 = MD5.Create();
			byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            string res = Convert.ToBase64String(s);
            return res;
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    internal class EncryptionUtilite
    {

        private static readonly string _OrginalChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*)(_+";
        private static readonly string _RandChar =    "qW1S6mXP+Hh@uD3V5J8MIw)zvb4k#0(p!Yn7G$L*ot&Uj9TFcOKlRBiZy_NxsCdgrA2Ea%^";
        public static string Encrypt(string password) {
            var sb = new StringBuilder();
            foreach (char ch in password)
            {
                var indexChar =_OrginalChar.IndexOf(ch);
                sb.Append(_RandChar[indexChar]);
            }
            return  sb.ToString();
        }
        public static string Dncrypt(string password) {
            var sb = new StringBuilder();
            foreach (char ch in password)
            {
                var indexChar = _RandChar.IndexOf(ch);
                sb.Append(_OrginalChar[indexChar]);
            }
            return sb.ToString();

        }


    }
}

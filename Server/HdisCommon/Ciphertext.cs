using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisCommon
{
   public class Ciphertext
    {
        private static char[] Key = "`1234567890-=~!@#$%^&*()_+qwertyuiop[]\\QWERTYUIOP{}|asdfghjkl;ASDFGHJKL:zxcvbnm,./ZXCVBNM<>? '\"".ToCharArray();


        public static string Decrypt(string str)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int pos = (GetCharPos(str[i]) - i - 1);
                pos = pos % Key.Length;
                if (pos < 0)
                {
                    pos += Key.Length;
                }
                builder.Append(GetChar(pos));
            }
            return builder.ToString();
        }

        public static string Encrypt(string str)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int pos = GetCharPos(str[i]) + i + 1;
                pos = pos % Key.Length;
                builder.Append(GetChar(pos));
            }
            return builder.ToString();
        }




        private static int GetCharPos(char ch)
        {
            int num = 0;
            for (int i = 0; i < Key.Length; i++)
            {
                if (ch == Key[i])
                {
                    return i;
                }
            }
            return num;
        }


        private static char GetChar(int Pos)
        {
            return Key[Pos];
        }

 

    }
}

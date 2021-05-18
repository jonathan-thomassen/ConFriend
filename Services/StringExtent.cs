
using System;

namespace ConFriend
{
    public static class StringExtent
    {
        public static string FormatToSQL(this String str)
        {
            str.Replace(",", "[*%0]");
            
            return str;
        }
        public static string FormatFromSQL(this String str)
        {
            str.Replace("[*%0]", ",");
            return str;
        }
    }
   
}

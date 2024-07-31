using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ConvertData
    {
        public static string ToString(this byte[] data, int toBase)
        {
            string str = "";
            for (int i = 0, length = data.Length; i < length; i++)
            {
                str += data[i].ToString(toBase) + "-";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }

        /// <param name="toBase">进制表示</param>
        public static string ToString(this byte num, int toBase)
        {
            if (toBase == 2)//二进制
                return Convert.ToString(num, toBase).PadLeft(8, '0');
            return Convert.ToString(num, toBase);
        }
    }
}

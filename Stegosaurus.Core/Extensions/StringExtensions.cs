using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Stegosaurus.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Transform string into binary string using UTF8 encoding
        /// </summary>
        /// <param name="data">Data string</param>
        /// <returns>Binary string</returns>
        public static string ToBinary(this string data)
        {
            return string.Join(
                string.Empty,
                Encoding.ASCII.GetBytes(data).Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))
                );
        }

        /// <summary>
        /// Transform binary string into string using UTF8 encoding
        /// </summary>
        /// <param name="data">Binary string</param>
        /// <returns>Original string</returns>
        public static string FromBinary(this string data)
        {
            return Encoding.ASCII.GetString(
                Regex.Split(data, "(.{8})")
                    .Where(binary => !string.IsNullOrEmpty(binary)) 
                    .Select(binary => Convert.ToByte(binary, 2))
                    .ToArray());
        }
    }
}

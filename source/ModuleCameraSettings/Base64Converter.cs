using System.Collections.Generic;
using System.Text;

namespace ModuleCameraSettings
{
    public static class Base64Converter
    {
        // 6-bits printable characters.
        private const string Table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        /// <summary>
        /// Converts a message to its equivalent string representation
        /// that is encoded with base-64 digits.
        /// </summary>
        /// <param name="message">text string.</param>
        /// <returns>The string representation, in base 64, of the contents of message.</returns>
        public static string Encode(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            return Encode(bytes);
        }

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        /// that is encoded with base-64 digits.
        /// </summary>
        /// <param name="bytes">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation, in base 64, of the contents of array.</returns>
        public static string Encode(byte[] bytes)
        {
            // Enumerate byte array by 6-bits.
            static IEnumerable<byte> GetTableIndexes(byte[] bytes)
            {
                int i, value;

                for (i = 0; i <= bytes.Length - 3; i += 3)
                {
                    value = (bytes[i] << 16) | (bytes[i + 1] << 8) | bytes[i + 2];
                    yield return (byte)((value >> 18) & 0x3f);
                    yield return (byte)((value >> 12) & 0x3f);
                    yield return (byte)((value >> 6) & 0x3f);
                    yield return (byte)(value & 0x3f);
                }

                switch (bytes.Length - i)
                {
                    case 2:
                        value = (bytes[i] << 16) | (bytes[i + 1] << 8);
                        yield return (byte)((value >> 18) & 0x3f);
                        yield return (byte)((value >> 12) & 0x3f);
                        yield return (byte)((value >> 6) & 0x3f);
                        break;
                    case 1:
                        value = bytes[i] << 16;
                        yield return (byte)((value >> 18) & 0x3f);
                        yield return (byte)((value >> 12) & 0x3f);
                        break;
                }
            }

            // Converts 6-bits values to a printable characters.
            var sb = new StringBuilder();
            foreach (byte index in GetTableIndexes(bytes))
            {
                sb.Append(Table[index]);
            }

            // Padding
            switch (bytes.Length % 3)
            {
                case 2:
                    sb.Append('=');
                    break;
                case 1:
                    sb.Append("==");
                    break;
            }

            return sb.ToString();
        }
    }
}

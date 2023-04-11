using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Datacom.TestAutomation.Common
{
    public static class StringExtensions
    {
        public static string ExtractPattern(this string source, string regex) => new Regex(regex).Match(source).Value;

        public static bool IsNullOrEmpty(this string? fieldValue)
            => string.IsNullOrWhiteSpace(fieldValue?.Trim()) && string.IsNullOrEmpty(fieldValue?.Trim());

        public static bool IsNotNullOrEmpty(this string? fieldValue)
            => !IsNullOrEmpty(fieldValue);

        public static bool IsValueEqualsTo(this string fieldValue, string expected, bool ignoreCase = true)
        {
            var isNullEmptyOrWhitespace = fieldValue.IsNullOrEmpty() && expected.IsNullOrEmpty();
            if (ignoreCase)
            {
                return fieldValue.Equals(expected, StringComparison.InvariantCultureIgnoreCase) || isNullEmptyOrWhitespace;
            }
            else
            {
                return fieldValue.Equals(expected) || isNullEmptyOrWhitespace;
            }
        }

        public static bool ValueContains(this string fieldValue, string expected, bool ignoreCase = true)
        {
            var isNullEmptyOrWhitespace = fieldValue.IsNullOrEmpty() && expected.IsNullOrEmpty();
            if (ignoreCase)
            {
                return fieldValue.Contains(expected, StringComparison.InvariantCultureIgnoreCase) || isNullEmptyOrWhitespace;
            }
            else
            {
                return fieldValue.Contains(expected) || isNullEmptyOrWhitespace;
            }
        }

        public static string ToLowerString(this string value)
            => value?.TrimSpecialCharacters().ToLower()!;

        public static SecureString ToSecureString(this string @string)
        {
            SecureString secureString = new();

            if (@string.Length > 0)
            {
                foreach (var c in @string.ToCharArray())
                    secureString.AppendChar(c);
            }

            return secureString;
        }

        public static string ToUnsecureString(this SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                return Marshal.PtrToStringUni(unmanagedString)!;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static string TrimSpecialCharacters(this string value)
        {
            char[] trimCharacters = {
                '\r',
                '\n',
                (char) 60644, // 
                (char) 60932, // 
                (char) 59540, // 
                (char) 60038, // 
                (char) 61424, // 
                (char) 59902, //
            };

            var result = value?.Trim()
                .Trim(trimCharacters)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            return result!;
        }

        public static IEnumerable<string> GetStringBetween(this string message, string firstString, string lastString)
        {
            Regex regx = new(@$"(?s)(?<={firstString}).+?(?={lastString})", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            MatchCollection matches = regx.Matches(message);

            return matches.Select(match => match.Value);
        }

        public static IEnumerable<string> GetStringAfter(this string message, string keyword)
        {
            Regex regx = new(@$"(?s)(?<={keyword}).+\S+", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            MatchCollection matches = regx.Matches(message);

            return matches.Select(match => match.Value);
        }

        public static string EncryptXOR(this string message, string key)
        {
            if (message == null || key == null) 
                return string.Empty;

            char[] keys = key.ToCharArray();
            char[] mesg = message.ToCharArray();
                
            int ml = mesg.Length;
            int kl = keys.Length;

            char[] newmsg = new char[ml];
            for (int i = 0; i < ml; i++)
            {
                newmsg[i] = (char)(mesg[i] ^ keys[i % kl]);
            }

            var valueBytes = Encoding.UTF8.GetBytes(newmsg);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecryptXOR(this string message, string key)
        {
            if (message == null || key == null)
                return string.Empty;

            char[] keys = key.ToCharArray();

            var valueBytes = Convert.FromBase64String(message);
            char[] mesg = Encoding.UTF8.GetChars(valueBytes);

            int ml = mesg.Length;
            int kl = keys.Length;
            char[] newmsg = new char[ml];
            for (int i = 0; i < ml; i++)
            {
                newmsg[i] = (char)(mesg[i] ^ keys[i % kl]);
            }

            return new string(newmsg);
        }

        private static string EncryptOrDecrypt(this string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
        }
    }
}

namespace SSO.Demo.Toolkits.Extension
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string inputStr)
        {
            return string.IsNullOrEmpty(inputStr);
        }

        public static string ValueOfAppsettings(this string inputStr)
        {
            // Configuration
            return "";
        }

        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                var hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                char c;

                c = char.ToLowerInvariant(chars[i]);

                chars[i] = c;
            }

            return new string(chars);
        }
    }
}

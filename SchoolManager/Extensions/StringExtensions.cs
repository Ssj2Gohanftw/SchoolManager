using System.Globalization;
namespace SchoolManager.Extensions
{
    public static class StringExtensions
    {
        public static string? ToTitleCase(this string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            var TextInfo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
            return str;
        }
    }
}

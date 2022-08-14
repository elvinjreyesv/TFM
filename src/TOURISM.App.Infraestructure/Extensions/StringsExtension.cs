using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TOURISM.App.Infrastructure.Extensions
{
    public static class StringsExtension
    {
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return Enum.TryParse<T>(value, true, out T result) ? result : default(T);
        }

        public static string CleanSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Trim();
        }

        public static string CleanSpace(this string str, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue : str.Trim();
        }

        public static DateTime ToInvariantShortDate(this string str)
        {
            return DateTime.TryParseExact(str, "d", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ? date : DateTime.MinValue;
        }

        public static DateTime ToInvariantDateTime(this string str)
        {
            return DateTime.TryParseExact(str, "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ? date : DateTime.MinValue;
        }

        public static string ToInvariantDateTime(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
        }

        public static DateTime? ToInvariantNullShortDate(this string str)
        {
            return DateTime.TryParseExact(str, "d", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ? date :default(DateTime?);
        }

        public static string ToInvariantShortDate(this DateTime dt)
        {
            return dt.ToString("d", CultureInfo.InvariantCulture);
        }

        public static string SplitTextUpper(this string str)
        {
            string[] text = Regex.Split(str, @"(?<!^)(?=[A-Z])");

            return string.Join(" ", text);
        }

        public static string JoinChar(this string str)
        {
            if(!string.IsNullOrWhiteSpace(str))
                return string.Join("|", str.Split('|').Take(2).ToArray());

            return string.Empty;
        }

        public static string RemoveTextSpaces(this string str)
        {
            string text = Regex.Replace(str, @"\s+", "");
            return text;
        }

        public static string NumberOnly(this string inputValue)
        {
            if (string.IsNullOrWhiteSpace(inputValue)) return string.Empty;

            inputValue = inputValue.Trim();

            var regex = new Regex(@"\d+");
            string returnValue = "";
            MatchCollection values = regex.Matches(inputValue);
            foreach (Match value in values)
            {
                returnValue += value.ToString();
            }
            return returnValue;
        }

        public static string FormatInvariantDate(this DateTime date, string language)
        {
            string month = date.ToString("MMMM", CultureInfo.CreateSpecificCulture(language.ToLower()));
            string day = date.ToString("dddd", CultureInfo.CreateSpecificCulture(language.ToLower()));

            var result = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase($"{day}, {month} {date.Day}, {date.Year}");
            return result;
        }

        public static string ReplaceString(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
                return str
                    .Replace(" ", "")
                    .Replace("-", "")
                    .Replace("(","")
                    .Replace(")","");
            return string.Empty;
        }

        public static string ReplaceStringAlt(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
                return str
                    .Replace(" ", "")
                    .Replace("(", "")
                    .Replace(")", "");
            return string.Empty;
        }

        public static string EnsureCorrectFilename(this string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }
    }
}

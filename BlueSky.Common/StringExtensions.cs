using System;

namespace BlueSky.Common
{
    public static class StringExtensions
    {
        public const string FormatMessageValueNotProperEnum =
            "The given value '{0}' is not valid for Enum of type '{1}'";
        public const double DefaultDouble = 0.0;

        public const int DefaultInt = 0;
        public const string KeyValue = "value";
        public const string FormatMessageEnumTypeNeeded = "Expected Type '{0}' to be an Enum.";
        public const string MessageValidStringNeededForParsingEnum = "Valid string needed for parsing enum.";

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime finalValue;
            DateTime.TryParse(value, out finalValue);
            return finalValue;
        }

        public static double ToDouble(this string input, double def = DefaultDouble)
        {
            double finalValue;
            return double.TryParse(input, out finalValue) ? finalValue : def;
        }

        public static T ToEnum<T>(this string value, bool ignorecase = true) where T : struct 
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(KeyValue, MessageValidStringNeededForParsingEnum);

            Type t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException(FormatMessageEnumTypeNeeded.FormatWith(t), KeyValue);

            T finalValue;
            if (!Enum.TryParse(value, ignorecase, out finalValue))
            {
                throw new ArgumentException(FormatMessageValueNotProperEnum.FormatWith(value, t));
            }

            return finalValue;
        }

        public static int ToInt(this string input, int def = DefaultInt)
        {
            int finalValue;
            return int.TryParse(input, out finalValue) ? finalValue : def;
        }
    }
}
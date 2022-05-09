using System;

namespace TelegramBot
{
    public static class Date
    {
        public static string GetRussianDay()
        {
            var englishDay = DateTime.Now.DayOfWeek.ToString();

            switch (englishDay)
            {
                case "Sunday":
                    return "Воскресенье";
                case "Monday":
                    return "Понедельник";
                case "Tuesday":
                    return "Вторник";
                case "Wednesday":
                    return "Среда";
                case "Thursday":
                    return "Четверг";
                case "Friday":
                    return "Пятница";
                default:
                    return "Суббота";
            }            
        }

        public static DateTime ConvertToDateTime(string str)
        {
            str = str.Replace("\"", string.Empty);

            try
            {
                return DateTime.ParseExact(str, "yyyy-MM-ddTHH:mm:ss.fffffff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}

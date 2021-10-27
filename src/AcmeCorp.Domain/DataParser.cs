using System;
using System.Collections.Generic;
using System.Globalization;

namespace AcmeCorp.Domain
{
    /// <summary>
    /// Static class on charge of parsing sentences
    /// </summary>
    public static class DataParser
    {
        private const string FormatSentence = "\"NAME\"=\"MO|TU|WE|TH|FR|SA|SU\"HH:mm-HH:mm,HH:mm-HH:mm...";
        private const string FormatSchedule = "MOHH:mm-HH:mm";

        private static readonly Dictionary<string, short> Days = new Dictionary<string, short>() { { "SU", 0 }, { "MO", 1 }, { "TU", 2 }, { "WE", 3 }, { "TH", 4 }, { "FR", 5 }, { "SA", 6 } };

        /// <summary>
        /// Function to parse the input received
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns>Employee object with a list with the hours detail</returns>
        public static Attendance ParseSentenceEmployee(this string sentence)
        {
            var sentenceDetail = sentence.Split('=');

            if (sentenceDetail.Length != 2)
                throw new ArgumentException($"Sentence should follow format {FormatSentence}");

            var attendance = new Attendance(new Employee { Name = sentenceDetail[0].Trim() });

            var scheduleDetail = sentenceDetail[1].Split(',');

            foreach (var currentSchedule in scheduleDetail)
            {
                var schedule = currentSchedule.ParseScheduleByEmployee();

                if (schedule != null)
                {
                    attendance.AddSchedule(schedule);
                }
            }

            return attendance;
        }

        /// <summary>
        /// Function that parse the hours worked by an employee
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns>ScheduleDetail with the detail of Start and End time</returns>
        private static Schedule ParseScheduleByEmployee(this string sentence)
        {
            Schedule schedule;

            var length = sentence.Trim().Length;
            if (length >= FormatSchedule.Length)
            {
                var day = sentence.Substring(0, 2);
                short dayOfWeek;
                try
                {
                    dayOfWeek = Days[day];
                }
                catch (Exception exception)
                {
                    throw new ArgumentException("Day of Week format invalid. Valid Values: {MO|TU|WE|TH|FR|SA|SU}", exception);
                }
                var hoursSchedule = sentence.Substring(2);

                var listHours = hoursSchedule.Split('-');
                if (listHours.Length == 2)
                {
                    var startDate = TimeSpan.ParseExact(listHours[0].Trim(), "h\\:mm", CultureInfo.InvariantCulture);
                    var endDate = TimeSpan.ParseExact(listHours[1].Trim(), "h\\:mm", CultureInfo.InvariantCulture);

                    if (startDate > endDate)
                        throw new ArgumentException("End date should be greater than Start Date");

                    schedule = new Schedule { ScheduleOfDay = sentence, DayOfWeek = dayOfWeek, StartTime = startDate, EndTime = endDate };
                }
                else
                {
                    throw new ArgumentException("Incomplete hours");
                }
            }
            else
            {
                throw new ArgumentException("Invalid format schedule");
            }

            return schedule;
        }
    }
}
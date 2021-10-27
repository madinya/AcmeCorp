using System;
using System.Runtime.InteropServices;

namespace AcmeCorp.Domain
{
    public class Schedule
    {
        public string ScheduleOfDay { get; set; }
        public short DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

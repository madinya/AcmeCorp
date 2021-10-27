using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorp.Domain
{
    /// <summary>
    /// Class on charge of process all the information about the employees
    /// </summary>
    public static class DataProcess
    {
        public static List<KeyValuePair<string, int>> ProcessCommonSchedule(List<Attendance> attendances)
        {
            var ConcurrentEmployees = new List<KeyValuePair<string, int>>();
            var ProcessedPair = new List<string>();

            for (var i = 0; i < attendances.Count() -1; i++)
            {
                var attendanceFirst = attendances[i];

                for (var j = 0; j < attendances.Count(); j++)
                {
                    if (i == j)
                        continue;
                    
                    var commonSchedule = 0;
                    var attendanceSecond = attendances[j];

                    var pairName = $"{attendanceFirst.Name} - {attendanceSecond.Name}";
                    ProcessedPair.Add(pairName);

                    if (VerifyIfAlreadyProcessed(ProcessedPair,  attendanceFirst, attendanceSecond))
                        continue;  // Makes no sense to check RENE-ANDRES | ANDRES-RENE

                    var scheduleFirst = attendanceFirst.ScheduleDetail;
                    var scheduleSecond = attendanceSecond.ScheduleDetail;
                    
                    foreach (var scheduleFirstDet in scheduleFirst)
                    {
                        foreach (var scheduleSecondDet in scheduleSecond)
                        {
                            if (!CompareSchedules(scheduleFirstDet, scheduleSecondDet)) continue;
                            commonSchedule++;
                            break;
                        }
                    }

                    if (commonSchedule > 0)
                        ConcurrentEmployees.Add(new KeyValuePair<string, int>(pairName, commonSchedule));
                }
            }
            return ConcurrentEmployees;
        }

        private static bool VerifyIfAlreadyProcessed(List<string> ProcessedPair,  Attendance attendanceFirst, Attendance attendanceSecond)
        {
            return ProcessedPair.Exists(key => key == $"{attendanceSecond.Name} - {attendanceFirst.Name}");
        }

        private static bool CompareSchedules(Schedule first, Schedule second)
        {
            // return first.DayOfWeek == second.DayOfWeek && (first.StartTime == second.StartTime && first.EndTime == second.EndTime); // Fixed Time
             return first.DayOfWeek == second.DayOfWeek && (first.StartTime < second.EndTime && second.StartTime < first.EndTime); // Overlapped Time
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorp.Domain
{
    public class Attendance
    {
        private readonly Employee _employee;
        private readonly List<Schedule> _scheduleDetail;

        public Attendance(Employee employee)
        {
            _employee = employee;
            _scheduleDetail = new List<Schedule>();
        }

        public void AddSchedule(Schedule schedule)
        {
            _scheduleDetail.Add(schedule);
        }

        public string Name => _employee.Name;

        public List<Schedule> ScheduleDetail => _scheduleDetail;
    }
}
using System;
using System.Collections.Generic;
using AcmeCorp.Domain;
namespace AcmeCorp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Reading the file input.txt");
                // Read File from output directory
                var stringArray = DataReaderIo.ReadFileScheduleTime("input.txt");

                Console.WriteLine("Read finished. Processing the data");
                // Define attendance list
                var attendanceList = new List<Attendance>();

                // Process the result with the DataParser 
                foreach (var item in stringArray)
                {
                    var attendanceDetail = item.ParseSentenceEmployee();

                    if (attendanceDetail != null)
                        attendanceList.Add(attendanceDetail);
                }
                // Once we get all the list parsed process the data to get the result
                var result = DataProcess.ProcessCommonSchedule(attendanceList);

                Console.WriteLine("------------------------------");
                Console.WriteLine("--Employees at the same time--");
                Console.WriteLine("------------------------------");

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
                Console.WriteLine("--------- That's it. ---------");

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

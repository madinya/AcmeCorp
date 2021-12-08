using System;
using System.Collections.Generic;
using System.IO;
using AcmeCorp.Domain;

namespace AcmeCorp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Reading the file input.txt \n\n");
                // Read File from output directory
                IDataReader datareader = new DataReaderIo("input.txt");
                var stringArray = datareader.Read();

                Console.WriteLine("Read finished. Processing the data \n\n");
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

                Console.WriteLine("\n--------- That's it. ---------\n");
                Console.WriteLine("\n---------  SUCCESS   ---------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION! Something wrong has happened. Error message: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
        }
    }
}
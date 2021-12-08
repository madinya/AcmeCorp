# AcmeCorp
Acme solution ~ by MaD

This project it's about processing an input file with a specific formatted data and getting the result of the common time the employees have been with.

The DataReaderIo reads the input file, the result is a string array that is processed by the DataParser class who decodes or notify any error in the format for each line, if there is no error it maps into objects of the business logic class (Attendance, Employee and Schedule) with these classes the DataProcess makes the magic processing the data and generates a key paired value with the results of the employees that has been at the same time that can be consummed by any UI to get the output. 

By default is only taking the times that matches (start time and end time) but there is an option commented to stablish if the times are overlapped. 


## Development 
The solution is made in C# with Visual Studio 2019 .NET Core 3.1. The solution contains three main projects.

  ## 1. AcmeCorp.Domain: 
  
  This class library project contains all the logic and classes used.
    
  - Employee.cs : Class with the Employee's information.
  - Schedule.cs : Class with the detail the Employee's working day start and end working time,.
  - Attendance.cs: Class to store and process the schedule's details for each employee.
  
  - DataReaderIo.cs:  Class to read the specified file (input.txt).
  - DataParser.cs: Class to process the format specified and mapping into the Attendance class.
  - DataProcess.cs: Class to get the common time the employees were on the same frame time.

  ## 2. AcmeCorp.Test: 
  NUnit test project detail all the test made to validate most of the scenarios to inspect and handle the possible errors.
  
  ## 3. AcmeCorp.UI: 
  Console application that reads the (input.txt) file in the output folder and proccess the common time the employees have coincided. The (input.txt) file is set to be copied to the output folder. 
  
  - Program.cs: Main method.
  - input.txt: File with the data to be processed
    
 ## Build 
 
  1.  Clone repository 

    $ git clone --recursive https://github.com/madinya/AcmeCorp.git

  2. Open the repository in VS2019 and build the solution.
  3. Set AcmeCorp.UI as start project.
  4. Run!
  
    

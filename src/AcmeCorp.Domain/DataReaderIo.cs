using System.IO;

namespace AcmeCorp.Domain
{
    /// <summary>
    /// Class on charge to read from a received path the file to process
    /// </summary>
    public static class DataReaderIo
    {
        public static string[] ReadFileScheduleTime(string _path)
        {
            if (!File.Exists(_path))
                throw new FileNotFoundException($"File not found in path {_path}, please check the file exists or you have access to it!");

            return File.ReadAllLines(_path);
        }
    }
}
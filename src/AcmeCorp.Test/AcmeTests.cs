using System;
using System.Collections.Generic;
using System.IO;
using AcmeCorp.Domain;
using NUnit.Framework;

namespace AcmeCorp.Test
{
    public class AcmeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInputReceived()
        {
            var rene = "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00".ParseSentenceEmployee();
            var amira = "ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00".ParseSentenceEmployee();
            var andres = "ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00".ParseSentenceEmployee();

            Assert.Pass();
        }

        [Test]
        public void InputBadFormatDividerNameSchedule()
        {
            Assert.Throws<ArgumentException>(() => "RENE:MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00".ParseSentenceEmployee());
        }

        [Test]
        public void InputStartDateGreaterThanEndDate()
        {
            Assert.Throws<ArgumentException>(() => "RENE=MO14:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00".ParseSentenceEmployee());
        }

        [Test]
        public void InputBadFormatInvalidTime()
        {
            Assert.Throws<OverflowException>(() => "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-25:00,SA14:00-18:00,SU20:00-23:00".ParseSentenceEmployee());
        }

        [Test]
        public void InputBadFormatInvalidFormatHour()
        {
            Assert.Throws<FormatException>(() => "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-xx:xx".ParseSentenceEmployee());
        }

        [Test]
        public void InputBadFormatInvalidMissingHour()
        {
            Assert.Throws<ArgumentException>(() => "RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU04:00-5:20".ParseSentenceEmployee());
        }

        [Test]
        public void InputBadFormatInvalidDay()
        {
            Assert.Throws<ArgumentException>(() => "RENE=LU14:00-12:00,MA10:00-12:00,MI01:00-03:00,SA14:00-18:00,DO20:00-".ParseSentenceEmployee());
        }

        [Test]
        public void InputFileExists()
        {
            var sentences = DataReaderIo.ReadFileScheduleTime(@"E:\input.txt");

            var attendances = new List<Attendance>();
            foreach (var sentence in sentences)
            {
                try
                {
                    var attendance = sentence.ParseSentenceEmployee();
                    if (attendance != null)
                        attendances.Add(attendance);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            var result = DataProcess.ProcessCommonSchedule(attendances);

            if (result.Count > 0)
                Assert.Pass();
        }

        [Test]
        public void InputFileDoesNotExists()
        {
            Assert.Throws<FileNotFoundException>(() => DataReaderIo.ReadFileScheduleTime(@"E\wsus.txt"));
        }
    }
}
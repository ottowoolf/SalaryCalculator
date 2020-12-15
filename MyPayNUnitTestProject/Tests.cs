using System;
using MyPayProject;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;


namespace MyPayNUnitTestProject
{
    public class Tests
    {
        private List<PayRecord> _records;
        double[] hours;
        double[] rates;
        public static string GetInputPath()
        {
            string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "Import", "employee-payroll-data.csv");
            return Path.GetFullPath(inputFile);
        }
        public static string GetPathOutput()
        {
            string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "Export", "record.csv");
            return Path.GetFullPath(outputFolder);
        }

        [SetUp]
        public void Setup()
        {
            hours = new double[] { 2, 3, 3, 4, 5, 6 };
            rates = new double[] { 25, 25, 25, 25, 32, 32 };
            _records = CsvImporter.ImportPayRecords(GetInputPath());

        }
        [Test]
        public void TestImport()
        {
            Assert.IsNotNull(_records);
            int expectedObjectNumber = 5;
            Assert.AreEqual(expectedObjectNumber, _records.Count);
        }

        [Test]
        public void TestGross()
        {
            ResidentPayRecord p = new ResidentPayRecord(1, hours, rates);
            double ExpectedGross = 652.00;
            double ActualGross = p.Gross;
            Assert.AreEqual(ExpectedGross, ActualGross);
        }

        [Test]
        public void TestTax()
        {
            ResidentPayRecord p = new ResidentPayRecord(1, hours, rates);
            double ActualTax = p.Tax;
            double ExpectedTax = TaxCalculator.CalculateResidentialTax(p.Gross);
            Assert.AreEqual(ExpectedTax, ActualTax);
        }

        [Test]
        public void TestNet()
        {
            ResidentPayRecord p = new ResidentPayRecord(1, hours, rates);
            double ExpectedNet = p.Gross - p.Tax;
            double ActualNet = p.Net;
            Assert.AreEqual(ActualNet, ExpectedNet);
        }

        [Test]
        public void TestExport()
        {
            static string GetPathOutput()
            {
                string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "Export", "record.csv");
                return Path.GetFullPath(outputFolder);
            }
            PayRecordWriter.Write(GetPathOutput(), _records, true);
            FileAssert.Exists(GetPathOutput());
        }
    }
}

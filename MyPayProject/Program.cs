using System;
using System.Collections.Generic;
using System.IO;

namespace MyPayProject
{
    class Program
    {
        public static void Main(string[] args)
        {
            // List where we import the data in the csv file
            List<PayRecord> payRecords = CsvImporter.ImportPayRecords(GetInputPath());

            // Variable storing datetime value to be used as part of export file name
            DateTime dateTimeNow = DateTime.UtcNow;

            // String containing full path of output folder + name of the file to be created
            string fileExportPath = Path.Combine(GetPathOutput(), dateTimeNow.Ticks + "-records.csv");

            // Calling the Write() method to create a new file and write the data into it
            PayRecordWriter.Write(fileExportPath, payRecords, true);
        }

        // Method that returns path to the input folder 
        public static string GetInputPath()
        {
            string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "Import", "employee-payroll-data.csv");
            return Path.GetFullPath(inputFile);
        }

        // Method that returns path to the output folder 
        public static string GetPathOutput()
        {
            string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "Export");
            return Path.GetFullPath(outputFolder);
        }
    }
}

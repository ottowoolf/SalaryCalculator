using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace MyPayProject
{
    /// <summary>
    /// Class with Method to write to a csv file using CsvHelper
    /// </summary>
    public static class PayRecordWriter
    {
        /// <summary>
        /// Method that creates a csv file and prints the imported data to that csv file and optionally print the results to the console
        /// </summary>
        /// <param name="file"></param>
        /// <param name="records"></param>
        /// <param name="writeToConsole"></param>
        public static void Write(string file, List<PayRecord> records, bool writeToConsole = false)
        {
            //using CsvHelper to create a file and write into it
            using (var writer = new StreamWriter(file))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Using Round-Trip formatting to format the values
                csv.Configuration.TypeConverterOptionsCache.GetOptions<double>().Formats = new[] { "F" };

                csv.WriteRecords(records);
            }
            // Conditional to call the appropriate GetDetails() method depending on the PayRecord
            // (Optional) Only prints if the Write method is given a 3rd parameter of true           
            if (writeToConsole)
            {
                foreach (var record in records)
                {
                    Console.WriteLine(record.GetDetails());
                }
            }
        }
    }
}

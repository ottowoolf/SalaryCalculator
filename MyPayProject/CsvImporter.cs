using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;

namespace MyPayProject
{
    /// <summary>
    /// Class with methods to import data from a csv file
    /// </summary>
    public class CsvImporter
    {
        /// <summary>
        /// Method will check if there is a dictionary entry
        /// If not, will create a new entry and store data
        /// If there isand entry, it will find the entry and update its array
        /// </summary>
        /// <param name="shifts"></param>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        public static void AddOrUpdateHours(Dictionary<int, List<double>> shifts, int id, double hours)
        {
            
    
            List<double> hoursList;
            if (shifts.TryGetValue(id, out hoursList))
            {
                // Here we update an existing entry in the dictionary
                hoursList.Add(hours);
            }
            else
            {
                // Here we create a new entry in the dictionary
                hoursList = new List<double>();
                hoursList.Add(hours);
                shifts.Add(id, hoursList);
            }
        }
        /// <summary>
        /// Method will check if there is a dictionary entry
        /// If not, will create a new entry and store data
        /// If there isand entry, it will find the entry and update its array
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        public static void AddOrUpdateRates(Dictionary<int, List<double>> rates, int id, double rate)
        {
            List<double> ratesList;
            if (rates.TryGetValue(id, out ratesList))
            {
                // Here we update an existing entry in the dictionary
                ratesList.Add(rate);
            }
            else
            {
                // Here we create a new entry in the dictionary
                ratesList = new List<double>();
                ratesList.Add(rate);
                rates.Add(id, ratesList);
            }
        }
        /// <summary>
        /// Method that creates and returns the appropriate PayRecord 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        /// <param name="visa"></param>
        /// <param name="yearToDate"></param>
        /// <returns></returns>
        public static PayRecord CreatePayRecord(int id, double[] hours, double[] rates, string visa, string yearToDate)
        {
            // if visa or yearToDate are an empty string, an instance of ResidentPayRecord will be created

            if (visa == "" || yearToDate == "")
            {
                ResidentPayRecord residentRecord = new ResidentPayRecord(id, hours, rates);
                return residentRecord;

            }
            // If visa and yearToDate have values, an instance of WorkingHolidayPayRecord will be created
            else
            {
                int.TryParse(visa, out int visaNumber);
                int.TryParse(yearToDate, out int yearToDateNumber);

                WorkingHolidayPayRecord workingHolidayrecord = new WorkingHolidayPayRecord(id, hours, rates, visaNumber, yearToDateNumber);
                return workingHolidayrecord;
            }
        }
        /// <summary>
        /// Method that imports records from a csv file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<PayRecord> ImportPayRecords(string file)
        {
            // List to store Payrecords
            List<PayRecord> payRecords = new List<PayRecord>();
            Dictionary<int, List<double>> shifts = new Dictionary<int, List<double>>();
            Dictionary<int, List<double>> rates = new Dictionary<int, List<double>>();
            Dictionary<int, string> visas = new Dictionary<int, string>();
            Dictionary<int, string> yearToDates = new Dictionary<int, string>();
            // Reading and iterating through the csv file with CsvHelper
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();


                // reading the first row to skip it since ReadHeader method throws error
                csv.Read();
                // Iterating through the csv file  and assigning values of the fields by index
                while (csv.Read())
                    {
                    string idtext = csv.GetField(0);
                    string hoursText = csv.GetField(1);
                    string rateText = csv.GetField(2);
                    string visaText = csv.GetField(3);
                    string yearToDateText = csv.GetField(4);
                    // convert text to appropriate data types to be assigned to lists
                    int id = int.Parse(idtext); 
                    double hours = double.Parse(hoursText);
                    double rate = double.Parse(rateText);
                    // Adding visa and yearToDate to Dictionary
                    visas.TryAdd(id, visaText);
                    yearToDates.TryAdd(id, yearToDateText);
                    // Adding hours and rates to Dictionary by using helper methods
                    AddOrUpdateHours(shifts, id, hours);
                    AddOrUpdateRates(rates, id, rate);
                }   
            }

            foreach (int id in shifts.Keys)
            {
                string visa = visas[id];
                string yearToDate = yearToDates[id];

            // Converting hours and rates lists to arrays to be assigned to the array parameters
                double[] arrayOfShifts = shifts[id].ToArray();
                double[] arrayOfRates = rates[id].ToArray();

                payRecords.Add(CreatePayRecord(id, arrayOfShifts, arrayOfRates, visa, yearToDate));
            }
            return payRecords;
        }       
    }
}

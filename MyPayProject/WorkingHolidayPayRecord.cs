using System;

namespace MyPayProject
{
    /// <summary>
    /// Class that inherits from PayRecord
    /// </summary>
    public class WorkingHolidayPayRecord : PayRecord
    {
        /// <summary>
        /// Property that stores the visa input
        /// </summary>
        public int Visa { get; private set; }
        /// <summary>
        /// Property that stores the Year to Date input
        /// </summary>
        public int YearToDate { get; private set; }


        /// <summary>
        /// Constructor for the WorkingHoliday class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        /// <param name="visa"></param>
        /// <param name="yearToDate"></param>
        public WorkingHolidayPayRecord(int id, double[] hours, double[] rates, int visa, int yearToDate) : base(id, hours, rates)
        {
            Visa = visa;
            YearToDate = yearToDate;
        }
        /// <summary>
        /// Override the GetDetails method so it behaves differently and displays the appropriate values
        /// </summary>
        /// <returns> Id,Gross,Net,Tax,Visa and YearToDate</returns>
        public override string GetDetails()
        {
            return $"-------- EMPLOYEE: {Id} --------\nGROSS:\t${Gross:0,0.00}\nNet:\t${Net:0,0.00}\nTAX:\t${Tax:0,0.00}\nVisa:\t{Visa}\nYTD:\t${YearToDate:0,0.00}";
        }
        /// <summary>
        /// Override the Tax property to calclate the appropriate tax amount
        /// </summary>
        public override double Tax { 
            get
            {
                return TaxCalculator.CalculateWorkingHolidayTax(Gross, YearToDate);
            }
        }

    }
}

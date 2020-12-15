using System;


namespace MyPayProject
{
    /// <summary>
    /// Class with methods to calculate tax
    /// </summary>
    public class TaxCalculator
    {
        /// <summary>
        /// Checks the gross value and assigns the appropriate coefficient values,
        /// Calculates residential tax by multiplying coefficient value A by gross and subtracting coefficient value B from it.
        /// </summary>
        /// <param name="gross"></param>
        /// <returns>The tax amount</returns>
        public static double CalculateResidentialTax(double gross)
        {
            double tax = 0;

            double[] a = { 0.19, 0.2342, 0.3477, 0.345, 0.39, 0.47 };
            double[] b = { 0.19, 3.213, 44.2476, 41.7311, 103.8657, 352.7888 };

            if (gross > -1 && gross <= 72)
            {

                tax = (a[0] * gross) - b[0];
            }
            else if (gross > 72 && gross <= 361)
            {
                tax = (a[1] * gross) - b[1];
            }
            else if (gross > 361 && gross <= 932)
            {
                tax = (a[2] * gross) - b[2];
            }
            else if (gross > 932 && gross <= 1380)
            {
                tax = (a[3] * gross) - b[3];
            }
            else if (gross > 1380 && gross <= 3111)
            {
                tax = (a[4] * gross) - b[4];
            }
            else if (gross > 3111 && gross <= 999999)
            {
                tax = (a[5] * gross) - b[5];
            }
            return tax;
        }
        
        /// <summary>
        /// Checks the totalGross by adding yearToDate to gross, Checks in what range is totalGross and assigns the appropriate rate value.
        /// calculates tax by multiplying tax by rate.
        /// </summary>
        /// <param name="gross"></param>
        /// <param name="yearToDate"></param>
        /// <returns>The tax amount</returns>
        public static double CalculateWorkingHolidayTax(double gross, double yearToDate)
        {
            double rate = 0;
            double tax;
            double totalGross = yearToDate + gross;

            if (totalGross > -1 && totalGross <= 3700)
            {
                rate = 0.15;

            }
            else if (totalGross > 37000 && totalGross <= 90000)
            {
                rate = 0.32;

            }
            else if (totalGross > 90000 && totalGross <= 180000)
            {
                rate = 0.37;

            }
            else if (totalGross > 180000 && totalGross <= 9999999)
            {
                rate = 0.45;
                
            }
            tax = gross * rate;
            return tax;
        }
    }
}

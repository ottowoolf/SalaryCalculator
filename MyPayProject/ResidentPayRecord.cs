using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyPayProject
{
    /// <summary>
    ///Class that inherits from PayRecord
    /// </summary>
    public class ResidentPayRecord : PayRecord
    {

        /// <summary>
        /// Constructor for the ResidentPayRecord class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        public ResidentPayRecord(int id, double[] hours, double[] rates)
             : base(id, hours, rates)
        {
        }
        /// <summary>
        /// Override the Tax property to calclate the appropriate tax amount
        /// </summary>
        public override double Tax
        {
            get
            {
                return TaxCalculator.CalculateResidentialTax(Gross);
            }
        }
    }
}
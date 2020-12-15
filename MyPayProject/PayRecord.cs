using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPayProject
{
    /// <summary>
    /// Abstract class to be inherited from with fields properties and a method
    /// </summary>
    public abstract class PayRecord
    {
        /// <summary>
        /// Array with hours values
        /// </summary>
        protected double[] _hours;
        /// <summary>
        /// Array with rates values
        /// </summary>
        protected double[] _rates;
        /// <summary>
        /// Employee Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Gross value property calculated by multilying the hours by the rate
        /// </summary>
        public double Gross 
        {
            get
            {
                
                double gross = 0;
                for (int i = 0; i < _hours.Length; i++)
                {
                    gross += _hours[i] * _rates[i];
                }
                return gross;
            }
            
        }
        /// <summary>
        /// Abstract property to be inherited from for calculating tax
        /// </summary>
        public abstract double Tax {get;}
        /// <summary>
        /// Net value property calculated by subtracting Tax from Gross
        /// </summary>
        public double Net 
        {
            get
            {
                return Gross - Tax;
            }
        }
        /// <summary>
        /// Constructor for the PayRecord class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hours"></param>
        /// <param name="rates"></param>
        public PayRecord(int id, double[] hours, double[] rates)
        {
            _hours = hours;
            _rates = rates;
            Id = id;
        }
        /// <summary>
        /// Method that prints details of a record
        /// </summary>
        /// <returns>Id,Gross,Tax and Net</returns>
        public virtual string GetDetails()
        {
            return $"-------- EMPLOYEE: {Id} --------\nGROSS:\t${Gross:0,0.00}\nTAX:\t${Tax:0,0.00}\nNet:\t${Net:0,0.00}";
        }
    }
}

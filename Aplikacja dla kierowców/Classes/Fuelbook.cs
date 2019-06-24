using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Class responsible for storing informations about Fuelbook-type objects, further refered to as "fuel note" or "fuel"
    /// </summary>
    public class Fuelbook
    {
        /// <summary>
        /// Indexer logistic value, handled by EF6 storage compartment
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Variable meant for storing plate number of a fuel note-related car
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// Variable meant for storing a description of a fuel note
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Variable meant for storing the cost of fuel 
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Variable meant for storing price of a fuel
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Variable meant for storing amount of litres refilled
        /// </summary>
        public decimal Litres { get; set; }
        /// <summary>
        /// Variable meant for storing distance driven since last refill
        /// </summary>
        public decimal Distance { get; set; }
        /// <summary>
        /// Variable meant for storing average consumption since last refill
        /// </summary>
        public decimal Consumption { get; set; }
        /// <summary>
        /// Variable meant for storing approximate distance driven in built-up area
        /// </summary>
        public decimal Area_City { get; set; }
        /// <summary>
        /// Variable meant for storing approximate distance driven in the open
        /// </summary>
        public decimal Area_Open { get; set; }

        /// <summary>
        /// Fuelbook-type object constructor, creates an empty note
        /// </summary>
        public Fuelbook() { }
        /// <summary>
        /// Fuelbook-type object constructor, creates a note containing all vital info
        /// </summary>
        /// <param name="PlateNumber">Plate number of a fuel note-related car</param>
        /// <param name="Description">Description of a fuel note</param>
        /// <param name="Cost">Cost of fuel</param>
        /// <param name="Price">Price of fuel</param>
        /// <param name="Litres">Litres refilled</param>
        /// <param name="Distance">Distance since last refill</param>
        /// <param name="Consumption">Average consumption since last refill</param>
        /// <param name="Area_City">Percentage of distance driven in cities</param>
        /// <param name="Area_Open">Percentage of distance driven in the open</param>
        public Fuelbook( string PlateNumber, string Description, decimal Cost, decimal Price, decimal Litres, decimal Distance, decimal Consumption, decimal Area_City, decimal Area_Open)
        {
            this.PlateNumber = PlateNumber;
            this.Description = Description;
            this.Cost = Cost;
            this.Price = Price;
            this.Litres = Litres;
            this.Distance = Distance;
            this.Consumption = Consumption;
            this.Area_City = Area_City;
            this.Area_Open = Area_Open;
        }
        /// <summary>
        /// Save object to the EF6 database
        /// </summary>
        /// <param name="DB">EF6 database reference (if valid)</param>
        public void SaveToDb(DbReference DB)
        {
            if (string.IsNullOrWhiteSpace(PlateNumber))
                throw new ArgumentException("Fuelbook PlateNumber is null or white space");
            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Fuelbook Description is nul or empty");
            if (Cost <= 0)
                throw new ArgumentOutOfRangeException("Fuelbook Cost equals zero or less");
            if (Price <= 0)
                throw new ArgumentOutOfRangeException("Fuelbook Price equals zero or less");
            if (Litres <= 0)
                throw new ArgumentOutOfRangeException("Fuelbook Litres equals zero or less");
            if (Distance <= 0)
                throw new ArgumentOutOfRangeException("Fuelbook Distance equals zero or less");
            if (Consumption <= 0)
                throw new ArgumentOutOfRangeException("Fuelbook Consumption equals zero or less");
            if (Area_City < 0 || Area_City > 100)
                throw new ArgumentOutOfRangeException("Fuelbook Area_City not in <0,100> range");
            if (Area_Open < 0 || Area_Open > 100)
                throw new ArgumentOutOfRangeException("Fuelbook Area_Open not in <0,100> range");
            else
            {
                DB.FuelDb.Add(this);
                DB.SaveChanges();
            }
        }
    }
}

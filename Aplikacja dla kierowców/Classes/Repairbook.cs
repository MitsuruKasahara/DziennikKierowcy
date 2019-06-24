using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Class responsible for storing informations about Repairbook-type objects, further refered to as "repair note" or "repair"
    /// </summary>
    public class Repairbook
    {
        /// <summary>
        /// Indexer logistic value, handled by EF6 storage compartment
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Variable meant for storing plate number of a repair note-related car
        /// </summary>
        public string Plate { get; set; }
        /// <summary>
        /// Variable meant for storing description of a repair note
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Variable meant for storing price of repair
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Boolean value indicating if the repair is still to be done
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Repairbook-type object constructor, creates an empty note
        /// </summary>
        public Repairbook() { }
        /// <summary>
        /// Repairbook-type object constructor, creates a repair note containing all vital info
        /// </summary>
        /// <param name="Plate">Plate number of a repair note-related car</param>
        /// <param name="Price">Price of repairs</param>
        /// <param name="Text">Desciption of a repair note</param>
        /// <param name="Active">Indicates if the repair is still to be carried out</param>
        public Repairbook(string Plate, decimal Price, string Text, bool Active)
        {
            this.Plate = Plate;
            this.Text = Text;
            this.Price = Price;
            this.Active = Active;
        }
        /// <summary>
        /// Save object to the EF6 database
        /// </summary>
        /// <param name="DB">EF6 database reference (if valid)</param>
        public void SaveToDb(DbReference DB)
        {
            if (string.IsNullOrWhiteSpace(Plate))
                throw new ArgumentException("Repairbook Plate is null or white space");
            if (string.IsNullOrWhiteSpace(Text))
                throw new ArgumentException("Repairbook Text is null or white sapce");
            if (Price < 0)
                throw new ArgumentOutOfRangeException("Repairbook Price equals zero or less");
            else
            {
                DB.RepairDb.Add(this);
                DB.SaveChanges();
            }
        }
    }
}
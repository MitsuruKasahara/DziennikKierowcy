using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Class responsible for storing informations about Notebook-type objects, further refered to as "note"
    /// </summary>
    public class Notebook
    {
        /// <summary>
        /// Indexer logistic value, handled by EF6 storage compartment
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Variable meant for storing plate number of a note-related car
        /// </summary>
        public string Plate { get; set; }
        /// <summary>
        /// Variable meant for storing content of a note
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Notebook-type object constructor, creates an empty note
        /// </summary>
        public Notebook() { }
        /// <summary>
        /// Notebook-type object constructor, creates a note with specified plate number and content
        /// </summary>
        /// <param name="CarPlate">Plate number of a note-related car</param>
        /// <param name="Content">Content of a note</param>
        public Notebook(string CarPlate, string Content)
        {
            Plate = CarPlate;
            Text = Content;
        }
        /// <summary>
        /// Save object to the EF6 database
        /// </summary>
        /// <param name="DB">EF6 database reference (if valid)</param>
        public void SaveToDb(DbReference DB)
        {
            if (string.IsNullOrWhiteSpace(Plate))
                throw new ArgumentException("Notebook Plate is empty or white space");
            if (string.IsNullOrWhiteSpace(Text))
                throw new ArgumentException("Notebook Text is empty or white space");
            else
            {
                DB.NotebookDb.Add(this);
                DB.SaveChanges();
            }
        }
    }
}

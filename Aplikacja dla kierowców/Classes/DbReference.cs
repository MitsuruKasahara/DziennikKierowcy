using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Centralized database-class of the application
    /// </summary>
    public class DbReference: DbContext
    {
        /// <summary>
        /// EF6 database initializer
        /// </summary>
        public DbReference(): base("DB node 0")
            {
            Database.Initialize(false);
            }

        /// <summary>
        /// DbSet containing Car-type objects, further refered to as "CarDb"
        /// </summary>
        public DbSet<Car> CarDb { get; set; }
        /// <summary>
        /// DbSet containing Notebook-type objects, further refered to as "NotebookDb"
        /// </summary>
        public DbSet<Notebook> NotebookDb { get; set; }
        /// <summary>
        /// DbSet containing Repairbook-type objects, further refered to as "RepairbookDb"
        /// </summary>
        public DbSet<Repairbook> RepairDb { get; set; }
        /// <summary>
        /// DbSet containing Fuelbook-type objects, further refered to as "FuelbookDb"
        /// </summary>
        public DbSet<Fuelbook> FuelDb { get; set; }

        /// <summary>
        /// Checks if the given plate number already exisits in the CarDb
        /// </summary>
        /// <param name="plate">Plate number of a car</param>
        /// <returns>True if found, false if no correlation</returns>
        public bool CheckPlate(string plate)
        {    
            foreach (var it in CarDb)
            {
                if (plate == it.PlateNumber)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the car with the given Platenumber-Password credentials exists in the CarDb
        /// </summary>
        /// <param name="Plate">Car's plate number</param>
        /// <param name="Password">Car owner's password</param>
        /// <returns>0 if no plate number and no password found, 1 if either plate number OR password found, 2 if plate number AND password found</returns>
        public int CheckPlateCredential(string Plate, string Password)
        {
            foreach (var it in CarDb)
            {
                if (Plate == it.PlateNumber && Password == it.Password)
                    return 2;
                if (Plate == it.PlateNumber || Password == it.Password)
                    return 1;
            }
            return 0;
        }
        /// <summary>
        /// Checks if the car with the given Username-Password credentials exists in the CarDb
        /// </summary>
        /// <param name="DisplayName">User's username</param>
        /// <param name="Password">User's password</param>
        /// <returns>0 if no username and no password found, 1 if either username OR password found, 2 if both username AND password found</returns>
        public int CheckCredentials(string DisplayName, string Password)
        {
            foreach (var it in CarDb)
            {
                if (DisplayName == it.DisplayName && Password == it.Password)
                    return 2;
                if (DisplayName == it.DisplayName || Password == it.Password)
                    return 1;
            }
            return 0;
        }
        /// <summary>
        /// Looks for a car-type object with matching credentials in CarDb and returns it
        /// </summary>
        /// <param name="DisplayName">Car user's Username</param>
        /// <param name="Password">Car user's Password</param>
        /// <returns>Empty car-type object if no correlation found, valid car-type object if matching credentials found</returns>
        public Car ReturnByCredentials(string DisplayName, string Password)
        {
            Car NewCar = new Car();
            foreach (var it in CarDb)
            {
                if (DisplayName == it.DisplayName && Password == it.Password)
                    NewCar = it;
            }
            return NewCar;
        }
        /// <summary>
        /// Returns plate number of the last item in the CarDb collection
        /// </summary>
        public string GetLastPlateNumber()
        {
            string x = "" ;
            foreach(Car it in CarDb)
            {
                x = it.PlateNumber;
            }
            return x;
        }
        
        /// <summary>
        /// Removes all items from all DbSets
        /// </summary>
        public void ClearCollection()
        {
            foreach(Car it in CarDb)
                CarDb.Remove(it);
            foreach (var it in NotebookDb)
                NotebookDb.Remove(it);
            foreach (var it in RepairDb)
                RepairDb.Remove(it);
            foreach (var it in FuelDb)
                FuelDb.Remove(it);
                
            this.SaveChanges();
        }
        /// <summary>
        /// Removes a Car-type object with matching plate number from CarDb 
        /// </summary>
        /// <param name="Plate">Plate number of a searched car</param>
        public void RemoveByPlate(string Plate)
        {
            foreach (Car it in CarDb)
                if (it.PlateNumber == Plate)
                    CarDb.Remove(it);
            this.SaveChanges();
        }
        /// <summary>
        /// Removes objects of all types with matching plate number value from all DbSets
        /// </summary>
        /// <param name="Plate">Plate number to be cleared</param>
        public void RemoveAllByPlate(string Plate)
        {
            foreach (Car Car in CarDb)
                if (Car.PlateNumber == Plate)
                {
                    foreach (var it in FuelDb)
                    {
                        if (it.PlateNumber == Plate)
                            FuelDb.Remove(it);
                    }
                    foreach (var it in RepairDb)
                    {
                        if (it.Plate == Plate)
                            RepairDb.Remove(it);
                    }
                    foreach (var it in NotebookDb)
                    {
                        if (it.Plate == Plate)
                            NotebookDb.Remove(it);
                    }
                    CarDb.Remove(Car);
                }
            this.SaveChanges();
        }
        /// <summary>
        /// Returns the list of all items in the CarDb
        /// </summary>
        /// <returns>String containing list of all objects in CarDb</returns>
        public string ViewCollection()
        {
            string x = "";
            foreach(Car it in CarDb)
            {
                x += $"{it.DisplayName}: {it.PlateNumber} {it.Year} {it.Manufacturer} {it.Model} {it.FuelType}; {it.FuelCapacity}l, VIN {it.VinNumber}\n";
            }
            return x;
        }
        /// <summary>
        /// Removes all items with username marked as Generic from CarDb
        /// </summary>
        public void RemoveGenerics()
        {
            foreach (Car it in CarDb)
            {
                if (it.DisplayName == "Generic")
                    CarDb.Remove(it);
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Removes all items with plate number marked as Generic from CarDb
        /// </summary>
        public void RemoveIncomplete()
        {
            foreach(Car it in CarDb)
            {
                if (it.PlateNumber == "Generic")
                    CarDb.Remove(it);
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches CarDb for an object with matching plate number and returns it
        /// </summary>
        /// <param name="Plate">Searched plate number</param>
        /// <returns>Empty Car-type object if no correlation found, valid car if matching plate number found</returns>
        public Car ReturnByPlateNumber(string Plate)
        {
            Car newCar = new Car();
            foreach (Car it in CarDb)
            {
                if(it.PlateNumber==Plate)
                {
                    newCar = it;
                }
            }
            return newCar;
        }
        
        /// <summary>
        /// Returns last entry in CarDb
        /// </summary>
        /// <returns>Last entry in CarDb</returns>
        public Car ReturnLastEntry()
        {
            Car _car = new Car();
            foreach(Car it in CarDb)
            {
                _car = it;//new Car(it);
            }
            return _car;
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces it with new one
        /// </summary>
        /// <param name="Plate">Seeked plate number to be replaced</param>
        /// <param name="NewItem">New plate number</param>
        public void ChangePlateNumber(string Plate, string NewItem)
        {
            foreach(Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.PlateNumber = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces owner's username with a new username
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New username</param>
        public void ChangeDisplayName(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.DisplayName = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces fuel type with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New fuel type</param>
        public void ChangeFuelType(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.FuelType = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces fuel capacity with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New fuel capacity</param>
        public void ChangeFuelCapacity(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.FuelCapacity = Int32.Parse(NewItem);
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces manufacturer with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New manufacturer</param>
        public void ChangeManufacturer(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.Manufacturer = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces model with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New model</param>
        public void ChangeModel(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.Model = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces year of production with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate number</param>
        /// <param name="NewItem">New year of production</param>
        public void ChangeYear(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.Year = Int32.Parse(NewItem);
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for Car-type object with matching plate number and replaces VIN number with a new one
        /// </summary>
        /// <param name="Plate">Seeked car's plate numbe</param>
        /// <param name="NewItem">New VIN number</param>
        public void ChangeVinNumber(string Plate, string NewItem)
        {
            foreach (Car it in CarDb)
            {
                if (it.PlateNumber == Plate)
                    it.VinNumber = NewItem;
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Count all notes with matching plate number in NotebookDb
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <returns>Number of notes related to given plate number</returns>
        public int CountNotes(string Plate)
        {
            int tmp = 0;
            foreach(var it in NotebookDb)
            {
                if (it.Plate == Plate)
                    tmp++;
            }
            return tmp;
        }
        /// <summary>
        /// Searches for Notebook-type object with matching plate number and content and replaces content with new one
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <param name="OldNote">Content of note to be replaced</param>
        /// <param name="NewNote">New content</param>
        public void ChangeNote(string Plate, string OldNote, string NewNote)
        {
            foreach (var note in NotebookDb)
            {
                if(note.Plate==Plate && note.Text == OldNote)
                {
                    note.Text = NewNote;
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for Notebook-type object with matching plate number and content and removes it
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <param name="Note">Seekend content</param>
        public void RemoveNote(string Plate, string Note)
        {
            foreach (var note in NotebookDb)
            {
                if (note.Plate == Plate && note.Text == Note)
                {
                    NotebookDb.Remove(note);
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for the last Notebook-type object related to the given plate number are returns it
        /// </summary>
        /// <param name="Plate">Plate number related to the note</param>
        /// <returns>Last NotebookDb note related to the given plate number</returns>
        public string ReturnLastNote(string Plate)
        {
            Notebook Note = new Notebook();
            foreach (var note in NotebookDb)
            {
                if(note.Plate==Plate)
                    Note = note;
            }
            return Note.Text;
        }
        /// <summary>
        /// Searches for the Repairbook-type object with relating plate number, text and price and removes it
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <param name="Note">Seeked text</param>
        /// <param name="Price">Seeked price</param>
        public void RemoveRepairNote(string Plate, string Note, string Price)
        {
            foreach (var note in RepairDb)
            {
                if (note.Plate == Plate && note.Text == Note && note.Price.ToString() == Price )
                {
                    RepairDb.Remove(note);
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for the Repairbook-type object with relating plate number, text and price and replaces text and price
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <param name="OldNote">Seeked text</param>
        /// <param name="OldPrice">New text</param>
        /// <param name="NewNote">Seeked price</param>
        /// <param name="NewPrice">New price</param>
        public void ChangeRepairNote(string Plate, string OldNote, decimal OldPrice, string NewNote, decimal NewPrice)
        {
            foreach (var note in RepairDb)
            {
                if (note.Plate == Plate && note.Text == OldNote && note.Price == OldPrice)
                {
                    note.Text = NewNote;
                    note.Price = NewPrice;
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for the Repairbook-type object with relating plate number, text and price and sets it to inactive
        /// </summary>
        /// <param name="Plate">Seeked plate number</param>
        /// <param name="Note">Seeked text</param>
        /// <param name="Price">Seeked price</param>
        public void ActivateRepairNote(string Plate, string Note, string Price)
        {
            foreach (var note in RepairDb)
            {
                if (note.Plate == Plate && note.Text == Note && note.Price.ToString() == Price)
                {
                    note.Active = false;
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for the Car-type object with relating plate number and sets it to owner's favourite
        /// </summary>
        /// <param name="Plate"></param>
        public void ChangeFavouriteStatus(string Plate)
        {
            foreach(Car Car in CarDb)
            {
                if (Car.PlateNumber == Plate)
                {
                    if (Car.IsFavourite == true)
                        Car.IsFavourite = false;
                    else
                        Car.IsFavourite = true;
                }
            }
            this.SaveChanges();
        }
        /// <summary>
        /// Searches for the relating username in all DbSets and removes all content related to it
        /// </summary>
        /// <param name="UserName">Seeked username</param>
        public void RemoveUser(string UserName)
        {
            foreach(Car Car in CarDb)
            {
                if(Car.DisplayName==UserName)
                {
                    foreach(var it in FuelDb)
                    {
                        if (it.PlateNumber == Car.PlateNumber)
                            FuelDb.Remove(it);
                    }
                    foreach (var it in RepairDb)
                    {
                        if (it.Plate == Car.PlateNumber)
                            RepairDb.Remove(it);
                    }
                    foreach(var it in NotebookDb)
                    {
                        if (it.Plate == Car.PlateNumber)
                            NotebookDb.Remove(it);
                    }
                    CarDb.Remove(Car);
                }
            }
            SaveChanges();
        }
        /// <summary>
        /// Searches for Fuelbook-type note with matching description and removes it
        /// </summary>
        /// <param name="Description">Seeked description</param>
        public void RemoveFuelNote(string Description)
        {
            foreach(var Note in FuelDb)
            {
                if (Note.Description == Description)
                    FuelDb.Remove(Note);
            }
            SaveChanges();
        }
        /// <summary>
        /// Creates list of plate numbers related to username and returns as array
        /// </summary>
        /// <param name="DisplayName">Seeked username</param>
        /// <returns>Array of plate numbers related to the username</returns>
        public string[] ReturnPlates(string DisplayName)
        {
            int Size = 0;
            foreach (Car Car in CarDb)
                if(Car.DisplayName==DisplayName)
                    Size++;
             
            string[] Plates = new string[Size];
            Size = 0;
            foreach (Car Car in CarDb)
            {
                if (Car.DisplayName == DisplayName)
                {
                    Plates[Size] = Car.PlateNumber;
                    Size++;
                }
            }
            return Plates;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Class responsible for storing informations about Car-type objects, further refered to as "car"
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Indexer logistic value, handled by EF6 storage compartment
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// Variable meant for storing plate number of a car
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// Variable meant for storing username of a car
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Variable meant for storing password of a car
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Variable meant for storing fuel type of a car
        /// </summary>
        public string FuelType { get; set; }
        /// <summary>
        /// Variable meant for storing fuel capacity of a car
        /// </summary>
        public int FuelCapacity { get; set; }
        /// <summary>
        /// Variable meant for storing manufacturer of a car
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Variable meant for storing model of a car
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Variable meant for storing year of production of a car
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Variable meant for storing VIN number of a car
        /// </summary>
        public string VinNumber { get; set; }
        /// <summary>
        /// Boolean indicating if the car is set to user's favourite
        /// </summary>
        public bool IsFavourite { get; set; }

        /// <summary>
        /// Car-type object constructor, creates an empty car with IsFavourite set to false
        /// </summary>
        public Car()
        {
            this.IsFavourite = false;
        }
        /// <summary>
        /// Car-type object constructor, PlateNumber-Password logic, used when registering a car using password and plate. Creates a car with all specifications empty, excepts for given plate and password. Requires an artificial integer as third parameter.
        /// </summary>
        /// <param name="plate">Starting plate number of a car</param>
        /// <param name="password">Starting password related to the car user</param>
        /// <param name="a">Artificial integer, 2-field constructor already in-use by Username-Password constructor.</param>
        public Car(string plate, string password, int a)
        {
            this.PlateNumber = plate;
            this.Password = password;
            this.DisplayName = "Generic";
            this.IsFavourite = false;
            this.FuelType = "Generic";
            this.FuelCapacity = 42;
            this.Manufacturer = "Generic";
            this.Model = "Generic";
            this.Year = 1970;
            this.VinNumber = "Generic";
        }
        /// <summary>
        /// Car-type object constructor, Username-Password logic, used when registering a car using username and plate. Creates a car with all specifications empty, excepts for given username and password.
        /// </summary>
        /// <param name="username">Starting username of an user</param>
        /// <param name="password">Starting password of an user</param>
        public Car(string username, string password)
        {
            this.DisplayName = username;
            this.Password = password;
            this.PlateNumber = "Generic";
            this.IsFavourite = false;
            this.FuelType = "Generic";
            this.FuelCapacity = 42;
            this.Manufacturer = "Generic";
            this.Model = "Generic";
            this.Year = 1970;
            this.VinNumber = "Generic";
        }
        /// <summary>
        /// Car-type object constructor, Username-Platenumber-Password logic, used when creating a new car via MyGarage panel.
        /// </summary>
        /// <param name="PlateNumber">Starting plate number of a car</param>
        /// <param name="DisplayName">Starting username of an user</param>
        /// <param name="Pasword">Starting password of an user</param>
        public Car(string PlateNumber, string DisplayName, string Pasword)
        {
            this.DisplayName = PlateNumber;
            this.Password = Password;
            this.PlateNumber = PlateNumber;
            this.IsFavourite = false;
            this.FuelType = "Generic";
            this.FuelCapacity = 42;
            this.Manufacturer = "Generic";
            this.Model = "Generic";
            this.Year = 1970;
            this.VinNumber = "Generic";
        }
        /// <summary>
        /// Car-type object constructor, All-data logic, used when specifying car info via CarCreationPanel
        /// </summary>
        /// <param name="platenumber">Plate number of a car</param>
        /// <param name="displayname">Username of an user</param>
        /// <param name="password">Password of an user</param>
        /// <param name="fueltype">Fuel type of a car</param>
        /// <param name="fuelcapacity">Fuel capacity of a car</param>
        /// <param name="manufacturer">Manufacturer of a car</param>
        /// <param name="model">Model of a car</param>
        /// <param name="year">Year of procution of a car</param>
        /// <param name="vinnumber">VIN number of a car</param>
        public Car(string platenumber,  string displayname, string password, string fueltype, int fuelcapacity, string manufacturer, string model, int year, string vinnumber)
        {
            this.PlateNumber = platenumber;
            this.DisplayName = displayname;
            this.Password = Password;
            this.FuelType = fueltype;
            this.FuelCapacity = fuelcapacity;
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Year = year;
            this.VinNumber = vinnumber;
            this.IsFavourite = false;
        }
        /// <summary>
        /// Save object to the EF6 database
        /// </summary>
        /// <param name="DB">EF6 database reference (if valid)</param>
        public void SaveToDb(DbReference DB)
        {
            if(string.IsNullOrWhiteSpace(this.PlateNumber))
                throw new ArgumentException("Car Platenumber is null or white space");
            if (string.IsNullOrWhiteSpace(this.DisplayName))
                throw new ArgumentException("Car Username is null or white space");
            if (string.IsNullOrWhiteSpace(this.Password))
                throw new ArgumentException("User Password is null or white space");
            if (FuelCapacity <= 0)
                throw new ArgumentOutOfRangeException("Car FuelCapacity is equal or lower than 0");
            if (string.IsNullOrWhiteSpace(this.Manufacturer))
                throw new ArgumentException("Car Manufacturer is null or white space");
            if (string.IsNullOrWhiteSpace(this.Model))
                throw new ArgumentException("Car Model is null or white space");
            if (this.Year < 1800)
                throw new ArgumentOutOfRangeException("Car Year is lower than 1800");
            if (string.IsNullOrWhiteSpace(this.VinNumber))
                throw new ArgumentException("Car VIN is null or white space");
            else
            {
                DB.CarDb.Add(this);
                DB.SaveChanges();
            }
        }
    }
}

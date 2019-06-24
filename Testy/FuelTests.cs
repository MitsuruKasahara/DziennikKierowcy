using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aplikacja_dla_kierowców;
namespace AppTests
{
    [TestClass]
    public class FuelTests
    {
        static DbReference DB = new DbReference();
        static string v_PlateNumber = "ValidPlateNumber";
        static string v_Description = "ValidDescription";
        static decimal v_Cost = 50;
        static decimal v_Price = 50;
        static decimal v_Litres = 50;
        static decimal v_Distance = 50;
        static decimal v_Consumption = 50;
        static decimal v_Area_City = 50;
        static decimal v_Area_Open = 50;

        [TestMethod]
        public void SaveToDb_ExpectValid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlateNumber_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = "";
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Description_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = "";
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Cost_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = 0;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Price_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = 0;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Litres_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = 0;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Distance_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = 0;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Consumption_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = 0;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Area_City_Under_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = -1;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Area_City_Over_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = 101;
            FuelTest.Area_Open = v_Area_Open;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Area_Open_Under_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = -1;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Area_Open_Over_ExpectInvalid()
        {
            //arrange
            Fuelbook FuelTest = new Fuelbook();
            FuelTest.PlateNumber = v_PlateNumber;
            FuelTest.Description = v_Description;
            FuelTest.Cost = v_Cost;
            FuelTest.Price = v_Price;
            FuelTest.Litres = v_Litres;
            FuelTest.Distance = v_Distance;
            FuelTest.Consumption = v_Consumption;
            FuelTest.Area_City = v_Area_City;
            FuelTest.Area_Open = 101;
            //act
            FuelTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }
    }
}

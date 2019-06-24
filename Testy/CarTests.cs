using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aplikacja_dla_kierowców;

namespace AppTests
{
    [TestClass]
    public class CarTests
    {
        static DbReference DB = new DbReference();
        static string v_PlateNumber = "ValidPlate";
        static string v_DisplayName = "ValidUser";
        static string v_Password = "ValidPassword";
        static int v_FuelCapacity = 42;
        static string v_Model = "ValidModel";
        static string v_Manufacturer = "ValidManufacturer";
        static int v_Year = 1970;
        static string v_VinNumber = "ValidVIN";

        [TestMethod]
        public void SaveToDb_ExpectValid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with no exceptions            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlateNumber_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = "";
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DisplayName_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = "";
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Password_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = "";
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FuelCapacity_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = 0;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Model_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = "";
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Manufacturer_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = "";
            TestCar.Year = v_Year;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Year_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = 0;
            TestCar.VinNumber = v_VinNumber;
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VinNumber_ExpectInvalid()
        {
            //arrange
            Car TestCar = new Car();
            TestCar.PlateNumber = v_PlateNumber;
            TestCar.DisplayName = v_DisplayName;
            TestCar.Password = v_Password;
            TestCar.FuelCapacity = v_FuelCapacity;
            TestCar.Model = v_Model;
            TestCar.Manufacturer = v_Manufacturer;
            TestCar.Year = v_Year;
            TestCar.VinNumber = "";
            //act
            TestCar.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aplikacja_dla_kierowców;

namespace AppTests
{
    [TestClass]
    public class RepairTests
    {
        static DbReference DB = new DbReference();
        static string v_Plate = "ValidPlate";
        static string v_Text = "ValidText";
        static decimal v_Price = 42;

        [TestMethod]
        public void SaveToDb_ExpectValid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = v_Plate;
            RepairTest.Text = v_Text;
            RepairTest.Price = v_Price;
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with no exceptions
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Plate_ExpectInvalid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = "";
            RepairTest.Text = v_Text;
            RepairTest.Price = v_Price;
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Text_ExpectInvalid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = v_Plate;
            RepairTest.Text = "";
            RepairTest.Price = v_Price;
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Price_ExceptInvalid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = v_Plate;
            RepairTest.Text = v_Text;
            RepairTest.Price = -1;
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentOutOfRangeException
        }
    }
}

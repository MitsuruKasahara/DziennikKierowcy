using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aplikacja_dla_kierowców;

namespace AppTests
{
    [TestClass]
    public class NoteTests
    {
        static DbReference DB = new DbReference();
        static string v_Plate = "ValidPlate";
        static string v_Text = "ValidText";

        [TestMethod]
        public void SaveToDb_ExpectValid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = v_Plate;
            RepairTest.Text = v_Text;
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Plate_ExpectInvalid()
        {
            //arrange
            Repairbook RepairTest = new Repairbook();
            RepairTest.Plate = "";
            RepairTest.Text = v_Text;
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
            //act
            RepairTest.SaveToDb(DB);
            //assert
            //should handle with ArgumentException
        }
    }
}

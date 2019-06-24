using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Interaction logic for Statisticsxaml.xaml
    /// </summary>
    public partial class Statisticsxaml : Page
    {
        DbReference DB = new DbReference();

        public Statisticsxaml()
        {
            InitializeComponent();

            LConsumption_Max.Text = M_LConsumption_Max();
            LConsumption_Min.Text = M_LConsumption_Min();
            LConsumption_Avg.Text = M_LConsumption_Avg();
            LLitres_Sum.Text = M_LLitres_Sum();
            LCost_Sum.Text = M_LCost_Sum();
            LDistance_Sum.Text = M_LDistance_Sum();
            LRepairs_Sum.Text = M_LRepairs_Sum();
            LRepairsToDistance.Text = M_LRepairsToDistance();
            LCycle_Avg.Text = M_LCycle_Avg();
            LRepairsAbove1000.Text = M_LRepairsAbove1000();
            LRepairsUnder1000.Text = M_LRepairsUnder1000();
            LOverall_Cost.Text = M_LOverall_Cost();

            GConsumption_Max.Text = M_GConsumption_Max();
            GConsumption_Min.Text = M_GConsumption_Min();
            GConsumption_Avg.Text = M_GConsumption_Avg();
            GLitres_Sum.Text = M_GLitres_Sum();
            GCost_Sum.Text = M_GCost_Sum();
            GDistance_Sum.Text = M_GDistance_Sum();
            GRepairs_Sum.Text = M_GRepairs_Sum();
            GRepairsToDistance.Text = M_GRepairsToDistance();
            GCycle_Avg.Text = M_GCycle_Avg();
            GRepairsAbove1000.Text = M_GRepairsAbove1000();
            GRepairsUnder1000.Text = M_GRepairsUnder1000();
            GOverall_Cost.Text = M_GOverall_Cost();
            GOldestCar.Text = M_GOldestCar();
            GNewestCar.Text = M_GNewestCar();
            GYearsAvg.Text = M_GYearsAvg() + " lat";
            GOpenCar.Text = M_GOpenCar();
            GCityCar.Text = M_GCityCar();
            GFrequentCar.Text = M_GFrequentCar();
            GCosty.Text = M_GCosty();
            GEco.Text = M_GEco();

            if(DB.RepairDb.Count()>0)
            {
                GUnreliable.Text = M_GUnreliable();
                GReliable.Text = M_GReliable();
            }
            else
            {
                GUnreliable.Text = "Nie zanotowano napraw";
                GReliable.Text = "Nie zanotowano napraw";
            }
        }

        public string M_LConsumption_Max()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    if (Note.Consumption > ReturnValue)
                        ReturnValue = Note.Consumption;
                }
            }
            return ReturnValue.ToString("0.##") + "L/100km";
        }
        public string M_LConsumption_Min()
        {
            decimal ReturnValue = 9999999999999;
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    if (Note.Consumption < ReturnValue)
                        ReturnValue = Note.Consumption;
                }
            }
            return ReturnValue.ToString("0.##") + "L/100km";
        }
        public string M_LConsumption_Avg()
        {
            decimal ReturnValue=0;
            decimal cnt = 0;
            foreach(var Note in DB.FuelDb)
            {
                if(Note.PlateNumber==DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Consumption;
                    cnt++;
                }
            }
            return (ReturnValue/cnt).ToString("0.##")+"L/100km";
        }
        public string M_LLitres_Sum()
        {
            decimal ReturnValue = 0;
            foreach(var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Litres;
                }
            }
            return ReturnValue.ToString("0.##") + "L";
        }
        public string M_LCost_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Cost;
                }
            }
            return ReturnValue.ToString("F") + "PLN";
        }
        public string M_LDistance_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Distance;
                }
            }
            return ReturnValue.ToString("0.#") + "km";
        }
        public string M_LRepairs_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (Note.Plate == DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Price;
                }
            }
            return ReturnValue.ToString("F") + "PLN";
        }
        public string M_LRepairsToDistance()
        {
            decimal Repairs = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (Note.Plate == DB.ReturnLastEntry().PlateNumber)
                {
                    Repairs += Note.Price;
                }
            }

            decimal Avg_Price = 0;
            decimal Avg_Consumption = 0;
            decimal cnt = 0;
            foreach(var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    Avg_Price += Note.Price;
                    Avg_Consumption += Note.Consumption;
                    cnt++;
                }
            }
            Avg_Price /= cnt;
            Avg_Consumption /= cnt;

            decimal ReturnValue = Repairs * 100 / (Avg_Price * Avg_Consumption);

            return ReturnValue.ToString("0.#") + "km";
        }
        public string M_LCycle_Avg()
        {
            decimal ReturnValue = 0;
            decimal cnt = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                {
                    ReturnValue += Note.Area_City;
                    cnt++;
                }
            }
            ReturnValue /= cnt;
            return $"Miejski: {ReturnValue.ToString("0.#")}%\tTrasa: {(100 - ReturnValue).ToString("0.#")}%";
        }
        public string M_LRepairsAbove1000()
        {
            int cnt = 0;
            foreach(var Note in DB.RepairDb)
            {
                if (Note.Plate == DB.ReturnLastEntry().PlateNumber && Note.Price > 1000)
                    cnt++;
            }
            return cnt.ToString();
        }
        public string M_LRepairsUnder1000()
        {
            int cnt = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (Note.Plate == DB.ReturnLastEntry().PlateNumber && Note.Price < 1000)
                    cnt++;
            }
            return cnt.ToString();
        }
        public string M_LOverall_Cost()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (Note.Plate == DB.ReturnLastEntry().PlateNumber)
                    ReturnValue += Note.Price;
            }
            foreach (var Note in DB.FuelDb)
            {
                if (Note.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                    ReturnValue += Note.Cost;
            }

            return ReturnValue.ToString("F")+"PLN";
        }

        public string M_GConsumption_Max()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Note.Consumption > ReturnValue)
                        ReturnValue = Note.Consumption;
                }
            }
            return ReturnValue.ToString("0.##") + "L/100km";
        }
        public string M_GConsumption_Min()
        {
            decimal ReturnValue = 9999999999999;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Note.Consumption < ReturnValue)
                        ReturnValue = Note.Consumption;
                }
            }
            return ReturnValue.ToString("0.##") + "L/100km";
        }
        public string M_GConsumption_Avg()
        {
            decimal ReturnValue = 0;
            decimal cnt = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    ReturnValue += Note.Consumption;
                    cnt++;
                }
            }
            return (ReturnValue / cnt).ToString("0.##") + "L/100km";
        }
        public string M_GLitres_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    ReturnValue += Note.Litres;
                }
            }
            return ReturnValue.ToString("0.##") + "L";
        }
        public string M_GCost_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    ReturnValue += Note.Cost;
                }
            }
            return ReturnValue.ToString("F") + "PLN";
        }
        public string M_GDistance_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    ReturnValue += Note.Distance;
                }
            }
            return ReturnValue.ToString("0.#") + "km";
        }
        public string M_GRepairs_Sum()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.Plate))
                {
                    ReturnValue += Note.Price;
                }
            }
            return ReturnValue.ToString("F") + "PLN";
        }
        public string M_GRepairsToDistance()
        {
            decimal Repairs = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.Plate))
                {
                    Repairs += Note.Price;
                }
            }

            decimal Avg_Price = 0;
            decimal Avg_Consumption = 0;
            decimal cnt = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    Avg_Price += Note.Price;
                    Avg_Consumption += Note.Consumption;
                    cnt++;
                }
            }
            Avg_Price /= cnt;
            Avg_Consumption /= cnt;

            decimal ReturnValue = Repairs * 100 / (Avg_Price * Avg_Consumption);

            return ReturnValue.ToString("0.#") + "km";
        }
        public string M_GCycle_Avg()
        {
            decimal ReturnValue = 0;
            decimal cnt = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    ReturnValue += Note.Area_City;
                    cnt++;
                }
            }
            ReturnValue /= cnt;
            return $"Miejski: {ReturnValue.ToString("0.#")}%\tTrasa: {(100 - ReturnValue).ToString("0.#")}%";
        }
        public string M_GRepairsAbove1000()
        {
            int cnt = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.Plate) && Note.Price > 1000)
                    cnt++;
            }
            return cnt.ToString();
        }
        public string M_GRepairsUnder1000()
        {
            int cnt = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.Plate) && Note.Price < 1000)
                    cnt++;
            }
            return cnt.ToString();
        }
        public string M_GOverall_Cost()
        {
            decimal ReturnValue = 0;
            foreach (var Note in DB.RepairDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.Plate))
                    ReturnValue += Note.Price;
            }
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                    ReturnValue += Note.Cost;
            }

            return ReturnValue.ToString("F") + "PLN";
        }
        public string M_GOldestCar()
        {
            Car Selected = new Car();
            Selected.Year = 9999;
            foreach(Car Note in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Note.Year < Selected.Year)
                        Selected = Note;
                }
            }
            return Selected.Year + " " + Selected.Manufacturer + " " + Selected.Model;
        }
        public string M_GNewestCar()
        {
            Car Selected = new Car();
            Selected.Year = 0;
            foreach (Car Note in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Note.Year > Selected.Year)
                        Selected = Note;
                }
            }
            return Selected.Year + " " + Selected.Manufacturer + " " + Selected.Model;
        }
        public string M_GYearsAvg()
        {
            int Year = 0;
            int Cnt = 0;
            foreach (Car Note in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    Year += Note.Year;
                    Cnt++;
                }
            }
            Year /= Cnt;
            DateTime Now = DateTime.Today;
            return (int.Parse(Now.ToString("yyyy")) - Year).ToString();
        }
        public string M_GOpenCar()
        {
            Fuelbook Car = new Fuelbook();
            Car ReturnCar = new Car();
            Car.Area_City = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Car.Area_Open < Note.Area_Open)
                        Car = Note;
                }
            }

            foreach(var Note in DB.CarDb)
            {
                if (Note.PlateNumber == Car.PlateNumber)
                    ReturnCar = Note;
            }
            return ReturnCar.Year + " " + ReturnCar.Manufacturer + " " + ReturnCar.Model + $"{Car.Area_Open}%";
        }
        public string M_GCityCar()
        {
            Fuelbook Car = new Fuelbook();
            Car ReturnCar = new Car();
            Car.Area_City = 0;
            foreach (var Note in DB.FuelDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Note.PlateNumber))
                {
                    if (Car.Area_City < Note.Area_City)
                        Car = Note;
                }
            }

            foreach (var Note in DB.CarDb)
            {
                if (Note.PlateNumber == Car.PlateNumber)
                    ReturnCar = Note;
            }
            return ReturnCar.Year + " " + ReturnCar.Manufacturer + " " + ReturnCar.Model + $" {Car.Area_City}%";
        }
        public string M_GFrequentCar()
        {
            decimal MaxDist = 0;
            decimal LocalMaxDist = 0;
            Car MaxDistCar = new Car();
            foreach (var Car in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Car.PlateNumber))
                {
                    foreach(var Note in DB.FuelDb)
                    {
                        if(Note.PlateNumber==Car.PlateNumber)
                        {
                            LocalMaxDist += Note.Distance;
                        }
                    }
                    if(LocalMaxDist>MaxDist)
                    {
                        MaxDist = LocalMaxDist;
                        MaxDistCar = Car;
                    }
                    LocalMaxDist = 0;
                }
            }
            return MaxDistCar.Year + " " + MaxDistCar.Manufacturer + " " + MaxDistCar.Model + " " + $"({MaxDist}km)";
        }
        public string M_GUnreliable()
        {
            decimal Unreliable = 0;
            decimal LocalUnreliable = 0;
            decimal LocalDistance = 0;
            Car UnreliableCar = new Car();
            foreach (var Car in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Car.PlateNumber))
                {
                    foreach (var Note in DB.RepairDb)
                    {
                        if (Note.Plate == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }
                    foreach(var Note in DB.FuelDb)
                    {
                        if (Note.PlateNumber == Car.PlateNumber)
                        {
                            LocalDistance += Note.Distance;
                        }
                    }
                    LocalUnreliable /= LocalDistance;

                    if (LocalUnreliable > Unreliable)
                    {
                        Unreliable = LocalUnreliable;
                        UnreliableCar = Car;
                    }
                    LocalUnreliable = 0;
                    LocalDistance = 0;
                }
            }
            return UnreliableCar.Year + " " + UnreliableCar.Manufacturer + " " + UnreliableCar.Model + " " + $"({Unreliable.ToString("F")}PLN napraw na kilometr)";
        }
        public string M_GReliable()
        {
            decimal Unreliable = 99999;
            decimal LocalUnreliable = 0;
            decimal LocalDistance = 0;
            Car UnreliableCar = new Car();
            foreach (var Car in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Car.PlateNumber))
                {
                    foreach (var Note in DB.RepairDb)
                    {
                        if (Note.Plate == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }
                    foreach (var Note in DB.FuelDb)
                    {
                        if (Note.PlateNumber == Car.PlateNumber)
                        {
                            LocalDistance += Note.Distance;
                        }
                    }
                    LocalUnreliable /= LocalDistance;

                    if (LocalUnreliable < Unreliable)
                    {
                        Unreliable = LocalUnreliable;
                        UnreliableCar = Car;
                    }
                    LocalUnreliable = 0;
                    LocalDistance = 0;
                }
            }
            return UnreliableCar.Year + " " + UnreliableCar.Manufacturer + " " + UnreliableCar.Model + " " + $"({Unreliable.ToString("F")}PLN napraw na kilometr)";
        }
        public string M_GCosty()
        {
            decimal Unreliable = 0;
            decimal LocalUnreliable = 0;
            Car UnreliableCar = new Car();
            foreach (var Car in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Car.PlateNumber))
                {
                    foreach (var Note in DB.RepairDb)
                    {
                        if (Note.Plate == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }
                    foreach (var Note in DB.FuelDb)
                    {
                        if (Note.PlateNumber == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }

                    if (LocalUnreliable > Unreliable)
                    {
                        Unreliable = LocalUnreliable;
                        UnreliableCar = Car;
                    }
                    LocalUnreliable = 0;
                }
            }
            return UnreliableCar.Year + " " + UnreliableCar.Manufacturer + " " + UnreliableCar.Model + " " + $"({Unreliable.ToString("F")}PLN)";
        }
        public string M_GEco()
        {
            decimal Unreliable = 99999;
            decimal LocalUnreliable = 0;
            Car UnreliableCar = new Car();
            foreach (var Car in DB.CarDb)
            {
                if (DB.ReturnPlates(DB.ReturnLastEntry().DisplayName).Contains(Car.PlateNumber))
                {
                    foreach (var Note in DB.RepairDb)
                    {
                        if (Note.Plate == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }
                    foreach (var Note in DB.FuelDb)
                    {
                        if (Note.PlateNumber == Car.PlateNumber)
                        {
                            LocalUnreliable += Note.Price;
                        }
                    }

                    if (LocalUnreliable < Unreliable)
                    {
                        Unreliable = LocalUnreliable;
                        UnreliableCar = Car;
                    }
                    LocalUnreliable = 0;
                }
            }
            return UnreliableCar.Year + " " + UnreliableCar.Manufacturer + " " + UnreliableCar.Model + " " + $"({Unreliable.ToString("F")}PLN)";
        }

    }
}

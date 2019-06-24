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
    /// Interaction logic for AddFuel.xaml
    /// </summary>
    public partial class AddFuel : Page
    {
        DbReference DB = new DbReference();
        public AddFuel()
        {
            InitializeComponent();
            _Button.Click += GlobalFocus;
            _Button.Click += Button_Click;
        }

        private void GlobalFocus(object sender, RoutedEventArgs e)
        {
            decimal Price = new decimal();
            decimal Litres = new decimal();
            decimal Cost = new decimal();
            decimal Consumption = new decimal();
            decimal Distance = new decimal();
            
            if (TryParse(_Consumption.Text) == true)
            {
                Consumption = decimal.Parse(_Consumption.Text.ToString().Replace(".", ","));

                if (TryParse(_Litres.Text) == true)
                {
                    Litres = decimal.Parse(_Litres.Text.ToString().Replace(".", ","));
                    _Distance.Text = (Litres / Consumption * 100).ToString("0.###");
                }
                else if (TryParse(_Cost.Text) == true && TryParse(_Price.Text) == true)
                {
                    Cost = decimal.Parse(_Cost.Text.ToString().Replace(".", ","));
                    Price = decimal.Parse(_Price.Text.ToString().Replace(".", ","));

                    _Distance.Text = (Cost * 100 / (Price * Consumption)).ToString("0.###");
                }
            }
            if (TryParse(_Cost.Text) == true)
            {
                Cost = decimal.Parse(_Cost.Text.ToString().Replace(".", ","));

                if (TryParse(_Litres.Text) == true)
                {
                    Litres = decimal.Parse(_Litres.Text.ToString().Replace(".", ","));

                    _Price.Text = (Cost / Litres).ToString("F");
                }
                if (TryParse(_Consumption.Text) == true && TryParse(_Distance.Text) == true)
                {
                    Consumption = decimal.Parse(_Consumption.Text.ToString().Replace(".", ","));
                    Distance = decimal.Parse(_Distance.Text.ToString().Replace(".", ","));

                    _Price.Text = (Cost * 100 / (Consumption * Distance)).ToString("F");
                }
            }
            if (TryParse(_Distance.Text) == true)
            {
                Distance = decimal.Parse(_Distance.Text.ToString().Replace(".", ","));

                if (TryParse(_Litres.Text) == true)
                {
                    Litres = decimal.Parse(_Litres.Text.ToString().Replace(".", ","));

                    _Consumption.Text = (Litres * 100 / Distance).ToString("0.##");
                }
                else if (TryParse(_Cost.Text) == true && TryParse(_Price.Text) == true)
                {
                    Cost = decimal.Parse(_Cost.Text.ToString().Replace(".", ","));
                    Price = decimal.Parse(_Price.Text.ToString().Replace(".", ","));

                    _Consumption.Text = (Cost * 100 / (Distance * Price)).ToString("0.##");
                }
            }
            if (TryParse(_Price.Text) == true)
            {
                Price = decimal.Parse(_Price.Text.Replace(".", ","));

                if (TryParse(_Litres.Text) == true)
                {
                    Litres = decimal.Parse(_Litres.Text.ToString().Replace(".", ","));
                    _Cost.Text = (Price / Litres).ToString("F");
                }
                if (TryParse(_Consumption.Text) == true && TryParse(_Distance.Text) == true)
                {
                    Consumption = decimal.Parse(_Consumption.Text.ToString().Replace(".", ","));
                    Distance = decimal.Parse(_Distance.Text.ToString().Replace(".", ","));

                    _Cost.Text = (Price * Consumption * Distance / 100).ToString("F");
                }
            }
            if (TryParse(_Consumption.Text) == true && TryParse(_Distance.Text) == true)
            {
                Consumption = decimal.Parse(_Consumption.Text.Replace(".",","));
                Distance = decimal.Parse(_Distance.Text.ToString().Replace(".", ","));

                _Litres.Text = (Consumption * Distance /100 ).ToString();
            }
            if (TryParse(_Cost.Text) == true && TryParse(_Price.Text) == true)
            {
                Price = decimal.Parse(_Price.Text.Replace(".", ","));
                Cost = decimal.Parse(_Cost.Text.ToString().Replace(".", ","));

                _Litres.Text = (Cost / Price).ToString("0.#");
            }
            if (TryParse(_Area_Open.Text) == true)
                _Area_City.Text = (100 - decimal.Parse(_Area_Open.Text.Replace(".", ","))).ToString("0.#");
            
            if (TryParse(_Area_City.Text) == true)
                _Area_Open.Text = (100 - decimal.Parse(_Area_City.Text.Replace(".", ","))).ToString("0.#");

            if((TryParse(_Area_Open.Text) == true || TryParse(_Area_City.Text) == true) && (decimal.Parse(_Area_Open.Text.Replace(".", ",")) > 100 || decimal.Parse(_Area_City.Text.Replace(".", ",")) > 100))
            {
                _Area_City.Text = "";
                _Area_Open.Text = "";
                MessageBox.Show("Przekroczon dopuszczalny zakres: 0-100");
            }
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (TryParse(_Price.Text) == true && TryParse(_Litres.Text) == true && TryParse(_Cost.Text) == true && TryParse(_Consumption.Text) == true && TryParse(_Distance.Text) == true && TryParse(_Area_City.Text)==true && TryParse(_Area_Open.Text)==true)
            {
                Fuelbook FuelNote = new Fuelbook(DB.ReturnLastEntry().PlateNumber, _Description.Text, decimal.Parse(_Cost.Text), decimal.Parse(_Price.Text), decimal.Parse(_Litres.Text), decimal.Parse(_Distance.Text), decimal.Parse(_Consumption.Text), decimal.Parse(_Area_City.Text), decimal.Parse(_Area_Open.Text));
                FuelNote.SaveToDb(DB);
                TextBlock Info = new TextBlock();
                Info.Text = $"Dodano notatkę. \nOpis: {FuelNote.Description}\nDolano Litrów: {FuelNote.Litres}L\nPrzejechany dystans: {FuelNote.Distance}km\nCena tankowania: {FuelNote.Price}zł\nKoszt paliwa: {FuelNote.Cost}PLN/L\nPrzeciętne spalanie: {FuelNote.Consumption}L/100km\nCykl jazdy: \n\t{FuelNote.Area_City}% w zabudowanym, \n\t{FuelNote.Area_Open}% w trasie\n";
                _Info.Children.Add(Info);
                
            }
            else
            {
                MessageBox.Show("Nie wszystkie rubryki zostały wypełnione lub dane są niepoprawne");
                _Price.Text = "";
                _Litres.Text = "";
                _Cost.Text = "";
                _Consumption.Text = "";
                _Distance.Text = "";
                _Area_City.Text = "";
                _Area_Open.Text = "";
            }
            
        }

        static bool TryParse(string String)
        {
            char[] Forbidden = " abcdefghijklmnopqrstuwvxyzABCDEFGHIJKLMNOPQRSTUWVXYZ.;'[]=-`*-+".ToCharArray();
            char[] Tested = String.ToCharArray();
            foreach(var Letter in Tested)
            {
                if (Forbidden.Contains(Letter))
                    return false;
            }
            if (String == "")
                return false;
            if (decimal.Parse(String) <= 0)
                return false;

            return true;
        }
    }
}

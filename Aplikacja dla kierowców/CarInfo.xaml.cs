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
    /// Interaction logic for CarInfo.xaml
    /// </summary>
    public partial class CarInfo : Page
    {
        DbReference DB = new DbReference();

        public CarInfo()
        {
            InitializeComponent();
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            this.DataContext = SelectedCar;
        }

        private void B0_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB0.Background = Brushes.White;
                TB0.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok" && DB.CheckPlate(SelectedCar.PlateNumber) == false)
            {
                obj.Content = "Zmień";
                TB0.IsReadOnly = true;
                TB0.Background = Brushes.LightGray;
                DB.ChangePlateNumber(SelectedCar.PlateNumber, TB0.Text);
            }
            else
            {
                TB0.Text = SelectedCar.PlateNumber;
                obj.Content = "Zmień";
                TB0.IsReadOnly = true;
                TB0.Background = Brushes.LightGray;
                MessageBox.Show("Tablica już jest wykorzystywana przez inny samochód");
            }
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB1.Background = Brushes.White;
                TB1.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB1.IsReadOnly = true;
                TB1.Background = Brushes.LightGray;
                DB.ChangeDisplayName(SelectedCar.PlateNumber, TB1.Text);
                
                MainWindow Main = new MainWindow();
                Main.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB2.Background = Brushes.White;
                TB2.IsEnabled = true;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB2.IsEnabled = false;
                TB2.Background = Brushes.LightGray;
                DB.ChangeFuelType(SelectedCar.PlateNumber, TB2.Text);
            }
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB3.Background = Brushes.White;
                TB3.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB3.IsReadOnly = true;
                TB3.Background = Brushes.LightGray;
                if (TryParse(TB3.Text) == false)
                {
                    TB3.Text = SelectedCar.FuelCapacity.ToString();
                    MessageBox.Show("Nieprawidłowy format ciągu wejściowego");
                }
                DB.ChangeFuelCapacity(SelectedCar.PlateNumber, TB3.Text);
            }
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB4.Background = Brushes.White;
                TB4.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB4.IsReadOnly = true;
                TB4.Background = Brushes.LightGray;
                DB.ChangeManufacturer(SelectedCar.PlateNumber, TB4.Text);
            }

        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB5.Background = Brushes.White;
                TB5.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB5.IsReadOnly = true;
                TB5.Background = Brushes.LightGray;
                DB.ChangeModel(SelectedCar.PlateNumber, TB5.Text);
            }
        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB6.Background = Brushes.White;
                TB6.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB6.IsReadOnly = true;
                TB6.Background = Brushes.LightGray;
                if (TryParse(TB6.Text) == false)
                {
                    TB6.Text = SelectedCar.Year.ToString();
                    MessageBox.Show("Nieprawidłowy format ciągu wejściowego");
                }
                DB.ChangeYear(SelectedCar.PlateNumber, TB6.Text);
            }
        }

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = DB.ReturnLastEntry(); // new Car(DB.ReturnLastEntry());
            Button obj = sender as Button;

            if ((string)obj.Content == "Zmień")
            {
                obj.Content = "Ok";
                TB7.Background = Brushes.White;
                TB7.IsReadOnly = false;
            }
            else if ((string)obj.Content == "Ok")
            {
                obj.Content = "Zmień";
                TB7.IsReadOnly = true;
                TB7.Background = Brushes.LightGray;
                DB.ChangeVinNumber(SelectedCar.PlateNumber, TB7.Text);
            }
        }
        static bool TryParse(string String)
        {
            char[] Forbidden = " abcdefghijklmnopqrstuwvxyzABCDEFGHIJKLMNOPQRSTUWVXYZ.;'[]=-`*-+".ToCharArray();
            char[] Tested = String.ToCharArray();
            foreach (var Letter in Tested)
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

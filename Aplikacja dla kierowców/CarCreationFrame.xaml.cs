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
    /// Interaction logic for CarCreationFrame.xaml
    /// </summary>
    public partial class CarCreationFrame : Page
    {
        DbReference DB = new DbReference();

        public CarCreationFrame()
        {
            Car car = DB.ReturnLastEntry();

            InitializeComponent();
            this.DataContext = car;

            if (car.PlateNumber == "Generic")
            {
                creator_DisplayName.IsReadOnly = true;
                creator_DisplayName.Background = Brushes.LightGray;
                creator_Password.IsReadOnly = true;
                creator_Password.Background = Brushes.LightGray;
            }
            if(car.DisplayName == "Generic")
            {
                creator_PlateNumber.IsReadOnly = true;
                creator_PlateNumber.Background = Brushes.LightGray;
                creator_Password.IsReadOnly = true;
                creator_Password.Background = Brushes.LightGray;
            }
            if (car.PlateNumber == "Generic")
                car.PlateNumber = "";
            if (car.DisplayName == "Generic")
                car.DisplayName = "";
        }

        private void SaveToDb_Click(object sender, RoutedEventArgs e)
        {
            Car car = DB.ReturnLastEntry();
            DB.RemoveByPlate(car.PlateNumber);

            if (creator_DisplayName.Text == "" || creator_Password.Text == "" || creator_FuelType.Text == "" || TryParse(creator_FuelCapacity.Text) == false || creator_Manufacturer.Text == "" || creator_Model.Text == "" || creator_VinNumber.Text == "" || TryParse(creator_Year.Text) == false)
            {
                MessageBox.Show("Uzupełnij poprawnie wszystkie rubryki");
                creator_FuelCapacity.Text = "";
                creator_Year.Text= "";
            }
            else if (DB.CheckPlate(creator_PlateNumber.Text) == true)
                MessageBox.Show("Tablica już w użyciu");
            else
            {

                car.PlateNumber = creator_PlateNumber.Text;
                car.DisplayName = creator_DisplayName.Text;
                car.Password = creator_Password.Text;
                car.FuelType = creator_FuelType.Text;
                car.FuelCapacity = Int32.Parse(creator_FuelCapacity.Text);
                car.Manufacturer = creator_Manufacturer.Text;
                car.Model = creator_Model.Text;
                car.Year = Int32.Parse(creator_Year.Text);
                car.VinNumber = creator_VinNumber.Text;

                car.SaveToDb(DB);
                SaveToDb.Visibility = Visibility.Collapsed;
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

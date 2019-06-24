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
using System.Windows.Shapes;

namespace Aplikacja_dla_kierowców
{
    /// <summary>
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        DbReference DB = new DbReference();
        //Car SelectedCar = new Car(DB.ReturnLastEntry());


        public UserPanel()
        {
            InitializeComponent();
            Car SelectedCar = DB.ReturnLastEntry(); //new Car(DB.ReturnLastEntry());
            LastNote_Box.Text = DB.ReturnLastNote(SelectedCar.PlateNumber);
            _CarName.Text = $"{SelectedCar.Manufacturer} {SelectedCar.Model}, rocznik {SelectedCar.Year} ";
            _UserName.Text = SelectedCar.DisplayName;
            this.DataContext = SelectedCar;
            
        }

        private void Check_CarInfo_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CarInfo();
        }

        private void Check_Stats_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            foreach (var it in DB.FuelDb)
            {
                if (it.PlateNumber == DB.ReturnLastEntry().PlateNumber)
                    check = true;
            }
            if (check==true)
                MainFrame.Content = new Statisticsxaml();
            else
                MessageBox.Show("Ekran statystyk jest dostępny po dodaniu co najmniej jednego tankowania");
        }

        private void Check_Garage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MyGarage();
        }

        private void Add_FuelInfo_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddFuel();
        }

        private void Add_RepairInfo_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddRepair();
        }

        private void Add_TravelInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Notes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddNote();
        }

        private void Return_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void Remove_Btn_Click(object sender, RoutedEventArgs e)
        {
            DB.RemoveUser(DB.ReturnLastEntry().DisplayName);
            DB.SaveChanges();

            MessageBox.Show("Użytkownik został usunięty");
            Window Main = new MainWindow();
            Main.Show();
            this.Close();
        }

        private void Check_Notes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new NoteBook();
        }

        private void Check_Repairs_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new RepairBook();
        }

        private void Check_Fuel_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FuelBook();
        }
        
    }
}

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        DbReference DB = new DbReference();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            DB.RemoveGenerics();
            DB.RemoveIncomplete();
            if (BeginButton.Content.ToString() == "Potwierdź")
            {
                DisplayFrame.Content = null;
                BeginButton.Content = "Rozpocznij";
            }
            else if (DB.CheckPlateCredential(S1Plate.Text, S1Password.Text) == 0)
            {
                Car newCar = new Car(S1Plate.Text, S1Password.Text, 0);
                newCar.SaveToDb(DB);

                BeginButton.Content = "Potwierdź";
                DisplayFrame.Content = new CarCreationFrame();
            }
            else
            {

                Car newCar = new Car();
                newCar = DB.ReturnByPlateNumber(S1Plate.Text);
                DB.RemoveByPlate(S1Plate.Text);
                newCar.SaveToDb(DB);


                UserPanel Usrpnl = new UserPanel();
                Usrpnl.Show();
                this.Close();
            }
            
        }

        private void _Login_Plate_Click(object sender, RoutedEventArgs e)
        {
            _Login_Plate.Visibility = Visibility.Collapsed;
            _Login_User.Visibility = Visibility.Collapsed;

            _Register_By_Plate_Panel.Visibility = Visibility.Visible;
        }

        private void _Login_User_Click(object sender, RoutedEventArgs e)
        {
            _Login_Plate.Visibility = Visibility.Collapsed;
            _Login_User.Visibility = Visibility.Collapsed;

            _Register_By_User_Credentials.Visibility = Visibility.Visible;
        }

        private void UserBeginButton_Click(object sender, RoutedEventArgs e)
        {
            DB.RemoveGenerics();
            DB.RemoveIncomplete();
            if (UserBeginButton.Content.ToString() == "Potwierdź")
            {
                DisplayFrame.Content = null;
                UserBeginButton.Content = "Zaloguj lub Zarejestruj";
            }
            else if (DB.CheckCredentials(Login_UserName.Text.ToString(), Login_Password.Text.ToString()) == 0)
            {
                Car newCar = new Car(Login_UserName.Text.ToString(), Login_Password.Text.ToString());
                newCar.SaveToDb(DB);

                UserBeginButton.Content = "Potwierdź";
                DisplayFrame.Content = new CarCreationFrame();
            }
            else
            {
                Car newCar = new Car();
                newCar = DB.ReturnByCredentials(Login_UserName.Text.ToString(), Login_Password.Text.ToString());
                DB.RemoveByPlate(newCar.PlateNumber);
                newCar.SaveToDb(DB);


                UserPanel Usrpnl = new UserPanel();
                Usrpnl.Show();
                this.Close();
                
            }
        }
    }
}

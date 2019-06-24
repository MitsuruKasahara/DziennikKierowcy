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
    /// Interaction logic for AddRepair.xaml
    /// </summary>
    public partial class AddRepair : Page
    {
        DbReference DB = new DbReference();
        public AddRepair()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ActiveString1 = "naprawioną ";
            string ActiveString2 = "";
            bool ActiveTmp = false;
            if (CheckBox.IsChecked == true)
            {
                ActiveTmp = true;
                ActiveString1 = "aktywną ";
                ActiveString2 = "przewidywany ";
            }
            if(TryParse(Price.Text.Replace(".", ","))==true)
            {
                Repairbook Note = new Repairbook(DB.ReturnLastEntry().PlateNumber, decimal.Parse(Price.Text.Replace(".", ",")), Description.Text, ActiveTmp);
                Note.SaveToDb(DB);
                TextBlock Info = new TextBlock
                {
                    Text = $"Dodano {ActiveString1}usterkę: {Description.Text}, {ActiveString2}koszt: {Price.Text}PLN"
                };
                _StackPanel.Children.Add(Info);
                Description.Text = "";
                Price.Text = "";
                CheckBox.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Niepoprawny format ciągu wejściowego dla ceny");
                Price.Text = "";
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

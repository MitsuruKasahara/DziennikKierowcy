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
    /// Interaction logic for FuelBook.xaml
    /// </summary>
    public partial class FuelBook : Page
    {
        DbReference DB = new DbReference();
        public FuelBook()
        {
            InitializeComponent();

            /*
            string test = "";
            foreach (var FuelNote in DB.FuelDb)
            {
                test += $"Tablica: {FuelNote.PlateNumber}\nOpis: {FuelNote.Description}\nDolano Litrów: {FuelNote.Litres}L\nPrzejechany dystans: {FuelNote.Distance}km\nCena tankowania: {FuelNote.Price}zł\nKoszt paliwa: {FuelNote.Cost}PLN/L\nPrzeciętne spalanie: {FuelNote.Consumption}L/100km\nCykl jazdy: \n\t{FuelNote.Area_City}% w zabudowanym, \n\t{FuelNote.Area_Open}% w trasie\n";
            }
            MessageBox.Show(test);
            */


            int Index = 0;
            foreach (var Note in DB.FuelDb)
            {
                if(Note.PlateNumber==DB.ReturnLastEntry().PlateNumber)
                {
                    Grid Grid = new Grid();
                    Grid.Name = $"Grid_{Index}";
                    ColumnDefinition Column0 = new ColumnDefinition();
                    ColumnDefinition Column1 = new ColumnDefinition();
                    ColumnDefinition Column2 = new ColumnDefinition();
                    ColumnDefinition Column3 = new ColumnDefinition();
                    Column0.Width = new GridLength(2, GridUnitType.Star);
                    Column1.Width = new GridLength(3, GridUnitType.Star);
                    Column2.Width = new GridLength(3, GridUnitType.Star);
                    Column3.Width = new GridLength(1, GridUnitType.Star);
                    Grid.ColumnDefinitions.Add(Column0);
                    Grid.ColumnDefinitions.Add(Column1);
                    Grid.ColumnDefinitions.Add(Column2);
                    Grid.ColumnDefinitions.Add(Column3);

                    RowDefinition Row0 = new RowDefinition();
                    RowDefinition Row1 = new RowDefinition();
                    RowDefinition Row2 = new RowDefinition();
                    RowDefinition Row3 = new RowDefinition();
                    RowDefinition Row4 = new RowDefinition();
                    RowDefinition Row5 = new RowDefinition();
                    RowDefinition Row6 = new RowDefinition();
                    Row0.Height = new GridLength(1, GridUnitType.Star);
                    Row1.Height = new GridLength(1, GridUnitType.Star);
                    Row2.Height = new GridLength(1, GridUnitType.Star);
                    Row3.Height = new GridLength(1, GridUnitType.Star);
                    Row4.Height = new GridLength(1, GridUnitType.Star);
                    Row5.Height = new GridLength(1, GridUnitType.Star);
                    Row6.Height = new GridLength(1, GridUnitType.Star);
                    Grid.RowDefinitions.Add(Row0);
                    Grid.RowDefinitions.Add(Row1);
                    Grid.RowDefinitions.Add(Row2);
                    Grid.RowDefinitions.Add(Row3);
                    Grid.RowDefinitions.Add(Row4);
                    Grid.RowDefinitions.Add(Row5);
                    Grid.RowDefinitions.Add(Row6);

                    TextBox Description = new TextBox();
                    Description.IsReadOnly = true;
                    Description.Text = Note.Description;
                    Grid.SetColumn(Description, 0);
                    Grid.SetColumnSpan(Description, 3);
                    Grid.SetRow(Description, 0);
                    Description.Name = $"Description_{Index}";
                    Description.Visibility = Visibility.Visible;

                    Button HideButton = new Button();
                    HideButton.Content = "Rozwiń";
                    Grid.SetColumn(HideButton, 3);
                    Grid.SetColumnSpan(HideButton, 1);
                    Grid.SetRow(HideButton, 0);
                    HideButton.Click += Hide_Click;
                    HideButton.Name = $"HideButton_{Index}";
                    HideButton.Visibility = Visibility.Visible;

                    TextBox LDistance = new TextBox();
                    LDistance.IsReadOnly = true;
                    LDistance.Text = "Dystans";
                    LDistance.Background = Brushes.LightGray;
                    Grid.SetColumn(LDistance, 0);
                    Grid.SetColumnSpan(LDistance, 1);
                    Grid.SetRow(LDistance, 1);
                    LDistance.Visibility = Visibility.Collapsed;

                    TextBox Distance = new TextBox();
                    Distance.IsReadOnly = true;
                    Distance.Text = Note.Distance.ToString();
                    Grid.SetColumn(Distance, 1);
                    Grid.SetColumnSpan(Distance, 2);
                    Grid.SetRow(Distance, 1);
                    Distance.Visibility = Visibility.Collapsed;

                    TextBox VDistance = new TextBox();
                    VDistance.IsReadOnly = true;
                    VDistance.Text = "km";
                    VDistance.Background = Brushes.LightGray;
                    Grid.SetColumn(VDistance, 3);
                    Grid.SetColumnSpan(VDistance, 1);
                    Grid.SetRow(VDistance, 1);
                    VDistance.Visibility = Visibility.Collapsed;
                    
                    TextBox LLitres = new TextBox();
                    LLitres.IsReadOnly = true;
                    LLitres.Text = "Dolano litrów";
                    LLitres.Background = Brushes.LightGray;
                    Grid.SetColumn(LLitres, 0);
                    Grid.SetColumnSpan(LLitres, 1);
                    Grid.SetRow(LLitres, 2);
                    LLitres.Visibility = Visibility.Collapsed;

                    TextBox Litres = new TextBox();
                    Litres.IsReadOnly = true;
                    Litres.Text = Note.Litres.ToString();
                    Grid.SetColumn(Litres, 1);
                    Grid.SetColumnSpan(Litres, 2);
                    Grid.SetRow(Litres, 2);
                    Litres.Visibility = Visibility.Collapsed;

                    TextBox VLitres = new TextBox();
                    VLitres.IsReadOnly = true;
                    VLitres.Text = "L";
                    VLitres.Background = Brushes.LightGray;
                    Grid.SetColumn(VLitres, 3);
                    Grid.SetColumnSpan(VLitres, 1);
                    Grid.SetRow(VLitres, 2);
                    VLitres.Visibility = Visibility.Collapsed;

                    TextBox LPC = new TextBox();
                    LPC.IsReadOnly = true;
                    LPC.Text = "Koszt / Cena";
                    LPC.Background = Brushes.LightGray;
                    Grid.SetColumn(LPC, 0);
                    Grid.SetColumnSpan(LPC, 1);
                    Grid.SetRow(LPC, 3);
                    LPC.Visibility = Visibility.Collapsed;

                    TextBox PPC = new TextBox();
                    PPC.IsReadOnly = true;
                    PPC.Text = Note.Price.ToString();
                    Grid.SetColumn(PPC, 1);
                    Grid.SetColumnSpan(PPC, 1);
                    Grid.SetRow(PPC, 3);
                    PPC.Visibility = Visibility.Collapsed;

                    TextBox CPC = new TextBox();
                    CPC.IsReadOnly = true;
                    CPC.Text = Note.Cost.ToString();
                    Grid.SetColumn(CPC, 2);
                    Grid.SetColumnSpan(CPC, 1);
                    Grid.SetRow(CPC, 3);
                    CPC.Visibility = Visibility.Collapsed;

                    TextBox VPC = new TextBox();
                    VPC.IsReadOnly = true;
                    VPC.Text = "zł (zł/l)";
                    VPC.Background = Brushes.LightGray;
                    Grid.SetColumn(VPC, 3);
                    Grid.SetColumnSpan(VPC, 1);
                    Grid.SetRow(VPC, 3);
                    VPC.Visibility = Visibility.Collapsed;

                    TextBox LConsumption = new TextBox();
                    LConsumption.IsReadOnly = true;
                    LConsumption.Text = "Spalanie";
                    LConsumption.Background = Brushes.LightGray;
                    Grid.SetColumn(LConsumption, 0);
                    Grid.SetColumnSpan(LConsumption, 1);
                    Grid.SetRow(LConsumption, 4);
                    LConsumption.Visibility = Visibility.Collapsed;

                    TextBox Consumption = new TextBox();
                    Consumption.IsReadOnly = true;
                    Consumption.Text = Note.Consumption.ToString();
                    Grid.SetColumn(Consumption, 1);
                    Grid.SetColumnSpan(Consumption, 2);
                    Grid.SetRow(Consumption, 4);
                    Consumption.Visibility = Visibility.Collapsed;

                    TextBox VConsumption = new TextBox();
                    VConsumption.IsReadOnly = true;
                    VConsumption.Text = "L/100km";
                    VConsumption.Background = Brushes.LightGray;
                    Grid.SetColumn(VConsumption, 3);
                    Grid.SetColumnSpan(VConsumption, 1);
                    Grid.SetRow(VConsumption, 4);
                    VConsumption.Visibility = Visibility.Collapsed;

                    TextBox LCycle = new TextBox();
                    LCycle.IsReadOnly = true;
                    LCycle.Text = "Cykl (miejski/trasa)";
                    LCycle.Background = Brushes.LightGray;
                    Grid.SetColumn(LCycle, 0);
                    Grid.SetColumnSpan(LCycle, 1);
                    Grid.SetRow(LCycle, 5);
                    LCycle.Visibility = Visibility.Collapsed;

                    TextBox CityCycle = new TextBox();
                    CityCycle.IsReadOnly = true;
                    CityCycle.Text = Note.Area_City.ToString();
                    Grid.SetColumn(CityCycle, 1);
                    Grid.SetColumnSpan(CityCycle, 1);
                    Grid.SetRow(CityCycle, 5);
                    CityCycle.Visibility = Visibility.Collapsed;

                    TextBox OpenCycle = new TextBox();
                    OpenCycle.IsReadOnly = true;
                    OpenCycle.Text = Note.Area_Open.ToString();
                    Grid.SetColumn(OpenCycle, 2);
                    Grid.SetColumnSpan(OpenCycle, 1);
                    Grid.SetRow(OpenCycle, 5);
                    OpenCycle.Visibility = Visibility.Collapsed;

                    TextBox VCycle = new TextBox();
                    VCycle.IsReadOnly = true;
                    VCycle.Text = "%";
                    VCycle.Background = Brushes.LightGray;
                    Grid.SetColumn(VCycle, 3);
                    Grid.SetColumnSpan(VCycle, 1);
                    Grid.SetRow(VCycle, 5);
                    VCycle.Visibility = Visibility.Collapsed;

                    Button DeleteButton = new Button();
                    DeleteButton.Content = "Usuń";
                    Grid.SetColumn(DeleteButton, 0);
                    Grid.SetColumnSpan(DeleteButton, 4);
                    Grid.SetRow(DeleteButton, 6);
                    DeleteButton.Click += Button_Click;
                    DeleteButton.Name = $"Button_{Index}";
                    DeleteButton.Visibility = Visibility.Collapsed;

                    Grid.Children.Add(Description);
                    Grid.Children.Add(HideButton);
                    Grid.Children.Add(LDistance);
                    Grid.Children.Add(Distance);
                    Grid.Children.Add(VDistance);
                    Grid.Children.Add(LLitres);
                    Grid.Children.Add(Litres);
                    Grid.Children.Add(VLitres);
                    Grid.Children.Add(LPC);
                    Grid.Children.Add(PPC);
                    Grid.Children.Add(CPC);
                    Grid.Children.Add(VPC);
                    Grid.Children.Add(LConsumption);
                    Grid.Children.Add(Consumption);
                    Grid.Children.Add(VConsumption);
                    Grid.Children.Add(LCycle);
                    Grid.Children.Add(CityCycle);
                    Grid.Children.Add(OpenCycle);
                    Grid.Children.Add(VCycle);
                    Grid.Children.Add(DeleteButton);

                    _StackPanel.Children.Add(Grid);
                    Index++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));

            foreach (Grid Grid in _StackPanel.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if(GridIndex==SenderIndex)
                {
                    Grid.Visibility = Visibility.Collapsed;
                    foreach(Control Control in Grid.Children)
                    {
                        if(Control.Name==$"Description_{SenderIndex}")
                        {
                            DB.RemoveFuelNote((Control as TextBox).Text);
                        }
                    }
                }

            }
        }
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));

            if((sender as Button).Content.ToString()=="Rozwiń")
            {
                (sender as Button).Content= "Zwiń";
                foreach (Grid Grid in _StackPanel.Children)
                {
                    int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                
                    if(GridIndex==SenderIndex)
                    {
                        foreach(Control Control in Grid.Children)
                        {
                            if (Control.Name == $"Description_{SenderIndex}" || Control.Name == $"HideButton_{SenderIndex}")
                                continue;
                            else
                                Control.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
            {
                (sender as Button).Content = "Rozwiń";
                foreach (Grid Grid in _StackPanel.Children)
                {
                    int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));

                    if (GridIndex == SenderIndex)
                    {
                        foreach (Control Control in Grid.Children)
                        {
                            if (Control.Name == $"Description_{SenderIndex}" || Control.Name == $"HideButton_{SenderIndex}")
                                continue;
                            //else
                                Control.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }
    }
}

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
    /// Interaction logic for MyGarage.xaml
    /// </summary>
    public partial class MyGarage : Page
    {
        DbReference DB = new DbReference();
        public MyGarage()
        {
            Car SelectedCar = DB.ReturnLastEntry();
            InitializeComponent();
            int Index = 0;
            foreach(Car Car in DB.CarDb)
            {
                if(Car.DisplayName == SelectedCar.DisplayName)
                {
                    int Counter = 1;
                    if (Car.IsFavourite == true)
                        Counter++;
                    for (int i = 0; i < Counter; i++)
                    {
                        Grid Grid = new Grid();
                        Grid.Name = $"Grid_{Index}";
                        ColumnDefinition Column1 = new ColumnDefinition();
                        Column1.Width = new GridLength(1, GridUnitType.Star);
                        ColumnDefinition Column2 = new ColumnDefinition();
                        Column2.Width = new GridLength(4, GridUnitType.Star);
                        ColumnDefinition Column3 = new ColumnDefinition();
                        Column3.Width = new GridLength(1, GridUnitType.Star);
                        ColumnDefinition Column4 = new ColumnDefinition();
                        Column4.Width = new GridLength(1, GridUnitType.Star);
                        ColumnDefinition Column5 = new ColumnDefinition();
                        Column5.Width = new GridLength(2, GridUnitType.Star);
                        Grid.ColumnDefinitions.Add(Column1);
                        Grid.ColumnDefinitions.Add(Column2);
                        Grid.ColumnDefinitions.Add(Column3);
                        Grid.ColumnDefinitions.Add(Column4);
                        Grid.ColumnDefinitions.Add(Column5);

                        TextBox PlateBox = new TextBox();
                        PlateBox.IsReadOnly = true;
                        PlateBox.Name = $"PlateNumber_{Index}";
                        PlateBox.Text = Car.PlateNumber;
                        Grid.SetColumn(PlateBox, 0);

                        TextBox NameBox = new TextBox();
                        NameBox.IsReadOnly = true;
                        NameBox.Name = $"Name_{Index}";
                        NameBox.Text = $"{Car.Manufacturer} {Car.Model} {Car.Year}; {Car.FuelType}, bak {Car.FuelCapacity}L, VIN: {Car.VinNumber} ";
                        Grid.SetColumn(NameBox, 1);

                        Button ChooseButton = new Button();
                        ChooseButton.Content = "Wybierz";
                        ChooseButton.Name = $"Choose_{Index}";
                        ChooseButton.Click += Choose_Click;
                        Grid.SetColumn(ChooseButton, 2);

                        Button RemoveButton = new Button();
                        RemoveButton.Content = "Usuń";
                        RemoveButton.Name = $"Remove_{Index}";
                        RemoveButton.Click += Remove_Click;
                        Grid.SetColumn(RemoveButton, 3);

                        Button FavouriteButton = new Button();
                        FavouriteButton.Name = $"Favourite_{Index}";
                        Grid.SetColumn(FavouriteButton, 4);

                        Grid.Children.Add(PlateBox);
                        Grid.Children.Add(NameBox);
                        Grid.Children.Add(ChooseButton);
                        Grid.Children.Add(RemoveButton);
                        Grid.Children.Add(FavouriteButton);

                        if (i==0)
                        {
                            if(Car.IsFavourite==true)
                            {
                                FavouriteButton.Content = "Usuń z Ulubionych";
                                FavouriteButton.Click += Favourite_Remove_Click;
                            }
                            else
                            {
                                FavouriteButton.Content = "Dodaj do Ulubionych";
                                FavouriteButton.Click += Favourite_Add_Click;
                            }
                            _Standard.Children.Add(Grid);
                        }
                        if(i==1 && Car.IsFavourite == true)
                        {
                            FavouriteButton.Content = "Usuń z Ulubionych";
                            FavouriteButton.Click += Favourite_Remove_Click;
                            _Favourite.Children.Add(Grid);
                        }
                    }
                    Index++;
                }

            }
            
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            string Plate = "";
            foreach (Grid Grid in _Standard.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                {
                    foreach (Control Control in Grid.Children)
                    {
                        if (Control is TextBox)
                        {
                            if (Control.Name == $"PlateNumber_{GridIndex}")
                                Plate = (Control as TextBox).Text; 
                        }
                    }
                }
            }

            Car newCar = new Car();
            newCar = DB.ReturnByPlateNumber(Plate);
            DB.RemoveByPlate(Plate);
            newCar.SaveToDb(DB);
            DB.RemoveGenerics();

            Window Reset = new UserPanel();
            Reset.Show();
            Window.GetWindow(this).Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            string Plate = "";
            foreach (Grid Grid in _Standard.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if(GridIndex==SenderIndex)
                {
                    foreach(Control Control in Grid.Children)
                    {
                        if (Control.Name == $"PlateNumber_{GridIndex}")
                            Plate = (Control as TextBox).Text;
                    }
                    Grid.Visibility = Visibility.Collapsed;
                }
            }
            foreach(Grid Grid in _Favourite.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                    Grid.Visibility = Visibility.Collapsed;
            }
            DB.RemoveAllByPlate(Plate);

            int Cnt = 0;
            foreach (Car _ in DB.CarDb)
                Cnt++;
            if (Cnt == 0)
            {
                MessageBox.Show("Lista jest pusta, konto zostanie usunięte");
                Window Main = new MainWindow();
                Main.Show();
                Window.GetWindow(this).Close();
            }

        }


        private void Favourite_Add_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            string Plate="";
            foreach (Grid Grid in _Standard.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                {
                    foreach (Control Control in Grid.Children)
                    {
                        if (Control.Name == $"PlateNumber_{GridIndex}")
                            Plate = (Control as TextBox).Text;
                    }


                    int Index = GridIndex;
                    

                    Car Car = DB.ReturnByPlateNumber(Plate);
                    Grid NewGrid = new Grid();
                    NewGrid.Name = $"Grid_{Index}";
                    ColumnDefinition Column1 = new ColumnDefinition();
                    Column1.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column2 = new ColumnDefinition();
                    Column2.Width = new GridLength(4, GridUnitType.Star);
                    ColumnDefinition Column3 = new ColumnDefinition();
                    Column3.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column4 = new ColumnDefinition();
                    Column4.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column5 = new ColumnDefinition();
                    Column5.Width = new GridLength(2, GridUnitType.Star);
                    NewGrid.ColumnDefinitions.Add(Column1);
                    NewGrid.ColumnDefinitions.Add(Column2);
                    NewGrid.ColumnDefinitions.Add(Column3);
                    NewGrid.ColumnDefinitions.Add(Column4);
                    NewGrid.ColumnDefinitions.Add(Column5);

                    TextBox PlateBox = new TextBox();
                    PlateBox.IsReadOnly = true;
                    PlateBox.Name = $"PlateNumber_{Index}";
                    PlateBox.Text = Car.PlateNumber;
                    Grid.SetColumn(PlateBox, 0);

                    TextBox NameBox = new TextBox();
                    NameBox.IsReadOnly = true;
                    NameBox.Name = $"Name_{Index}";
                    NameBox.Text = $"{Car.Manufacturer} {Car.Model} {Car.Year}; {Car.FuelType}, bak {Car.FuelCapacity}L, VIN: {Car.VinNumber}";
                    Grid.SetColumn(NameBox, 1);

                    Button ChooseButton = new Button();
                    ChooseButton.Content = "Wybierz";
                    ChooseButton.Name = $"Choose_{Index}";
                    ChooseButton.Click += Choose_Click;
                    Grid.SetColumn(ChooseButton, 2);

                    Button RemoveButton = new Button();
                    RemoveButton.Content = "Usuń";
                    RemoveButton.Name = $"Remove_{Index}";
                    RemoveButton.Click += Remove_Click;
                    Grid.SetColumn(RemoveButton, 3);

                    Button FavouriteButton = new Button();
                    FavouriteButton.Name = $"Favourite_{Index}";
                    FavouriteButton.Click += Favourite_Remove_Click;
                    FavouriteButton.Content = "Usuń z Ulubionych";
                    Grid.SetColumn(FavouriteButton, 4);

                    NewGrid.Children.Add(PlateBox);
                    NewGrid.Children.Add(NameBox);
                    NewGrid.Children.Add(ChooseButton);
                    NewGrid.Children.Add(RemoveButton);
                    NewGrid.Children.Add(FavouriteButton);
                    _Favourite.Children.Add(NewGrid);
                    
                }
            }
            (sender as Button).Content = "Usuń z Ulubionych";
            (sender as Button).Click -= Favourite_Add_Click;
            (sender as Button).Click += Favourite_Remove_Click;
            
            

            DB.ChangeFavouriteStatus(Plate);
        }
        private void Favourite_Remove_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            string Plate = "";
            foreach (Grid Grid in _Favourite.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                {
                    foreach (Control Control in Grid.Children)
                    {
                        if (Control.Name == $"PlateNumber_{GridIndex}")
                            Plate = (Control as TextBox).Text;
                    }

                    DB.ChangeFavouriteStatus(Plate);
                    Grid.Visibility = Visibility.Collapsed;

                    foreach(Grid NewGrid in _Standard.Children)
                    {
                        //int NewGridIndex = int.Parse(NewGrid.Name.Substring(NewGrid.Name.IndexOf("_") + 1));
                        //if(NewGridIndex == GridIndex)
                        foreach (Control Control in NewGrid.Children)
                        {
                            bool tmp = false;
                            if (Control == sender)
                            {
                                (sender as Button).Content = "Dodaj do Ulubionych";
                                (sender as Button).Click -= Favourite_Remove_Click;
                                (sender as Button).Click += Favourite_Add_Click;
                                tmp = true;
                            }
                            else if (Control.Name == $"Favourite_{GridIndex}" && DB.ReturnByPlateNumber(Plate).IsFavourite==false && tmp==false)
                            {
                                (Control as Button).Content = "Dodaj do Ulubionych";
                                (Control as Button).Click -= Favourite_Remove_Click;
                                (Control as Button).Click += Favourite_Add_Click;
                                
                            }
                        }
                    }
                }
            }
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            DB.RemoveIncomplete();
            Car UserData = DB.ReturnLastEntry();
            
            if((sender as Button).Content.ToString() != "Potwierdź")
            {
                Car newCar = new Car(UserData.DisplayName,UserData.Password);
                newCar.SaveToDb(DB);

                (sender as Button).Content = "Potwierdź";
                DisplayFrame.Content = new CarCreationFrame();
            }
            else
            {
                DisplayFrame.Content = null;
                (sender as Button).Content = "Dodaj nowy samochód";
                Car Car = new Car();
                Car = DB.ReturnLastEntry();

                bool breakcheck = false;
                foreach(Grid G in _Standard.Children)
                {
                    int GI = int.Parse(G.Name.Substring(G.Name.IndexOf("_") + 1));
                    foreach (Control C in G.Children)
                        if (C.Name == $"PlateNumber_{GI}")
                            if ((C as TextBox).Text == Car.PlateNumber)
                                breakcheck = true;
                    
                }

                if (breakcheck == false)
                {
                    int Index = 1;
                    foreach (Car _ in DB.CarDb)
                        Index++;


                    Grid Grid = new Grid();
                    Grid.Name = $"Grid_{Index}";
                    ColumnDefinition Column1 = new ColumnDefinition();
                    Column1.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column2 = new ColumnDefinition();
                    Column2.Width = new GridLength(4, GridUnitType.Star);
                    ColumnDefinition Column3 = new ColumnDefinition();
                    Column3.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column4 = new ColumnDefinition();
                    Column4.Width = new GridLength(1, GridUnitType.Star);
                    ColumnDefinition Column5 = new ColumnDefinition();
                    Column5.Width = new GridLength(2, GridUnitType.Star);
                    Grid.ColumnDefinitions.Add(Column1);
                    Grid.ColumnDefinitions.Add(Column2);
                    Grid.ColumnDefinitions.Add(Column3);
                    Grid.ColumnDefinitions.Add(Column4);
                    Grid.ColumnDefinitions.Add(Column5);

                    TextBox PlateBox = new TextBox();
                    PlateBox.IsReadOnly = true;
                    PlateBox.Name = $"PlateNumber_{Index}";
                    PlateBox.Text = Car.PlateNumber;
                    Grid.SetColumn(PlateBox, 0);

                    TextBox NameBox = new TextBox();
                    NameBox.IsReadOnly = true;
                    NameBox.Name = $"Name_{Index}";
                    NameBox.Text = $"{Car.Manufacturer} {Car.Model} {Car.Year}; {Car.FuelType}, bak {Car.FuelCapacity}L, VIN: {Car.VinNumber}";
                    Grid.SetColumn(NameBox, 1);

                    Button ChooseButton = new Button();
                    ChooseButton.Content = "Wybierz";
                    ChooseButton.Name = $"Choose_{Index}";
                    ChooseButton.Click += Choose_Click;
                    Grid.SetColumn(ChooseButton, 2);

                    Button RemoveButton = new Button();
                    RemoveButton.Content = "Usuń";
                    RemoveButton.Name = $"Remove_{Index}";
                    RemoveButton.Click += Remove_Click;
                    Grid.SetColumn(RemoveButton, 3);

                    Button FavouriteButton = new Button();
                    FavouriteButton.Name = $"Favourite_{Index}";
                    Grid.SetColumn(FavouriteButton, 4);

                    Grid.Children.Add(PlateBox);
                    Grid.Children.Add(NameBox);
                    Grid.Children.Add(ChooseButton);
                    Grid.Children.Add(RemoveButton);
                    Grid.Children.Add(FavouriteButton);


                    FavouriteButton.Content = "Dodaj do Ulubionych";
                    FavouriteButton.Click += Favourite_Add_Click;

                    _Standard.Children.Add(Grid);
                }
            }


        }
    }
}

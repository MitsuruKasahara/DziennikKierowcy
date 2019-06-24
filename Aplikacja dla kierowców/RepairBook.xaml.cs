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
    /// Interaction logic for RepairBook.xaml
    /// </summary>
    public partial class RepairBook : Page
    {
        DbReference DB = new DbReference();
        public RepairBook()
        {
            InitializeComponent();
            Car SelectedCar = DB.ReturnLastEntry();

            int IndexA = 0;
            int IndexB = 0;
            foreach(Repairbook Entry in DB.RepairDb)
            {
                if(Entry.Active==false && SelectedCar.PlateNumber==Entry.Plate)
                {
                    Grid Grid = new Grid();
                    Grid.Name = $"Grid_{IndexA}";
                    ColumnDefinition Column1 = new ColumnDefinition
                    {
                        Width = new GridLength(5, GridUnitType.Star)
                    };
                    ColumnDefinition Column2 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    };
                    ColumnDefinition Column3 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    };

                    Grid.ColumnDefinitions.Add(Column1);
                    Grid.ColumnDefinitions.Add(Column2);
                    Grid.ColumnDefinitions.Add(Column3);

                    TextBox TextBox = new TextBox();
                    TextBox.Text = Entry.Text;
                    TextBox.Name = $"TextBox_{IndexA}";
                    TextBox.Background = Brushes.LightGray;
                    TextBox.IsReadOnly = true;
                    Grid.SetColumn(TextBox, 0);

                    TextBox Price = new TextBox();
                    Price.Text = Entry.Price.ToString();
                    Price.Name = $"Price_{IndexA}";
                    Price.Background = Brushes.LightGray;
                    Price.IsReadOnly = true;
                    Grid.SetColumn(Price, 1);
                    
                    Button Button = new Button();
                    Button.Content = "Usuń";
                    Button.Name = $"RemoveButton_{IndexA}";
                    Button.Click += Remove_A_Click;
                    Grid.SetColumn(Button, 2);

                    Grid.Children.Add(TextBox);
                    Grid.Children.Add(Button);
                    Grid.Children.Add(Price);
                    _Repaired.Children.Add(Grid);
                    IndexA++;
                }
                if(Entry.Active==true && SelectedCar.PlateNumber == Entry.Plate)
                {
                    Grid Grid = new Grid();
                    Grid.Name = $"Grid_{IndexB}";
                    ColumnDefinition Column1 = new ColumnDefinition
                    {
                        Width = new GridLength(4, GridUnitType.Star)
                    };
                    ColumnDefinition Column2 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    };
                    ColumnDefinition Column3= new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    };
                    ColumnDefinition Column4 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    };
                    ColumnDefinition Column5 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    };
                    Grid.ColumnDefinitions.Add(Column1);
                    Grid.ColumnDefinitions.Add(Column2);
                    Grid.ColumnDefinitions.Add(Column3);
                    Grid.ColumnDefinitions.Add(Column4);
                    Grid.ColumnDefinitions.Add(Column5);

                    TextBox TextBox = new TextBox();
                    TextBox.Text = Entry.Text;
                    TextBox.IsReadOnly = true;
                    TextBox.Background = Brushes.LightGray;
                    TextBox.Name = $"TextBox_{IndexB}";

                    TextBox Price = new TextBox();
                    Price.Text = Entry.Price.ToString();
                    Price.Name = $"Price_{IndexB}";
                    Price.Background = Brushes.LightGray;
                    Price.IsReadOnly = true;

                    Button Edit = new Button();
                    Edit.Content = "Edytuj";
                    Edit.Name = $"Edit_{IndexB}";
                    Edit.Click += Edit_Click;

                    Button Remove = new Button();
                    Remove.Content = "Usuń";
                    Remove.Name = $"Remove_{IndexB}";
                    Remove.Click += Remove_B_Click;

                    Button Shuffle = new Button();
                    Shuffle.Content = "Naprawione";
                    Shuffle.Name = $"Shuffle_{IndexB}";
                    Shuffle.Click += Shuffle_Click;

                    Grid.SetColumn(TextBox, 0);
                    Grid.SetColumn(Price, 1);
                    Grid.SetColumn(Edit, 2);
                    Grid.SetColumn(Remove, 3);
                    Grid.SetColumn(Shuffle, 4);

                    Grid.Children.Add(TextBox);
                    Grid.Children.Add(Price);
                    Grid.Children.Add(Edit);
                    Grid.Children.Add(Remove);
                    Grid.Children.Add(Shuffle);
                    _WIP.Children.Add(Grid);

                    IndexB++;
                }
            }
        }

        private void Remove_A_Click(object sender, RoutedEventArgs e)
        {

            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") +1));
            foreach(Grid Grid in _Repaired.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                {
                    Grid.Visibility = Visibility.Collapsed;
                    Car SelectedCar = DB.ReturnLastEntry();
                    string Text = "";
                    string Price = "";
                    foreach(Control Control in Grid.Children)
                    {
                        if(Control is TextBox)
                        {
                            if (Control.Name == $"TextBox_{GridIndex}")
                                Text = (Control as TextBox).Text;
                            if (Control.Name == $"Price_{GridIndex}")
                                Price = (Control as TextBox).Text;
                        }
                    }
                    DB.RemoveRepairNote(SelectedCar.PlateNumber, Text, Price);
                }
            }
        }
        private void Remove_B_Click(object sender, RoutedEventArgs e)
        {

            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            foreach (Grid Grid in _WIP.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if (GridIndex == SenderIndex)
                {
                    Grid.Visibility = Visibility.Collapsed;
                    Car SelectedCar = DB.ReturnLastEntry();
                    string Text = "";
                    string Price = "";
                    foreach (Control Control in Grid.Children)
                    {
                        if (Control is TextBox)
                        {
                            if (Control.Name == $"TextBox_{GridIndex}")
                                Text = (Control as TextBox).Text;
                            if (Control.Name == $"Price_{GridIndex}")
                                Price = (Control as TextBox).Text;
                        }
                    }
                    DB.RemoveRepairNote(SelectedCar.PlateNumber, Text, Price);
                }
            }

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));
            if ((sender as Button).Content.ToString() == "Edytuj") //<SENDER=="EDYTUJ"? >
            {
                foreach (Grid Grid in _WIP.Children)
                {
                    int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));

                    if (GridIndex == SenderIndex)
                    {
                        foreach (Control Control in Grid.Children)
                        {
                            (sender as Button).Content = "Zmiana";
                            if (Control is TextBox)
                            {
                                Control.Background = Brushes.White;
                                (Control as TextBox).IsReadOnly = false;

                                if (Control.Name == $"Price_{GridIndex}")
                                {
                                    TextBlock TextBlock = new TextBlock();
                                    TextBlock.Text = (Control as TextBox).Text;
                                    TextBlock.Name = $"Price_{GridIndex}";
                                    _Backup.Children.Add(TextBlock);
                                }
                                else if (Control.Name == $"TextBox_{GridIndex}")
                                {
                                    TextBlock TextBlock = new TextBlock();
                                    TextBlock.Text = (Control as TextBox).Text;
                                    TextBlock.Name = $"TextBox_{GridIndex}";
                                    _Backup.Children.Add(TextBlock);
                                }
                            }
                        }
                    }
                }
            }                                                               //<SENDER=="EDYTUJ"? />
            else if ((sender as Button).Content.ToString() == "Zmiana")     //<SENDER=="ZMIANA"? >
            {
                foreach (Grid Grid in _WIP.Children)
                {
                    int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                    if (GridIndex == SenderIndex)
                    {
                        string OldNote = "";
                        string NewNote = "";
                        string OldPrice = "";
                        string NewPrice = "";

                        foreach (Control Control in Grid.Children)
                        {
                            (sender as Button).Content = "Edytuj";
                            if (Control is TextBox)
                            {
                                Control.Background = Brushes.LightGray;
                                (Control as TextBox).IsReadOnly = true;

                                if ((Control as TextBox).Name == $"TextBox_{GridIndex}")
                                {
                                    NewNote = (Control as TextBox).Text;
                                }
                                if ((Control as TextBox).Name == $"Price_{GridIndex}")
                                {
                                    NewPrice = (Control as TextBox).Text;

                                    foreach (TextBlock Backup in _Backup.Children)
                                    {
                                        if ((Backup as TextBlock).Name == $"TextBox_{GridIndex}")
                                        {
                                            OldNote = (Backup as TextBlock).Text;
                                        }
                                        if ((Backup as TextBlock).Name == $"Price_{GridIndex}")
                                        {
                                            OldPrice = (Backup as TextBlock).Text;
                                        }
                                    }
                                }
                            }
                        }
                        DB.ChangeRepairNote(DB.ReturnLastEntry().PlateNumber, OldNote, Convert.ToDecimal(OldPrice), NewNote, Convert.ToDecimal(NewPrice));
                    }
                }
            }
        }
        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            int SenderIndex = int.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("_") + 1));

            int _RepairedIndex = 1;
            foreach (Grid _ in _Repaired.Children)
                _RepairedIndex++;


            foreach (Grid Grid in _WIP.Children)
            {
                int GridIndex = int.Parse(Grid.Name.Substring(Grid.Name.IndexOf("_") + 1));
                if(GridIndex==SenderIndex)
                {
                    Grid NewGrid = new Grid();
                    NewGrid.Name = $"Grid_{_RepairedIndex}";
                    ColumnDefinition Column1 = new ColumnDefinition
                    {
                        Width = new GridLength(5, GridUnitType.Star)
                    };
                    ColumnDefinition Column2 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    };
                    ColumnDefinition Column3 = new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    };
                    NewGrid.ColumnDefinitions.Add(Column1);
                    NewGrid.ColumnDefinitions.Add(Column2);
                    NewGrid.ColumnDefinitions.Add(Column3);


                    string Plate = DB.ReturnLastEntry().PlateNumber;
                    string Note = "";
                    string Price = "";
                    foreach(Control Control in Grid.Children)
                    {
                        if(Control is TextBox)
                        {
                            if ((Control as TextBox).Name == $"TextBox_{GridIndex}")
                            {
                                Note = (Control as TextBox).Text;

                                TextBox NewBoxA = new TextBox();
                                NewBoxA.Text = (Control as TextBox).Text;
                                NewBoxA.IsReadOnly = true;
                                NewBoxA.Background = Brushes.LightGray;
                                NewBoxA.Name = $"TextBox_{_RepairedIndex}";
                                Grid.SetColumn(NewBoxA, 0);
                                NewGrid.Children.Add(NewBoxA);
                            }
                            if ((Control as TextBox).Name == $"Price_{GridIndex}")
                                Price = (Control as TextBox).Text;
                                
                                TextBox NewBox = new TextBox();
                                NewBox.Text = (Control as TextBox).Text; ;
                                NewBox.IsReadOnly = true;
                                NewBox.Background = Brushes.LightGray;
                                NewBox.Name = $"Price_{_RepairedIndex}";
                                Grid.SetColumn(NewBox, 1);
                                NewGrid.Children.Add(NewBox);
                        }
                    }
                    DB.ActivateRepairNote(Plate, Note, Price);
                    Grid.Visibility = Visibility.Collapsed;

                    Button Button = new Button();
                    Button.Content = "Usuń";
                    Button.Name = $"RemoveButton_{_RepairedIndex}";
                    Button.Click += Remove_A_Click;
                    Grid.SetColumn(Button, 2);
                    NewGrid.Children.Add(Button);
                    _Repaired.Children.Add(NewGrid);
                }
            }
        }
    }
}

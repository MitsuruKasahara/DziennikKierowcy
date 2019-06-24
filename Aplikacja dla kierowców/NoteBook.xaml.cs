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
    /// Interaction logic for NoteBook.xaml
    /// </summary>
    public partial class NoteBook : Page
    {
        DbReference DB = new DbReference();
        public NoteBook()
        {
            InitializeComponent();
            Car SelectedCar = DB.ReturnLastEntry();
            this.DataContext = SelectedCar;
            int Index = 0;
            


            TextBox[] TextBox = new TextBox[DB.NotebookDb.Count()];
            Button[] EditButton = new Button[DB.NotebookDb.Count()];
            Button[] RemoveButton = new Button[DB.NotebookDb.Count()];
            

            foreach (var NotepadEntry in DB.NotebookDb)
            {
                if (NotepadEntry.Plate == SelectedCar.PlateNumber)
                {
                    ColumnDefinition Column1 = new ColumnDefinition();
                    Column1.Width = new GridLength(1,GridUnitType.Star);
                    ColumnDefinition Column2 = new ColumnDefinition();
                    Column2.Width = new GridLength(1, GridUnitType.Auto);
                    ColumnDefinition Column3 = new ColumnDefinition();
                    Column3.Width = new GridLength(1, GridUnitType.Auto);
                    Grid Grid = new Grid();
                    Grid.ColumnDefinitions.Add(Column1);
                    Grid.ColumnDefinitions.Add(Column2);
                    Grid.ColumnDefinitions.Add(Column3);

                    TextBox[Index] = new TextBox();
                    TextBox[Index].Background = Brushes.LightGray;
                    TextBox[Index].Text = NotepadEntry.Text;
                    TextBox[Index].IsReadOnly = true;
                    TextBox[Index].Name = $"TextBox_{Index}";
                    Grid.SetColumn(TextBox[Index], 0);


                    EditButton[Index] = new Button();
                    EditButton[Index].Content = "Edytuj";
                    EditButton[Index].Name = $"Edit_{Index}";
                    EditButton[Index].Click += new RoutedEventHandler(Edit_Click);                    
                    Grid.SetColumn(EditButton[Index], 1);
                    
                    RemoveButton[Index] = new Button();
                    RemoveButton[Index].Content = "Usuń";
                    RemoveButton[Index].Name = $"Remove_{Index}";
                    RemoveButton[Index].Click += new RoutedEventHandler(Remove_Click);
                    Grid.SetColumn(RemoveButton[Index], 2);

                    Grid.SetRow(TextBox[Index], Index);
                    Grid.SetRow(EditButton[Index], Index);
                    Grid.SetRow(RemoveButton[Index], Index);
                    
                    
                    Grid.Children.Add(TextBox[Index]);
                    Grid.Children.Add(EditButton[Index]);
                    Grid.Children.Add(RemoveButton[Index]);
                
                    _StackPanel.Children.Add(Grid);
                    

                    Index++;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            foreach(Grid MainGrid in _StackPanel.Children)
            {
                foreach (Control it in MainGrid.Children)
                    if (it is TextBox)
                    {
                        string StringedIndex = (sender as Button).Name;

                        StringedIndex = StringedIndex.Substring(StringedIndex.IndexOf("_") + 1);
                        int Index = Int32.Parse(StringedIndex);
                        if (it.Name == $"TextBox_{Index}" && (sender as Button).Name == $"Edit_{Index}")
                        {
                            if((sender as Button).Content.ToString() == "Edytuj")
                            {
                                (it as TextBox).IsReadOnly = false;
                                (it as TextBox).Background = Brushes.White;
                                (sender as Button).Content = "Zapisz";

                                TextBlock Backup = new TextBlock();
                                Backup.Text = (it as TextBox).Text.ToString();
                                Backup.Visibility = Visibility.Visible;
                                Backup.Name = $"Backup_{Index}";
                                _Backup.Children.Add(Backup);
                            }
                            else if((sender as Button).Content.ToString() == "Zapisz")
                            {
                                Car SelectedCar = DB.ReturnLastEntry();
                                foreach (TextBlock BackupText in _Backup.Children)
                                {
                                    if ( BackupText.Name == $"Backup_{Index}")
                                    {
                                        DB.ChangeNote(SelectedCar.PlateNumber, BackupText.Text.ToString() , (it as TextBox).Text.ToString());
                                        (it as TextBox).IsReadOnly = true;
                                        (it as TextBox).Background = Brushes.LightGray;
                                        (sender as Button).Content = "Edytuj";
                                    }
                                }
                            }
                        } 
                    }
            }
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            foreach(Grid Grid in _StackPanel.Children)
            {
                foreach(Control Control in Grid.Children)
                {
                    if(Control is TextBox)
                    {
                        string StringedIndex = (sender as Button).Name;
                        StringedIndex = StringedIndex.Substring(StringedIndex.IndexOf("_") + 1);
                        int Index = Int32.Parse(StringedIndex);

                        if(Control.Name==$"TextBox_{Index}")
                        {
                            Car SelectedCar = DB.ReturnLastEntry();
                            DB.RemoveNote(SelectedCar.PlateNumber, (Control as TextBox).Text);
                            Grid.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }

        }
    }
}

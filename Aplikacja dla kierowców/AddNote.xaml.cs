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
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Page
    {
        DbReference DB = new DbReference();
        public AddNote()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Notebook Note = new Notebook(DB.ReturnLastEntry().PlateNumber, TextBox.Text);
            string tmp = TextBox.Text;
            TextBox.Text = "";
            Note.SaveToDb(DB);

            TextBlock Info = new TextBlock
            {
                Text = $"Dodano notatkę: {tmp}"
            };
            _StackPanel.Children.Add(Info);

        }
    }
}

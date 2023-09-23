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
using System.Configuration;
using System.Data.Common;

namespace Connection_with_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repostortemployee employeerepostory = new Repostortemployee();
        public MainWindow()
        {
            InitializeComponent();
            DataGridview1.ItemsSource = employeerepostory.getAll();
        }

        private void ADbtn(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Firtxt.Text) && !string.IsNullOrEmpty(Lasttxt.Text))
            {
                employeerepostory.ADD(new Employee() { FirstName = Firtxt.Text, LastName = Lasttxt.Text });
                Firtxt.Text = string.Empty;
                Lasttxt.Text = string.Empty;
            }
            DataGridview1.ItemsSource = employeerepostory.getAll();
        }

        private void SelcetItem(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridview1.SelectedItems.Count > 0)
            {
                var row = DataGridview1.SelectedItems[0];
                var emplyoee = (Employee)row;
                LableId.Content = emplyoee.ID.ToString();
                TXTF.Text = emplyoee.FirstName;
                TXTL.Text = emplyoee.LastName;
            }
        }

        private void UDbtn(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TXTF.Text) && !string.IsNullOrEmpty(TXTL.Text))
            {
                employeerepostory.UPdate(new Employee()
                {
                    ID = int.Parse(LableId.Content.ToString().Trim()),
                    FirstName = TXTF.Text.Trim(),
                    LastName = TXTL.Text.Trim()
                });
                TXTF.Text = string.Empty;
                TXTL.Text = string.Empty;
                DataGridview1.ItemsSource = employeerepostory.getAll();
            }
           
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LableId.Content.ToString())) ;
            {
                employeerepostory.Delete(int.Parse(LableId.Content.ToString()));
                
            }
            TXTF.Text = string.Empty;
            TXTL.Text = string.Empty;
            DataGridview1.ItemsSource = employeerepostory.getAll();
        }
    }
   
}

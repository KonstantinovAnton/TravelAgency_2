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

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для WindowEditLoginAndPassword.xaml
    /// </summary>
    public partial class WindowEditLoginAndPassword : Window
    {
        public WindowEditLoginAndPassword()
        {
            InitializeComponent();
        }

        private void buttonGotoPageUserMenu_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

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

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для PageUserMenu.xaml
    /// </summary>
    public partial class PageUserMenu : Page
    {
        public PageUserMenu()
        {
            InitializeComponent();


            var userObj = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

      
            textBlockName.Text = userObj.name;
            textBlockSurname.Text = userObj.surname;
            textBlockLogin.Text = userObj.login;
            textBlockBirthday.Text = Convert.ToString(userObj.birthday);


        }

        private void buttonEditData_Click(object sender, RoutedEventArgs e)
        {
           
            WindowEditPersonalData windowEditPersonalData = new WindowEditPersonalData();
            windowEditPersonalData.Show();

            Application.Current.MainWindow.Hide();

        }

        private void buttonEditLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowEditLoginAndPassword windowEditLoginAndPassword = new WindowEditLoginAndPassword();
            windowEditLoginAndPassword.Show();

            Application.Current.MainWindow.Hide();

        }
    }
}

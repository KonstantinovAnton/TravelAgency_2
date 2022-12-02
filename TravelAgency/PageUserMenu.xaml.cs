using System;
using System.Collections.Generic;
using System.IO;
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

        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }

        public PageUserMenu()
        {

            
            InitializeComponent();

           

            if (GlobalValues.role == 1)
                buttonGotoMenu.Visibility = Visibility.Visible;

            var userObj = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

            var photo = Base.EM.PhotoUser.FirstOrDefault(x => x.id_photo == userObj.id_photo);

            if (photo != null)
            {
                byte[] Bar = photo.phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imagePhotoUser);  // отображаем картинку
            }




            textBlockName.Text = userObj.name;
            textBlockSurname.Text = userObj.surname;
            textBlockLogin.Text = userObj.login;
            textBlockBirthday.Text = Convert.ToString(userObj.birthday);

            


        }



        private void buttonEditData_Click(object sender, RoutedEventArgs e)
        {

            WindowEditPersonalData windowEditPersonalData = new WindowEditPersonalData();

            windowEditPersonalData.Show();

            foreach (Window w in App.Current.Windows)
            {
                if(w != windowEditPersonalData)
                w.Close();
            }
           
        }

        private void buttonEditLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowEditLoginAndPassword windowEditLoginAndPassword = new WindowEditLoginAndPassword();

            //  foreach (Window w in App.Current.Windows)
            //    w.Hide();


            App.Current.MainWindow.Close();
            windowEditLoginAndPassword.Show();

            

        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserMenu());
        }

        private void buttonGotoMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }

        private void buttonEditPhoto_Click(object sender, RoutedEventArgs e)
        {
            WindowEditPhoto windowEditPhoto = new WindowEditPhoto();

            App.Current.MainWindow.Close();

            windowEditPhoto.Show();

           
        }

       
    }
}

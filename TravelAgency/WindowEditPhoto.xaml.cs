using Microsoft.Win32;
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
using System.Drawing;
using System.IO;

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для WindowEditPhoto.xaml
    /// </summary>
    public partial class WindowEditPhoto : Window
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

        PhotoUser[] arrayPhotoUser;
        int photoCount;
        int currentPhotoIndex;
     

        public WindowEditPhoto()
        {
            InitializeComponent();

            arrayPhotoUser = Base.EM.PhotoUser.Where(x => x.id_user == GlobalValues.id_user).ToArray();
            photoCount = arrayPhotoUser.Length;
            currentPhotoIndex = 0;
            

          

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[0].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }
        }

        private void buttonGotoPageUserMenu_Click(object sender, RoutedEventArgs e)
        {



            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void buttonChooseNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoUser photoUser = new PhotoUser();  // создание объекта для добавления записи в таблицу, где хранится фото
            photoUser.id_user = GlobalValues.id_user;  // присваиваем значение полю idUser (id авторизованного пользователя)

            OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                                                        //OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
            OFD.ShowDialog();  // открываем диалоговое окно  
            

            string path = OFD.FileName;  // считываем путь выбранного изображения

            try
            {
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу

                ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                photoUser.phot = Barray;  // заполяем поле photoBinary полученным байтовым массивом
            }
            catch
            {
                return;
            }

            Base.EM.PhotoUser.Add(photoUser);
            Base.EM.SaveChanges();


            MessageBox.Show("Фото добавлено");
            

            
        }

        private void buttonChooseOldPhoto_Click(object sender, RoutedEventArgs e)
        {
            User user = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

            user.id_photo = arrayPhotoUser[currentPhotoIndex].id_photo;

            Base.EM.SaveChanges();

            MessageBox.Show("Фото изменено");


        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            currentPhotoIndex--;
            if (currentPhotoIndex == -1)
            {
                currentPhotoIndex = photoCount-1;
            }

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[currentPhotoIndex].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            currentPhotoIndex++;
            if (currentPhotoIndex == photoCount)
            {
                currentPhotoIndex = 0;
            }

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[currentPhotoIndex].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }

        }
    }
}

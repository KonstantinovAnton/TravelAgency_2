using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
        }

        private void btnRegistrate_Click(object sender, RoutedEventArgs e) // Добавления пользователя в БД
        {

            try
            {

                if (isNullText(textBoxSurname, "Фамилия")) return;
                if (isNullText(textBoxName, "Имя")) return;
                if (isNullText(textBoxLogin, "Логин")) return;


                if (isNullPass(passBox, "пароль")) return;
                if (isNullPass(passBoxRepeatPass, "пароль ещё раз")) return;

                if (passBox.Password != passBoxRepeatPass.Password)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }

                int gender;

                if ((bool)radioBtnMale.IsChecked || (bool)radioBtnFemale.IsChecked)
                {
                    if ((bool)radioBtnMale.IsChecked)
                        gender = 1;
                    else gender = 2;
                }
                else
                {
                    MessageBox.Show("Выберите пол");
                    return;
                }

                if (datePickBirthday.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату своего рождения");
                    return;
                }

                if(textBoxPatronymic.Text == "")
                    textBoxPatronymic.Text = null;


                var userObj = Base.EM.User.FirstOrDefault(x => x.login == textBoxLogin.Text);

                if (userObj != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!isCorrectPass(passBox)) return;

                User user = new User()
                {
                    surname = textBoxSurname.Text,
                    name = textBoxName.Text,
                    patronymic = textBoxPatronymic.Text,
                    login = textBoxLogin.Text,
                    password = passBox.Password.ToString().GetHashCode(),
                    birthday = datePickBirthday.SelectedDate.Value,
                    id_role = 2,
                    id_gender = gender
                };

                Base.EM.User.Add(user);
                Base.EM.SaveChanges();

                MessageBox.Show("Пользователь успешно зарегистрирован", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Пользователь не зарегистрирован");
            }
            
        }



        private bool isNullText(TextBox tb, String columnName)
        {
            if (tb.Text == "")
            {
                MessageBox.Show("Пустое поле \""+columnName+"\"");
                return true;
            }
            else return false;
        }
        private bool isNullPass(PasswordBox pb, String columnName)
        {
            if (pb.Password == "")
            {
                MessageBox.Show("Введите " + columnName);
                return true;
            }
            else return false;
        }

        private bool isCorrectPass(PasswordBox pb)
        {
            string pass = pb.Password;

            Regex regBigLat = new Regex("(?=.*[A-Z])"); // Одна большая латинская
            Regex regSmallLat = new Regex("(?=.*[a-z])"); // Три строчных латинских
            Regex regDig = new Regex("(?=.*[0-9])"); // Две цифры
            Regex regSpec = new Regex("(?=.*[!@#$%^&*()+=])"); // Спец символы



            if (!(regBigLat.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится ни один заглавный латинский символ");
                return false;
            }
            if (!(regSmallLat.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится 3 прописных латинских символа");
                return false;
            }
            if (!(regDig.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится 2 цифр");
                return false;
            }
            if (!(regSpec.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержутся спец. символы");
                return false;
            }

            if (pass.Length < 8)
            {
                MessageBox.Show("В вашем пароле менее 8 символов");
                return false;
            }

            else return true;
        }

        
    }


}

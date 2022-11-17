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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgency
{
    

    /// <summary>
    /// Логика взаимодействия для PageAdminAddTour.xaml
    /// </summary>
    public partial class PageAdminAddTour : Page
    {
        Tour tourUpdated;
        string path;
        bool isNew;

        public PageAdminAddTour(Tour tour, bool isNew)
        {

            InitializeComponent();

            listBoxDepartCity.ItemsSource = Base.EM.City.ToList();
            listBoxDepartCity.SelectedValuePath = "id_city";
            listBoxDepartCity.DisplayMemberPath = "city_name";


            listBoxReturnCity.ItemsSource = Base.EM.City.ToList();
            listBoxReturnCity.SelectedValuePath = "id_city";
            listBoxReturnCity.DisplayMemberPath = "city_name";

            listBoxTourType.ItemsSource = Base.EM.Tour_Type.ToList();
            listBoxTourType.SelectedValuePath = "id_tour_type";
            listBoxTourType.DisplayMemberPath = "tour_type1";

            listBoxNutrition.ItemsSource = Base.EM.Nutrition.ToList();
            listBoxNutrition.SelectedValuePath = "id_nutrition";
            listBoxNutrition.DisplayMemberPath = "nutrition_type";

            listBoxHotel.ItemsSource = Base.EM.Hotel.ToList();
            listBoxHotel.SelectedValuePath = "id_hotel";
            listBoxHotel.DisplayMemberPath = "hotel_name";

            

            this.isNew = isNew;
            tourUpdated = tour;

            if (!isNew)
            {
               
                textBoxTourName.Text = tour.tour_name;
                textBoxPrice.Text = Convert.ToString(tour.price);
                listBoxDepartCity.SelectedIndex = tour.departure_city_id - 1;
                listBoxReturnCity.SelectedIndex = tour.return_city_id - 1;

                listBoxTourType.SelectedIndex = tour.id_tour_type - 1;
                listBoxNutrition.SelectedIndex = tour.id_nutrition - 1;
                listBoxHotel.SelectedIndex = tour.id_hotel - 1;

                dataPickerDepart.SelectedDate = tour.departure_date;
                dataPickerReturn.SelectedDate = tour.return_date;

               
                
            }


        }

        private void listBoxReturnCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idCity = Convert.ToInt32(listBoxReturnCity.SelectedValue.ToString());
           
            var count = Base.EM.City.FirstOrDefault(x => x.id_city == idCity);
           
            var counrtyName = Base.EM.Country.FirstOrDefault(x => x.id_country == count.id_country);

            labelCountry.Content = counrtyName.country_name;



        }

        private void dataPickerDepart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataPickerReturn.SelectedDate != null)
            {
                string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
                string[] days1 = days.Split('.');
                labelDaysAmount.Content = days1[0];
            }

        }

        private void dataPickerReturn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataPickerDepart.SelectedDate != null)
            {
                string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
                string[] days1 = days.Split('.');
                labelDaysAmount.Content = days1[0];
            }
        }

        private void buttonAddTour_Click(object sender, RoutedEventArgs e)
        {

            int idCity = Convert.ToInt32(listBoxReturnCity.SelectedValue.ToString());
            var count = Base.EM.City.FirstOrDefault(x => x.id_city == idCity);
            string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
            string[] days1 = days.Split('.');
           

            try
            {

                if (isNew)
                {
                    Tour tour = new Tour()
                    {
                        tour_name = textBoxTourName.Text,
                        price = Convert.ToDecimal(textBoxPrice.Text),
                        departure_date = dataPickerDepart.SelectedDate.Value,
                        departure_city_id = Convert.ToInt32(listBoxDepartCity.SelectedValue),
                        return_date = dataPickerReturn.SelectedDate.Value,
                        return_city_id = Convert.ToInt32(listBoxReturnCity.SelectedValue),
                        days_amount = Convert.ToInt32(days1[0]),
                        id_country = count.id_country,
                        id_tour_type = Convert.ToInt32(listBoxTourType.SelectedValue),
                        id_nutrition = Convert.ToInt32(listBoxNutrition.SelectedValue),
                        id_hotel = Convert.ToInt32(listBoxHotel.SelectedValue),
                        tour_img = path
                    };
                    Base.EM.Tour.Add(tour);
                    Base.EM.SaveChanges();

                }
                else
                {
                    tourUpdated.tour_name = textBoxTourName.Text;
                    tourUpdated.price = Convert.ToDecimal(textBoxPrice.Text);
                    tourUpdated.departure_date = dataPickerDepart.SelectedDate.Value;
                    tourUpdated.departure_city_id = Convert.ToInt32(listBoxDepartCity.SelectedValue);
                    tourUpdated.return_date = dataPickerReturn.SelectedDate.Value;
                    tourUpdated.return_city_id = Convert.ToInt32(listBoxReturnCity.SelectedValue);
                    tourUpdated.days_amount = Convert.ToInt32(days1[0]);
                    tourUpdated.id_country = count.id_country;
                    tourUpdated.id_tour_type = Convert.ToInt32(listBoxTourType.SelectedValue);
                    tourUpdated.id_nutrition = Convert.ToInt32(listBoxNutrition.SelectedValue);
                    tourUpdated.id_hotel = Convert.ToInt32(listBoxHotel.SelectedValue);
                    tourUpdated.tour_img = path;

                    
                    Base.EM.SaveChanges();
                }
                   
                    MessageBox.Show("Успешно");
                
               
            }
            catch {
                MessageBox.Show("Проблема");
            }
        }

        private void buttonAddChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();  // создаем объект диалогового окна
            OFD.ShowDialog();  // открываем диалоговое окно
            path = OFD.FileName;  // извлекаем полный путь к картинке
            string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
            path = "\\" + arrayPath[arrayPath.Length - 3] + "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки

            
        }

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }
    }
}

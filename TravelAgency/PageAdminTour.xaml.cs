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
    /// Логика взаимодействия для PageAdminTour.xaml
    /// </summary>
    public partial class PageAdminTour : Page
    {
        public PageAdminTour()
        {
            InitializeComponent();

            var tour = Base.EM.Tour.ToList();
            var tour_type = Base.EM.Tour_Type.ToList();
            var city = Base.EM.City.ToList();
            var country = Base.EM.Country.ToList();
            var hotel = Base.EM.Hotel.ToList();
            var nut = Base.EM.Nutrition.ToList();

            var allData = from t in tour
                          join tt in tour_type
                          on t.id_tour_type equals tt.id_tour_type
                          join c in city
                          on t.departure_city_id equals c.id_city
                          join c2 in city
                          on t.return_city_id equals c2.id_city
                          join co in country
                          on t.id_country equals co.id_country
                          join h in hotel
                          on t.id_hotel equals h.id_hotel
                          join n in nut
                          on t.id_nutrition equals n.id_nutrition

                          select new {t.tour_name, t.price, t.departure_date, t.return_date, city1 =  c.city_name, city2 = c2.city_name, t.days_amount, co.country_name, h.hotel_name, n.nutrition_type};

            listView.ItemsSource = allData.ToList();



        }

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }

        private void gotoPageAdminAddTour_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminAddTour());
        }
    }
}

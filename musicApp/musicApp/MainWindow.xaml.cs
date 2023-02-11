using musicApp.apiCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace musicApp
{
    /// <api key>
    /// Pixabay :
    ///     key : 33423123-b42832bf6ee20d22ac1dc20f2
    ///     query exemple : https://pixabay.com/api/?key=33423123-b42832bf6ee20d22ac1dc20f2&category=music&image_type=photo&pretty=true&min_width=900&min_height=600
    ///Discogs :
    ///     Consumer Key 	VsuLTyjYmHdQAdebSyLD
    ///     Consumer Secret     AxSajuVrIJgxbQPgckKYKNfJWOHLWoZM
    ///     Demander l'URL du Token 	https://api.discogs.com/oauth/request_token
    ///     Autoriser l'URL 	https://www.discogs.com/fr/oauth/authorize
    ///     URL du jeton d'accès 	https://api.discogs.com/oauth/access_token
    ///     query exemple : https://api.discogs.com/database/search?q=Nirvana&key=VsuLTyjYmHdQAdebSyLD&secret=AxSajuVrIJgxbQPgckKYKNfJWOHLWoZM
    /// </api key>
    public partial class MainWindow : Window
    {
        bool MenuStateClose = true;

        public MainWindow()
        {
            InitializeComponent();

            //variable declaration
            RootPixa pixabay = GetApi.GetPixabay();
            RootDisc discogs = new RootDisc();

            //fetch a random image and set it as the background
            int randNbr = RandNbr();
            string URLimage = pixabay.hits[randNbr].largeImageURL;
            image.Source = new BitmapImage(new Uri(URLimage));

            //filter search result for discogs api
            string parametre = "&q=Nirvana&format=Vinyl";
            discogs = GetApi.GetDiscogs(parametre).Result;

            MessageBox.Show(discogs.results[0].cover_image);
        }


        //diplay a certain amount of album in the xaml
        private void get_Disque()
        {
            //this.lbxAsteroid.ItemsSource = Name_List;
        }


        //function for xaml
        //enable the drag of the window
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //button to exit the program
        private void Click_toClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Click_toMenu(object sender, RoutedEventArgs e)
        {

        }

        private void Click_toSlideMenu(object sender, RoutedEventArgs e)
        {
            if (MenuStateClose)
            {
                sidePanel.Width = 170;
                MenuStateClose = false;
                sidePanel_Button.HorizontalAlignment = HorizontalAlignment.Right;
                sidePanel_Button.Margin=new Thickness(0,0,10,0);
            }
            else
            {
                sidePanel.Width = 40;
                sidePanel_Button.HorizontalAlignment= HorizontalAlignment.Center;
                MenuStateClose = true;
            }
        }

        //return a random number
        private int RandNbr()
        {
            Random rnd = new Random();
            int num = rnd.Next(100);
            return num;
        }
    }
}

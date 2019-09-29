using Squad.Views;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Squad
{
	public partial class App : Application
	{
        public static bool IsUserLoggedIn { get; set; }
        public static string ApiToken { get; set; }
        public static string UserName { get; set; }
        public int ResumeAtSquadId { get; set; }
        public static readonly HttpClient HttpClient = new HttpClient();
      
        public App ()
		{
			InitializeComponent();
            if (!Current.Properties.ContainsKey("IsLoggedIn"))
            {
                Current.Properties["IsLoggedIn"] = false;
            }
            var nav = new NavigationPage() ;
            Resources = new ResourceDictionary();
            Resources.Add("primaryBlack", Color.FromHex("000000"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));
            if ((Boolean)Current.Properties["IsLoggedIn"] == false)
            {
                nav = new NavigationPage(new LoginPage());
            }
            else
            {
                nav = new NavigationPage(new Squad.HomePage());
            }
          
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryBlack"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
           
        }
        static SquadItemDatabase database;

        public static SquadItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SquadItemDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SquadSQLite.db3"));
                }
                return database;
            }
        }
        protected override void OnStart ()
		{
           

            // Handle when your app starts
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            App.ApiToken = (String)Current.Properties["ApiToken"];
            App.UserName = (String)Application.Current.Properties["UserName"];

        }
	}

   
}

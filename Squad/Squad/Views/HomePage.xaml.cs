using Newtonsoft.Json;
using Squad.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad { 
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
 
        public HomePage()
		{
			InitializeComponent ();
           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

   
            listView.ItemsSource = await App.Database.GetSquads();
        }
       
       

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new SquadItemPage
                {
                    BindingContext = e.SelectedItem as SquadItem
                });
            }
        }
        async void OnSignOutButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("ApiToken");
            Application.Current.Properties.Remove("UserName");
            Application.Current.Properties["IsLoggedIn"] = false;

            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
        async void OnFriendsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Friends
            {});
        }

    }
}
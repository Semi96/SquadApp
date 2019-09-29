using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad { 
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Friends : ContentPage
	{
        public Friends()
        {
            InitializeComponent();
        

        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            if (e.SelectedItem != null)
            {   

                BindingContext = e.SelectedItem as Friend;
                deleteFriend.IsVisible = true;
                deleteFriend.IsEnabled = true;
            }
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetFriendsAsync();
        }

        async void AddFriend(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendSearch
            {
              
            });
        }
        async void onFriendDelete(object sender, EventArgs e)
        {
            var friend = BindingContext;
            await App.Database.DeleteFriendAsync(friend as Friend);
            listView.BeginRefresh();
        }
       
        

        private async void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = BindingContext as FriendViewModel;
            var dbItems = await App.Database.GetFriendsAsync(); 
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listView.ItemsSource = dbItems;
            else
                listView.ItemsSource = dbItems.Where(i => i.friendName.ToLower().Contains(e.NewTextValue.ToLower()));


        }
        //async void apiPost(object sender, EventArgs e)
        //{
        //    var squadItem = (SquadItem)BindingContext;

        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("http://10.0.2.2:57304");


        //   // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await client.GetAsync("/api/Squad/"+ ScreenMessage.Text+"");

        //    // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
        //    var result = await response.Content.ReadAsStringAsync();
        //    SquadItem squad = JsonConvert.DeserializeObject<SquadItem>(result);

        //    if (squad.SquadName != null )
        //    {


        //        await App.Database.SaveItemAsync(squad);
        //        await Navigation.PopAsync();
        //        await Navigation.PushAsync(new SquadItemPage
        //        {
        //            BindingContext = (SquadItem)BindingContext
        //        });
        //    }




        //}
    }
}
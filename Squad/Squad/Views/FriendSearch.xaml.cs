using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad {
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendSearch : ContentPage
	{
		public FriendSearch()
		{
			InitializeComponent ();
		}

        public class FriendsJson
        {
            [JsonProperty("friends")]
            public List<Friend> friends { get; set; }
         
        }
        async void onFriendSearch(object sender, EventArgs e)
        {      
            var client = App.HttpClient;
            client.BaseAddress = new Uri("http://10.0.2.2:57304");
          //  StringContent content = new StringContent(JsonConvert.SerializeObject(friendSearch.Text), Encoding.UTF8, "application/json");
            // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.GetAsync("/api/Friend/Search/?name=" + friendSearch.Text+"");
            var token = await response.Content.ReadAsStringAsync();
            var token2 = response.Content.ReadAsStringAsync().Result;

          
           var friendList = JsonConvert.DeserializeObject<List<Friend>>(token);
           
           // BindingContext = friendList; 


            listView.ItemsSource = friendList;

        }

        async void AddFriend(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Friend currFriend = e.SelectedItem as Friend;
                var answer = await DisplayAlert("Add friend?", "Would you like to add"+ currFriend.friendName+" user?", "Yes", "No");

                if (answer)
                {

                    var client = App.HttpClient;
                    client.BaseAddress = new Uri("http://10.0.2.2:57304");

                    StringContent content = new StringContent(JsonConvert.SerializeObject(currFriend.friendName), Encoding.UTF8, "application/json");
                    // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("/api/Friend/Request/", content);
                    var token = await response.Content.ReadAsStringAsync();
                    var token2 = response.Content.ReadAsStringAsync().Result;


                    var friendList = JsonConvert.DeserializeObject<List<Friend>>(token);

                    // BindingContext = friendList; 


                    listView.ItemsSource = friendList;
                }
            }

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
using Newtonsoft.Json;
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
	public partial class SquadEvents : ContentPage
	{
		public SquadEvents()
		{
			InitializeComponent ();
		}

      
        async void apiPost(object sender, EventArgs e)
        {
            var squadItem = (SquadItem)BindingContext;

            var client = App.HttpClient;
            client.BaseAddress = new Uri("http://10.0.2.2:57304");


           // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.GetAsync("/api/Squad/"+ ScreenMessage.Text+"");

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            SquadItem squad = JsonConvert.DeserializeObject<SquadItem>(result);

            if (squad.SquadName != null )
            {
               

                await App.Database.SaveItemAsync(squad);
                await Navigation.PopAsync();
                await Navigation.PushAsync(new SquadItemPage
                {
                    BindingContext = (SquadItem)BindingContext
                });
            }
            
          
           

        }
    }
}
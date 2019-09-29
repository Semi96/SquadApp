using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new LoginModel
            {
                UserName = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var client = App.HttpClient;
            client.BaseAddress = new Uri("http://10.0.2.2:57304");

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/Login", content);
            var token= await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                App.IsUserLoggedIn = true;
                Application.Current.Properties["IsLoggedIn"] = true;
                App.ApiToken = token;
                Application.Current.Properties["UserName"] = user.UserName;
                Application.Current.Properties["ApiToken"] =token;
                var currUser = new UserModel { UserName = user.UserName };
                //await Navigation.PushAsync(new HomePage
                //{
                  
                //    BindingContext = currUser
                  
                //});
                Navigation.InsertPageBefore(new HomePage(), this);
                await Navigation.PopAsync();
            } else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
            }
     
            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"

           
          
}
    }
           

      
    

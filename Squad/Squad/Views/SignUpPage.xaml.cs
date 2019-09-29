using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new RegisterModel()
            {
                 
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text,
                FirstName = firstnameEntry.Text,
            LastName = lastnameEntry.Text,
            BirthDate = birthDatePicker.Date,
            PhoneNumber = phonenumberEntry.Text
            };
          //  var json = JsonConvert.SerializeObject(user);
          
            // Sign up logic goes here

            var client = App.HttpClient;
          client.BaseAddress = new Uri("http://10.0.2.2:57304");

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/Register", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"

            if (response.IsSuccessStatusCode)
            {
                await Navigation.PushAsync(new LoginPage
                {
                   
                });
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }


        
    }
}
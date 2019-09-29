using Acr.UserDialogs;
using Squad.Views;
using System;
using Xamarin.Forms;

namespace Squad
{
    public partial class SquadItemPage : ContentPage
    {


        public SquadItemPage()
        {
            InitializeComponent();
        }

        async void squadEvents(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SquadEvents
            {
                BindingContext = (SquadItem)BindingContext
        });
        }
        async void squadChat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatPage
            {
                BindingContext = (SquadItem)BindingContext
            });
        }

        async void onSquadEdit(object sender, EventArgs e)
        {
            var squadItem = (SquadItem)BindingContext;
            // var squadItem = nameEntry;
            // string newText = e.;
            PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            { InputType = InputType.Name,
                Text = squadItem.SquadName,
                OkText = "Save",
                Title = "Edit Squad Name",
                AndroidStyleId = 2131427721


            });
            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                squadItem.SquadName = pResult.Text;
                await App.Database.SaveItemAsync(squadItem);
                await Navigation.PopAsync();
            }


            // var nameEntry = new Entry();
            //  nameEntry.SetBinding(Entry.TextProperty, newText);
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            //if (e.SelectedItem != null)
            //{      
            //    await Navigation.PushAsync(new EditSquadDetails
            //    {
            //        BindingContext = e.SelectedItem as SquadItem
            //    });
            //}
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var squadItem = (SquadItem)BindingContext;
            await App.Database.SaveItemAsync(squadItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var squadItem = (SquadItem)BindingContext;
            await App.Database.DeleteItemAsync(squadItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
   
    }
}

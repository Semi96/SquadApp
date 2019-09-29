using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Squad
{
    public partial class SquadListPage : ContentPage
    {
        public SquadListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtSquadId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SquadItemPage
            {
                BindingContext = new SquadItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new SquadItemPage
                {
                    BindingContext = e.SelectedItem as SquadItem
                });
            }
        }
    }
}

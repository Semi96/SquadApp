
using Xamarin.Forms;

namespace Squad
{
    public class SquadItemPageCS : ContentPage
    {
        public SquadItemPageCS()
        {
            Title = "Squad Item";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "SquadName");




            var editButton = new Button { Text = "Edit" };
            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var squadItem = (SquadItem)BindingContext;
                await App.Database.SaveItemAsync(squadItem);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var squadItem = (SquadItem)BindingContext;
                await App.Database.DeleteItemAsync(squadItem);
                await Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };


            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    editButton,
                    new Label { Text = "SquadName",  },
                    nameEntry,

                    saveButton,
                    deleteButton,
                    cancelButton,

                }
            };
        }
    }
}

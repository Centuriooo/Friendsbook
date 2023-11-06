using System;
using System.IO;
using Friendbook.Models;
using Xamarin.Forms;

namespace Friendbook.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class FriendEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadFriend(value);
            }
        }

        public FriendEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Friend.
            BindingContext = new Friend();
        }

        async void LoadFriend(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the friend and set it as the BindingContext of the page.
                Friend friend = await App.Database.GetFriendAsync(id);
                BindingContext = friend;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load friend.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            friend.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(friend.Name))
            {
                await App.Database.SaveFriendAsync(friend);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            await App.Database.DeleteFriendAsync(friend);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}
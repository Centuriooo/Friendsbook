using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Friendbook.Models;
using Xamarin.Forms;

namespace Friendbook.Views
{
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the friends from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetFriendsAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the FriendEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(FriendEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the FriendEntryPage, passing the ID as a query parameter.
                Friend friend = (Friend)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(FriendEntryPage)}?{nameof(FriendEntryPage.ItemId)}={friend.ID.ToString()}");
            }
        }
    }
}
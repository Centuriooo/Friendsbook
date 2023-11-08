using System;
using System.IO;
using System.Threading.Tasks;
using Friendbook.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Friendbook.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class FriendEntryPage : ContentPage
    {
        string PhotoPath = "person_icon.png";

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

            imagePath.Source = PhotoPath;
        }

        async void LoadFriend(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the friend and set it as the BindingContext of the page.
                Friend friend = await App.Database.GetFriendAsync(id);
                BindingContext = friend;
                imagePath.Source = friend.ProfilePic;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load friend.");
            }
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = "person_icon.png";
                return;
            }

            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
            imagePath.Source = PhotoPath;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            friend.Date = DateTime.UtcNow;
            friend.ProfilePic = PhotoPath;
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

        async void OnCameraButtonClicked(object sender, EventArgs e)
        {
            await TakePhotoAsync(); 
        }

        async void OnGalleryButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { });

                await LoadPhotoAsync(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
            }
        }

    }
}
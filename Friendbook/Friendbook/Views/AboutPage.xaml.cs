using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Friendbook.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://github.com/Centuriooo/Friendsbook/tree/main/Friendbook");
        }
    }
}
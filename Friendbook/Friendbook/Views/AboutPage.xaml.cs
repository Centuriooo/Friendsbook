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

        async void OnWikiButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://github.com/Centuriooo/Friendsbook/wiki/About-the-app");
        }
        async void OnCodeButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://github.com/Centuriooo/Friendsbook");
        }
    }
}
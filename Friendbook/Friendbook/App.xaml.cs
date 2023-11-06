using System;
using System.IO;
using Friendbook.Data;
using Xamarin.Forms;

namespace Friendbook
{
    public partial class App : Application
    {
        static FriendDatabase database;

        // Create the database connection as a singleton.
        public static FriendDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FriendDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Friends.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
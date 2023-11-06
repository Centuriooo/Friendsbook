using Friendbook.Views;
using Xamarin.Forms;

namespace Friendbook
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FriendEntryPage), typeof(FriendEntryPage));
        }
    }
}
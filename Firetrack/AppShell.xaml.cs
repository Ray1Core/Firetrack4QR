namespace Firetrack
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(Views.LoginPage));
            Routing.RegisterRoute("DashboardPage", typeof(Views.DashboardPage));
            Routing.RegisterRoute("ScannerPage", typeof(Views.ScannerPage));
            Routing.RegisterRoute("TransferPage", typeof(Views.TransferPage));   // <-- NEW
            CurrentItem = Items[0];
        }
    }
}
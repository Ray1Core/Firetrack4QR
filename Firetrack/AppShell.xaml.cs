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
            Routing.RegisterRoute("GenerateQRPage", typeof(Views.GenerateQRPage));
            Routing.RegisterRoute("ReportDamagePage", typeof(Views.ReportDamagePage));
            Routing.RegisterRoute("IcsPage", typeof(Views.IcsPage));
            Routing.RegisterRoute("ClearancePage", typeof(Views.ClearancePage));
            Routing.RegisterRoute("AddUserPage", typeof(Views.AddUserPage));
            CurrentItem = Items[0];
        }
    }
}
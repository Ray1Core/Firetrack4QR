using Firetrack.Models;
using Firetrack.ViewModels;
using Microsoft.Maui.Controls;

namespace Firetrack.Views
{
    public partial class ReportDamagePage : ContentPage
    {
        public ReportDamagePage(EquipmentModel equipment)
        {
            InitializeComponent();
            BindingContext = new ReportDamageViewModel(equipment);
        }
    }
}
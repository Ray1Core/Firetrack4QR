using Firetrack.ViewModels;
using Microsoft.Maui.Controls;
// using ZXing.Net.Maui;   // uncomment when re‑enabling
// using ZXing.Net.Maui.Controls;

namespace Firetrack.Views;

public partial class ScannerPage : ContentPage
{
    private readonly ScannerViewModel _vm;

    public ScannerPage()
    {
        InitializeComponent();
        _vm = new ScannerViewModel();
        BindingContext = _vm;
    }

    // ============================================================
    // DISABLED: CameraView event – will be re‑enabled later
    // ============================================================
    /*
    private void OnBarcodeDetected(object sender, BarcodeDetectedEventArgs e)
    {
        var result = e.Results?.FirstOrDefault();
        if (result == null || string.IsNullOrEmpty(result.Value))
            return;

        if (_vm.ScanCommand.CanExecute(result))
            _vm.ScanCommand.Execute(result);
    }
    */

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // _vm.ResumeScanning();   // would resume camera
        // cameraView.IsEnabled = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // cameraView.IsEnabled = false;
    }
}
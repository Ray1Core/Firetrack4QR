using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using Firetrack.Services;   // <-- make sure this using is present

namespace Firetrack
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    // Manually register the CameraView handler (for QR scanner)
                    handlers.AddHandler(typeof(CameraView), typeof(CameraViewHandler));
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ========== ADD THIS SERVICE REGISTRATION ==========
            builder.Services.AddSingleton<PdfGenerationService>();
            // ====================================================

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
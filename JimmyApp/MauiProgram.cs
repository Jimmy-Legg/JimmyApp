using Microsoft.Extensions.Logging;

namespace JimmyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<Page1>();
            builder.Services.AddSingleton<Page2>();
            builder.Services.AddSingleton<Page3>();
            builder.Services.AddSingleton<Page4>();
            return builder.Build();
        }
    }
}

using System.Net.Http.Headers;
// using Android.Net; ❌ remove this unless absolutely needed
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using StudentTaskTechVegas.ApiServices;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.ViewModels;
using StudentTaskTechVegas.Views;

namespace StudentTaskTechVegas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new System.Uri("https://testapi.techvegas.in/") // ✅ full reference if needed
            });

            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IProfileService, ProfileService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddSingleton<LoginViewModel>();
            // ✅ Register your ViewModel
            builder.Services.AddSingleton<HomePageViewModel>();
            // Register Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AcademicPage>();
            builder.Services.AddTransient<ProfileViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

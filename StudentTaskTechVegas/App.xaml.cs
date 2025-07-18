using Microsoft.Extensions.DependencyInjection;
using StudentTaskTechVegas.Views;

namespace StudentTaskTechVegas
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Light;

            MainPage = CheckLogin(serviceProvider);
        }

        private Page CheckLogin(IServiceProvider serviceProvider)
        {
            var token = SecureStorage.GetAsync("Token").Result;

            if (!string.IsNullOrEmpty(token))
            {
                return serviceProvider.GetRequiredService<HomePage>();
            }
            else
            {
                return serviceProvider.GetRequiredService<LoginPage>(); 
            }
        }
    }
}

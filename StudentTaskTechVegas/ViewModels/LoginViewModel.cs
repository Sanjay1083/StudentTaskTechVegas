using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.Helpers;
using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ILoginService _loginService;
        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [ObservableProperty]
        private string? phoneNumber;

        [ObservableProperty]
        private string? password;

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter phone number and password", "OK");
                return;
            }
         //   var loginService = GlobalServices.LoginService;
            var loginModel = new LoginModel
            {
                PhoneNumber = PhoneNumber,
                Password = Password
            };

            var response = await _loginService.GetLoginDetailsAsync(loginModel);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                await SecureStorage.SetAsync("Token", response.Token);

                var parentId = JwtHelper.GetClaimFromToken(response.Token, "ParentId"); 

                if (!string.IsNullOrEmpty(parentId))
                {
                    await SecureStorage.SetAsync("ParentId", parentId);
                }
                await Toast.Make("Login Successful").Show();
                Application.Current.MainPage = new AppShell(); // Navigate to the main page after login
            }
            else
            {
                await Shell.Current.DisplayAlert("Login Failed", "Invalid credentials", "OK");
            }

        }
    }
}

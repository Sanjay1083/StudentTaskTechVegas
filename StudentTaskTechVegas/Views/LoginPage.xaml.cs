using StudentTaskTechVegas.ViewModels;

namespace StudentTaskTechVegas.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
using Microsoft.Maui.Controls;

namespace MyMauiApp;

public partial class LoginFormPage : ContentPage
{
    private bool _isPasswordHidden = true;
    public bool IsPasswordHidden
    {
        get => _isPasswordHidden;
        set
        {
            _isPasswordHidden = value;
            OnPropertyChanged(nameof(IsPasswordHidden));
        }
    }

    public LoginFormPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void OnShowPasswordClicked(object sender, EventArgs e)
    {
        IsPasswordHidden = !IsPasswordHidden;
        ShowPasswordButton.Text = IsPasswordHidden ? "👁" : "👁‍🗨";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try 
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password", "OK");
                return;
            }

            // Add your authentication logic here
            // For example:
            // await AuthService.Login(username, password);
            
            // If login successful, navigate to main page
            // await Shell.Current.GoToAsync("///MainPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnSignUpTapped(object sender, TappedEventArgs e)
    {
        try
        {
            var signUpPage = new SignUpPage();
            await Navigation.PushModalAsync(signUpPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
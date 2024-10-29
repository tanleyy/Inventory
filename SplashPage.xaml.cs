namespace MyMauiApp;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
        NavigateToMainAsync();
    }

    private async void NavigateToMainAsync()
    {
        await Task.Delay(5000);
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}
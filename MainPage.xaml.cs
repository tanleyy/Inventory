using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            StartLoadingProcess();
        }

        private void StartLoadingProcess()
        {
            // Fire and forget
            _ = LoadAndNavigateAsync();
        }

        private async Task LoadAndNavigateAsync()
        {
            try
            {
                // Wait for 5 seconds
                await Task.Delay(5000);

                // Check if Shell.Current is not null
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    // Fallback navigation if Shell navigation fails
                    if (Application.Current?.MainPage != null)
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }
    }
}
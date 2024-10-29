using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    [QueryProperty("UserData", "UserData")]
    public partial class SetPasswordPage : ContentPage
    {
        private UserData _userData = new UserData(); // Initialize with new instance
        public UserData UserData
        {
            get => _userData;
            set
            {
                _userData = value ?? new UserData(); // Handle null case
                OnPropertyChanged(nameof(UserData));
            }
        }

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

        private bool _isConfirmPasswordHidden = true;
        public bool IsConfirmPasswordHidden
        {
            get => _isConfirmPasswordHidden;
            set
            {
                _isConfirmPasswordHidden = value;
                OnPropertyChanged(nameof(IsConfirmPasswordHidden));
            }
        }

        public SetPasswordPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "UserData", UserData }
                };
                await Shell.Current.GoToAsync($"///SignUpPage", navigationParameter);
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

        private void OnShowConfirmPasswordClicked(object sender, EventArgs e)
        {
            IsConfirmPasswordHidden = !IsConfirmPasswordHidden;
            ShowConfirmPasswordButton.Text = IsConfirmPasswordHidden ? "👁" : "👁‍🗨";
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate passwords
                if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a password", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
                {
                    await DisplayAlert("Error", "Please confirm your password", "OK");
                    return;
                }

                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match", "OK");
                    return;
                }

                // Password strength validation
                if (PasswordEntry.Text.Length < 8)
                {
                    await DisplayAlert("Error", "Password must be at least 8 characters long", "OK");
                    return;
                }

                if (!PasswordEntry.Text.Any(char.IsUpper))
                {
                    await DisplayAlert("Error", "Password must contain at least one uppercase letter", "OK");
                    return;
                }

                if (!PasswordEntry.Text.Any(char.IsLower))
                {
                    await DisplayAlert("Error", "Password must contain at least one lowercase letter", "OK");
                    return;
                }

                if (!PasswordEntry.Text.Any(char.IsDigit))
                {
                    await DisplayAlert("Error", "Password must contain at least one number", "OK");
                    return;
                }

                if (!PasswordEntry.Text.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    await DisplayAlert("Error", "Password must contain at least one special character", "OK");
                    return;
                }

                // Store the password in UserData
                UserData.Password = PasswordEntry.Text;

                // If all validation passes
                await DisplayAlert("Success", "Account created successfully!", "OK");

                // Navigate back to login
                await Shell.Current.GoToAsync("//LoginFormPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
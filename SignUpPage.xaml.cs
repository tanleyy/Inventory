using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace MyMauiApp
{
    [QueryProperty("UserData", "UserData")]
    public partial class SignUpPage : ContentPage
    {
        private UserData _userData;
        public UserData UserData
        {
            get => _userData;
            set
            {
                _userData = value;
                LoadUserData();
            }
        }

        public SignUpPage()
        {
            InitializeComponent();
            _userData = new UserData();
            BindingContext = _userData;
        }

        private void LoadUserData()
        {
            if (_userData != null)
            {
                FullNameEntry.Text = _userData.FullName;
                EmailEntry.Text = _userData.Email;
                UsernameEntry.Text = _userData.Username;
                PositionEntry.Text = _userData.Position;
                MobileEntry.Text = _userData.Mobile;
                DateOfBirthPicker.Date = _userData.DateOfBirth;
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//LoginFormPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            try
            {
                // Basic validation - check if any field is empty
                if (string.IsNullOrWhiteSpace(FullNameEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter your full name", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter your email", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a username", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(PositionEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter your position", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(MobileEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter your mobile number", "OK");
                    return;
                }

                // Email validation
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(EmailEntry.Text, emailPattern))
                {
                    await DisplayAlert("Error", "Please enter a valid email address", "OK");
                    return;
                }

                // Mobile number validation (assuming Philippine format)
                string mobilePattern = @"^(09|\+639)\d{9}$";
                if (!Regex.IsMatch(MobileEntry.Text, mobilePattern))
                {
                    await DisplayAlert("Error", "Please enter a valid mobile number (e.g., 09123456789)", "OK");
                    return;
                }

                // Date validation - ensure user is at least 18 years old
                var minimumAge = 18;
                var today = DateTime.Today;
                var age = today.Year - DateOfBirthPicker.Date.Year;
                if (DateOfBirthPicker.Date.Date > today.AddYears(-age)) age--;

                if (age < minimumAge)
                {
                    await DisplayAlert("Error", "You must be at least 18 years old to register", "OK");
                    return;
                }

                // Store the current values in UserData
                _userData.FullName = FullNameEntry.Text;
                _userData.Email = EmailEntry.Text;
                _userData.Username = UsernameEntry.Text;
                _userData.Position = PositionEntry.Text;
                _userData.Mobile = MobileEntry.Text;
                _userData.DateOfBirth = DateOfBirthPicker.Date;

                var navigationParameter = new Dictionary<string, object>
                {
                    { "UserData", _userData }
                };

                await Shell.Current.GoToAsync($"SetPasswordPage", navigationParameter);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
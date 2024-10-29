using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("SplashPage", typeof(SplashPage));
            Routing.RegisterRoute("LoginFormPage", typeof(LoginFormPage));
            Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("SetPasswordPage", typeof(SetPasswordPage));
        }
    }
}
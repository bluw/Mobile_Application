using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mobile_Application.Services;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Application.ViewModel
{
    public class LoginUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public LoginUserViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private async void SignIn_Click()
        {
            if (Email != null && Password != null) {

                LoginService loginService = new LoginService();
                IsLogged = await loginService.VerifiyLoginAsync(Email, Password);

                if (IsLogged) {

                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    localSettings.Values["Email"] = Email;

                    _navigationService.NavigateTo("FavoritePage");

                } else {
                    //wrong email or password
                }
            } else {
                ShowToast("please enter email and password");
            }
        }


        public void ShowToast(String value)
        {
            //var loader = 

            ToastVisual visual = new ToastVisual() {
                TitleText = new ToastText() {
                    Text = value
                },
            };

            ToastContent content = new ToastContent();
            content.Visual = visual;
            var toast = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private ICommand _signIn;
        public ICommand SignIn
        {
            get
            {
                if (_signIn == null) {
                    _signIn = new RelayCommand(SignIn_Click);
                }
                return _signIn;
            }
        }

        private ICommand _signUp;
        public ICommand SignUp
        {
            get
            {
                if (_signUp == null) {
                    _signUp = new RelayCommand(() => GoSignUpPage());
                }
                return _signUp;
            }
        }

        private void GoSignUpPage()
        {
            _navigationService.NavigateTo("RegisterPage");
        }

        public bool IsLogged { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}

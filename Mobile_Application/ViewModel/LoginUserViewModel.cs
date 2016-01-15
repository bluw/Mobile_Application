using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mobile_Application.Exceptions;
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
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                LoginService loginService = new LoginService();
                try
                {
                    IsLogged = await loginService.VerifiyLoginAsync(Email, Password);

                    if (IsLogged)
                    {

                        localSettings.Values["Email"] = Email;
                        if (IsRemember)
                        {
                            localSettings.Values["isRemember"] = IsRemember;
                        }
                        else
                        {
                            IsRemember = false;
                            localSettings.Values["isRemember"] = IsRemember;
                        }

                        _navigationService.NavigateTo("FavoritePage");

                    }
                    else
                    {
                        ShowToast("wrong_email_password");
                    }

                }
                catch (NoNetworkException e)
                {
                    ShowToast(e.ToString());
                }
            }
            else
            {
                ShowToast("email_password_missing");
            }
        }

        internal void OnNavigatedTo()
        {
            
            var localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;
            if(localSetting.Values != null)
            {
                if (localSetting.Values["isRemember"] != null)
                {
                    bool value = (bool)localSetting.Values["isRemember"];

                    if (value)
                    {
                        // ne marche pas
                        _navigationService.NavigateTo("FavoritePage");
                    }
                }
                if (localSetting.Values["Email"] != null)
                {
                    string email = (string)localSetting.Values["Email"];
                    if (email != null)
                    {
                        Email = email;
                    }
                }
            }
        }
        
        public void ShowToast(String value)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            ToastVisual visual = new ToastVisual() {
                TitleText = new ToastText() {
                    Text = loader.GetString(value)
                },
            };

            ToastContent content = new ToastContent();
            content.Visual = visual;
            var toast = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /* Navigation command */

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

        /*gettors & settors */

        public bool IsLogged { get; set; }
        public bool IsRemember { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

    }
}

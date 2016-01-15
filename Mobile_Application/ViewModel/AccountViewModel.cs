using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mobile_Application.Exceptions;
using Mobile_Application.Model;
using Mobile_Application.Services;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Application.ViewModel
{
    public class AccountViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public AccountViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public void OnNavigatedTo()
        {
            loadUser();
        }


        private async void loadUser()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var email = (string)localSettings.Values["Email"];

            PersonService service = new PersonService();
            try {
                User = await service.getDetailsPersonAsync(email);
            }catch (NoNetworkException e)
            {
                ShowToast(e.ToString());
            }
        }
        public void ShowToast(String value)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText()
                {
                    Text = loader.GetString(value)
                },
            };

            ToastContent content = new ToastContent();
            content.Visual = visual;
            var toast = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /* Navigation command & event handling */

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if (_search == null) {
                    _search = new RelayCommand(() => Search_Tapped());
                }
                return _search;
            }
        }

        private ICommand _goFavorite;
        public ICommand GoFavorite
        {
            get
            {
                if (_goFavorite == null) {
                    _goFavorite = new RelayCommand(() => Favorite_Tapped());
                }
                return _goFavorite;
            }
        }

        private ICommand _modif;
        public ICommand Modification
        {
            get
            {
                if (_modif == null) {
                    _modif = new RelayCommand(() => Modifications_Tapped());
                }
                return _modif;
            }
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }

        private void Search_Tapped()
        {
            _navigationService.NavigateTo("SearchPage");
        }

        private void Favorite_Tapped()
        {
            _navigationService.NavigateTo("FavoritePage");
        }

        private void Modifications_Tapped()
        {
            _navigationService.NavigateTo("ModificationPage", User);
        }


        public Person User { get; set; }    
    }
}

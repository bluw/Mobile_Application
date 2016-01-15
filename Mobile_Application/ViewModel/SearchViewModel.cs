using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mobile_Application.Model;
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

namespace Mobile_Application.ViewModel
{
    public class SearchViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public SearchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        private async void Search_Click()
        {
            if (CanExecute()) {

                PersonService service = new PersonService();

                try {

                    Input = FirstLetterToUpperCase(Input);

                    if (EmailChecked) {
                        var person = await service.getDetailsPersonAsync(Input);
                        ListPerson[0] = person;

                    } else {

                        if (NameChecked) {
                            ListPerson = await service.searchPersonByNameAsync(Input);
                        } else {
                            ListPerson = await service.searchPersonByCompanyAsync(Input);
                        }
                    }

                    _navigationService.NavigateTo("SearchListPage", ListPerson);

                } catch (Exception e) {
                    ShowToast("search_not_found");
                }
            }
        }


        private string FirstLetterToUpperCase(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }

            char[] tabStr = str.ToCharArray();
            tabStr[0] = char.ToUpper(tabStr[0]);

            return new string(tabStr);
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

        public Boolean CanExecute()
        {
            if (_input == null) {
                return false;
            }

            return true;
        }

        /* Navigation command  & event handling */

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if (_search == null) {
                    _search = new RelayCommand(() => Search_Click());
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

        private ICommand _account;
        public ICommand Account
        {
            get
            {
                if (_account == null) {
                    _account = new RelayCommand(() => Account_Tapped());
                }
                return _account;
            }
        }

        private void Account_Tapped()
        {
            _navigationService.NavigateTo("Account");
        }

        private void Favorite_Tapped()
        {
            _navigationService.NavigateTo("FavoritePage");
        }

        private void rb_Name_Checked()
        {
           Name_Checked_change();
        }

        private void rb_Email_Checked()
        {
            Email_Checked_change();
        }

        private void rb_Company_Checked()
        {
            Company_Checked_change();
        }


        public void Company_Checked_change()
        {
            CompanyChecked = true;
            NameChecked = false;
            EmailChecked = false;
        }

        public void Name_Checked_change()
        {
            CompanyChecked = false;
            NameChecked = true;
            EmailChecked = false;
        }

        public void Email_Checked_change()
        {
            CompanyChecked = false;
            NameChecked = false;
            EmailChecked = true;
        }


        /* gettors & settors */

        private string _input;
        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                RaisePropertyChanged("Input");
            }
        }

        public bool CompanyChecked { get; set; }
        public bool NameChecked { get; set; }
        public bool EmailChecked { get; set; }

        public Person[] ListPerson { get; set; }
    }
}

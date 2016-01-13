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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Application.ViewModel
{
    public class ModificationsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public ModificationsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        private async void SubmitClick()
        {
            try {

                string encryptedPassword = PasswordManager();

                if (CanExecute()) {

                    try {
                        Person newUser = new Person();
                        newUser.Email = Email;
                        newUser.Password = encryptedPassword;
                        newUser.FirstName = FirstName;
                        newUser.LastName = LastName;
                        newUser.KeyLength = ConvertKey();
                        newUser.KeyUsed = KeyUsed;
                        newUser.Company.NameCompany = FirstLetterToUpperCase(CompanyName);
                        newUser.TypeAlgo.Type = SelectedAlgorithm;

                        PersonService peopleService = new PersonService();
                        await peopleService.UpdatePersonAsync(newUser);

                        //return to favorite page
                        _navigationService.NavigateTo("FavoritePage");
                    } catch (Exception e) {
                        ShowToast(e.ToString());
                    }
                }

            } catch (Exception e) {
                ShowToast("not_same_password");
            }
 
        }


        private int ConvertKey()
        {
            // clean la cle si il a mis "128 bits" en 128
            string cleanKey = Regex.Replace(KeyLength, @"[^\d]", "");

            if (cleanKey  == String.Empty) {
                return 0;
            }

            return int.Parse(cleanKey);
        }


        private string MyCrypt(string password)
        {
            return password;
        }


        private string PasswordManager()
        {
            string encryptedPassword;

            if (NewPasswordEntered()) {
                if (EqualsdOldPassword()) {
                    if (EqualsNewPassword()) {
                        encryptedPassword = MyCrypt(NewPassword);
                    } else {
                        throw new Exception();
                    }
                } else {
                    throw new Exception();
                }

            } else {
                encryptedPassword = Password;
            }

            return encryptedPassword;
        }


        private bool NewPasswordEntered()
        {
            if (NewPassword != null && PasswordCheck != null) {
                return true;
            } else {
                return false;
            }
        }

        private bool EqualsdOldPassword()
        {
            string encryptedPasswd = MyCrypt(OldPassword);

            if (encryptedPasswd.Equals(Password)) {
                return true;
            } else {
                return false;
            }
        }

        private bool EqualsNewPassword()
        {
            if (NewPassword.Equals(PasswordCheck)) {
                return true;
            } else {
                return false;
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


        public void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadAlgorithms();
            var user = (Person)e.Parameter;
            LoadInfo(user);
        }


        private async void LoadAlgorithms()
        {
            AlgorithmService service = new AlgorithmService();
            AlgorithmsList = await service.GetAlgorithmsAsync();
        }


        private void LoadInfo(Person user)
        {
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            KeyLength = user.KeyLength.ToString();
            KeyUsed = user.KeyUsed;
            CompanyName = user.Company.NameCompany;
            SelectedAlgorithm = user.TypeAlgo.Type;
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

        private Boolean CanExecute()
        {
            if (KeyLength == null || KeyUsed == null || CompanyName == null || SelectedAlgorithm == null) {
                return false;
            }

            return true;
        }


        /* Navigation command */

        private ICommand _submit;
        public ICommand Submit
        {
            get
            {
                if (_submit == null) {
                    _submit = new RelayCommand(() => SubmitClick());
                }
                return _submit;
            }
        }

        private ICommand _back;
        public ICommand Back
        {
            get
            {
                if (_back == null) {
                    _back = new RelayCommand(() => GoBack());
                }
                return _back;
            }
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }


        /* gettors & settors of each box */

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                RaisePropertyChanged("OldPassword");
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string _passwordCheck;
        public string PasswordCheck
        {
            get { return _passwordCheck; }
            set
            {
                _passwordCheck = value;
                RaisePropertyChanged("PasswordCheck");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        private string _keyLength;
        public string KeyLength
        {
            get { return _keyLength; }
            set
            {
                _keyLength = value;
                RaisePropertyChanged("KeyLength");
            }
        }

        private string _keyUsed;
        public string KeyUsed
        {
            get { return _keyUsed; }
            set
            {
                _keyUsed = value;
                RaisePropertyChanged("KeyUsed");
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                RaisePropertyChanged("CompanyName");
            }
        }

        private string _selectedAlgorithm;
        public string SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                _selectedAlgorithm = value;
                RaisePropertyChanged("SelectedAlgorithm");
            }
        }

        private IEnumerable<Algorithm> _algorithmsList;
        public IEnumerable<Algorithm> AlgorithmsList
        {
            get { return _algorithmsList; }
            set
            {
                _algorithmsList = value;
                RaisePropertyChanged("AlgorithmsList");
            }
        }
    }
}

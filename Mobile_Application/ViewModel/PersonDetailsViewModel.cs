﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Mobile_Application.Model;
using Mobile_Application.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Application.ViewModel
{
    public class PersonDetailsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public PersonDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            personCliked = (Person)e.Parameter;           
        }

        private async void AddFavorite_Tapped()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var email = (string)localSettings.Values["Email"];

            FavoriteService service = new FavoriteService();

            if (email != personCliked.Email) {
 
                await service.AddFavoriteAsync(email, personCliked.Email);
                _navigationService.NavigateTo("FavoriteDetails", personCliked);
    
            } else {
                //show message si il veut sajouter lui meme en favoris
            }
        }

        private async void RemoveFavorite_Tapped()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var email = (string)localSettings.Values["Email"];

            FavoriteService service = new FavoriteService();
            await service.RemoveFavoriteAsync(email, personCliked.Email);

            _navigationService.NavigateTo("PersonDetails", personCliked);
        }

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

        private ICommand _addFavorite;
        public ICommand AddFavorite
        {
            get
            {
                if (_addFavorite == null) {
                    _addFavorite = new RelayCommand(() => AddFavorite_Tapped());
                }
                return _addFavorite;
            }
        }

        private ICommand _removeFavorite;
        public ICommand RemoveFavorite
        {
            get
            {
                if (_removeFavorite == null) {
                    _removeFavorite = new RelayCommand(() => RemoveFavorite_Tapped());
                }
                return _removeFavorite;
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

        private void Search_Tapped()
        {
            _navigationService.NavigateTo("SearchPage");
        }

        private void Favorite_Tapped()
        {
            _navigationService.NavigateTo("FavoritePage");
        }

        public Person personCliked { get; set; }
    }
}

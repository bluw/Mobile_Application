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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mobile_Application.ViewModel
{
    public class ListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public ListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedToFavorite()
        {
            LoadFavorites();
        }

        public void OnNavigatedToSearch(NavigationEventArgs e)
        {
            IEnumerable<Person> list = (Person[])e.Parameter;
            PeopleList = list;
        }

        public void Person_Click(ItemClickEventArgs e)
        {
            SelectedPerson = (Person)((ItemClickEventArgs)e).ClickedItem;
            _navigationService.NavigateTo("PersonDetails", SelectedPerson);
        }

        public void Favorite_Click(ItemClickEventArgs e)
        {
            SelectedPerson = (Person)((ItemClickEventArgs)e).ClickedItem;
            _navigationService.NavigateTo("FavoriteDetails", SelectedPerson);
        }

        private async void LoadFavorites()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var email = (string)localSettings.Values["Email"];
            FavoriteService service = new FavoriteService();
            PeopleList = await service.getFavoritesAsync(email);
        }

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }

        private IEnumerable<Person> _peopleList;
        public IEnumerable<Person> PeopleList
        {
            get { return _peopleList; }
            set
            {
                _peopleList = value;
                RaisePropertyChanged("FavoritesList");
            }
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

        private void Account_Tapped()
        {
            _navigationService.NavigateTo("Account");
        }

        private void Favorite_Tapped()
        {
            _navigationService.NavigateTo("FavoritePage");
        }

        private void Search_Tapped()
        {
            _navigationService.NavigateTo("SearchPage");
        }
    }
}

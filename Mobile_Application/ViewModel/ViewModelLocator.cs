using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Mobile_Application.View;
using Mobile_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LoginUserViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<SearchListViewModel>();
            SimpleIoc.Default.Register<FavoritesViewModel>();
            SimpleIoc.Default.Register<PersonDetailsViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<AddClientViewModel>();
            SimpleIoc.Default.Register<ModificationsViewModel>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Unregister<INavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("LoginPage", typeof(LoginPage));
            navigationService.Configure("RegisterPage", typeof(RegisterPage));
            navigationService.Configure("FavoritePage", typeof(FavoritePage));
            navigationService.Configure("FavoriteDetails", typeof(FavoriteDetails));
            navigationService.Configure("SearchPage", typeof(SearchPage));
            navigationService.Configure("SearchListPage", typeof(SearchListPage));
            navigationService.Configure("PersonDetails", typeof(PersonDetails));
            navigationService.Configure("Account", typeof(Account));
            navigationService.Configure("ModificationPage", typeof(ModificationPage));
        }

        public LoginUserViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginUserViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        public SearchListViewModel SearchList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchListViewModel>();
            }
        }

        public FavoritesViewModel Favorite
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FavoritesViewModel>();
            }
        }

        public PersonDetailsViewModel PersonDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PersonDetailsViewModel>();
            }
        }

        public AccountViewModel Account
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountViewModel>();
            }
        }

        public AddClientViewModel Register
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddClientViewModel>();
            }
        }

        public ModificationsViewModel Modifications
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ModificationsViewModel>();
            }
        }
    }
}
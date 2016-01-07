using Mobile_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mobile_Application.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {

        public SearchPage()
        {
            this.InitializeComponent();
        }

        private void rb_Name_Checked(object sender, RoutedEventArgs e)
        {
            ((SearchViewModel)DataContext).Name_Checked_change();
        }

        private void rb_Email_Checked(object sender, RoutedEventArgs e)
        {
            ((SearchViewModel)DataContext).Email_Checked_change();
        }

        private void rb_Company_Checked(object sender, RoutedEventArgs e)
        {
            ((SearchViewModel)DataContext).Company_Checked_change();
        }
    }
}

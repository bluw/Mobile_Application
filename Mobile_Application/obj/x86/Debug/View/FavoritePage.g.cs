﻿#pragma checksum "C:\Users\admin\Documents\Visual Studio 2015\Projects\Mobile_Application_2\Mobile_Application\View\FavoritePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1ABEA6E097E5CC730BC6E721416ABFE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mobile_Application.View
{
    partial class FavoritePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.appBar = (global::Windows.UI.Xaml.Controls.CommandBar)(target);
                }
                break;
            case 2:
                {
                    this.Account = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 3:
                {
                    this.Search = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 4:
                {
                    this.SearchPagePanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.ListView element5 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 21 "..\..\..\View\FavoritePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)element5).ItemClick += this.Favorite_ItemClick;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


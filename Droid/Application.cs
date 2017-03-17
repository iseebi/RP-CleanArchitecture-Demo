using System;
using Android.App;
using Android.Runtime;
using GalaSoft.MvvmLight.Views;

namespace MasterDetail.Droid
{
    [Application]
    public class Application : Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var navigationService = new NavigationService();
            var dialogService = new DialogService();

            navigationService.Configure("Browse", typeof(Views.BrowseActivity));
            navigationService.Configure("Detail", typeof(Views.DetailActivity));
            navigationService.Configure("NewItem", typeof(Views.NewItemActivity));

            App.Initialize(navigationService, dialogService);
        }
    }
}

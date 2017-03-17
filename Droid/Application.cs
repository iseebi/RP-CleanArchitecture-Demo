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

            navigationService.Configure(nameof(ViewModels.BrowseViewModel), typeof(Views.BrowseActivity));

            App.Initialize(navigationService, dialogService);
        }
    }
}

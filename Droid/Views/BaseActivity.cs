using System;
using Android.App;
using GalaSoft.MvvmLight.Views;
using MasterDetail.ViewModels;
using Microsoft.Practices.Unity;
using MasterDetail.Views;
using Android.Runtime;

namespace MasterDetail.Droid.Views
{
    public interface IView<T>
        where T : IViewModel
    {
        T ViewModel { get; set; }
    }

    internal static class ViewExtensions
    {
        public static void OnBeforeCreated<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            view.ViewModel = App.Current.ViewModelLocator.Create<TVm>();
        }

        public static void OnAfterCreated<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;
            var navigationService = App.Current.Container.Resolve<INavigationService>() as NavigationService;

            watching?.OnLoad(navigationService?.GetAndRemoveParameter(activity.Intent));
        }

        public static void OnStartCalled<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnWillAppear();
        }

        public static void OnResumeCalled<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnDidAppear();
        }

        public static void OnPauseCalled<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnWillDisappear();
        }

        public static void OnStopCalled<TActivity, TVm>(this TActivity activity)
            where TActivity : Activity, IView<TVm>
            where TVm : IViewModel
        {
            var view = activity as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnDidDisappear();
        }
    }

    public abstract class BaseActivity<T> : ActivityBase, IView<T>
        where T : IViewModel
    {
        public T ViewModel { get; set; }

        /*
        public BaseActivity()
        {
        }

        public BaseActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        */

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            this.OnBeforeCreated<BaseActivity<T>, T>();
            base.OnCreate(savedInstanceState);
            this.OnAfterCreated<BaseActivity<T>, T>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.OnStartCalled<BaseActivity<T>, T>();
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.OnResumeCalled<BaseActivity<T>, T>();
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.OnPauseCalled<BaseActivity<T>, T>();
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.OnStopCalled<BaseActivity<T>, T>();
        }
    }
}
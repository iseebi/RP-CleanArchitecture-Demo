using System;
using GalaSoft.MvvmLight.Views;
using MasterDetail.ViewModels;
using MasterDetail.Views;
using Microsoft.Practices.Unity;
using UIKit;

namespace MasterDetail.iOS.Views
{
    public interface IView<T>
        where T : IViewModel
    {
        T ViewModel { get; set; }
    }

    internal static class ViewExtensions
    {
        public static void OnBeforeViewDidLoad<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            view.ViewModel = App.Current.ViewModelLocator.Create<TVm>();
        }

        public static void OnAfterViewDidLoad<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;
            var navigationService = App.Current.Container.Resolve<INavigationService>() as NavigationService;

            watching?.OnLoad(navigationService?.GetAndRemoveParameter(viewController));
        }

        public static void OnWillAppear<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnWillAppear();
        }

        public static void OnDidAppear<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnDidAppear();
        }

        public static void OnWillDisappear<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnWillDisappear();
        }

        public static void OnDidDisappear<TVc, TVm>(this TVc viewController)
            where TVc : UIViewController, IView<TVm>
            where TVm : IViewModel
        {
            var view = viewController as IView<TVm>;
            var watching = view.ViewModel as IViewLifecycleWatching;

            watching?.OnDidDisappear();
        }
    }

    public class BaseViewController<T> : UIViewController, IView<T>
        where T : IViewModel
    {
        public T ViewModel { get; set; }

        public BaseViewController(IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            this.OnBeforeViewDidLoad<BaseViewController<T>, T>();
            base.ViewDidLoad();
            this.OnAfterViewDidLoad<BaseViewController<T>, T>();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.OnWillAppear<BaseViewController<T>, T>();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            this.OnDidAppear<BaseViewController<T>, T>();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            this.OnWillDisappear<BaseViewController<T>, T>();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.OnDidDisappear<BaseViewController<T>, T>();
        }
    }

    public class BaseTableViewController<T> : UITableViewController, IView<T>
        where T : IViewModel
    {
        public T ViewModel { get; set; }

        public BaseTableViewController(IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            this.OnBeforeViewDidLoad<BaseTableViewController<T>, T>();
            base.ViewDidLoad();
            this.OnAfterViewDidLoad<BaseTableViewController<T>, T>();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.OnWillAppear<BaseTableViewController<T>, T>();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            this.OnDidAppear<BaseTableViewController<T>, T>();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            this.OnWillDisappear<BaseTableViewController<T>, T>();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.OnDidDisappear<BaseTableViewController<T>, T>();
        }
    }
}
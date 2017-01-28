using GalaSoft.MvvmLight.Views;
using MasterDetail.DataStores;
using MasterDetail.Repositories;
using MasterDetail.Scenes;
using MasterDetail.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace MasterDetail
{
    public class App
    {
        public static App Current { get; private set; }

        public IUnityContainer Container { get; }
        public IServiceLocator ServiceLocator { get; }
        public IViewModelLocator ViewModelLocator { get; }

        public static void Initialize(INavigationService navigationService, IDialogService dialogService)
        {
            Current = new App(navigationService, dialogService);
        }

        protected App(INavigationService navigationService, IDialogService dialogService)
        {
            Container = new UnityContainer();
            ViewModelLocator = new ViewModelLocator(Container);
            ServiceLocator = new UnityServiceLocator(Container);
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => ServiceLocator);

            Container.RegisterInstance(navigationService);
            Container.RegisterInstance(dialogService);

            SetupContainer(Container);
        }

        protected void SetupContainer(IUnityContainer container)
        {
            container.RegisterType<IWordsDataStore, WordsDataStore>(new ContainerControlledLifetimeManager());
            container.RegisterType<IWordsRepository, WordsRepository>(new ExternallyControlledLifetimeManager());
            container.RegisterType<IWordsScene, WordsScene>(new ExternallyControlledLifetimeManager());
        }
    }
}
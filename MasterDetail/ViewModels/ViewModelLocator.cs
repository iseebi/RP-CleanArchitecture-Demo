using System.Runtime.Versioning;
using Microsoft.Practices.Unity;

namespace MasterDetail.ViewModels
{
    public interface IViewModelLocator
    {
        T Create<T>() where T : IViewModel;
    }

    public class ViewModelLocator : IViewModelLocator
    {
        public IUnityContainer Container { get; }

        public ViewModelLocator(IUnityContainer container)
        {
            Container = container;
        }

        public T Create<T>() where T : IViewModel
        {
            return Container.Resolve<T>();
        }
    }
}
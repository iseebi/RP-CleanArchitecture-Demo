namespace MasterDetail.Views
{
    public interface IViewLifecycleWatching
    {
        void OnLoad(object parameter);
        void OnWillAppear();
        void OnDidAppear();
        void OnWillDisappear();
        void OnDidDisappear();
    }
}
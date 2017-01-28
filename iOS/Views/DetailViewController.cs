using System;
using MasterDetail.ViewModels;
using Reactive.Bindings;

namespace MasterDetail.iOS.Views
{
    public partial class DetailViewController : BaseViewController<DetailViewModel>
    {
        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TitleLabel.SetBinding(v => v.Text, ViewModel.Title);
            DescriptionLabel.SetBinding(v => v.Text, ViewModel.Description);
        }
    }
}
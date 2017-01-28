using Foundation;
using System;
using System.Reactive.Linq;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.ViewModels;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using UIKit;

namespace MasterDetail.iOS.Views
{
    public partial class NewItemViewController : BaseViewController<NewItemViewModel>
    {
        public NewItemViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TitleTextField.SetBinding(v => v.Text, ViewModel.Title,
                v => Observable.FromEventPattern(v, nameof(v.EditingChanged)).ToUnit());
            DescriptionTextField.SetBinding(v => v.Text, ViewModel.Description,
                v => Observable.FromEventPattern(v, nameof(v.EditingChanged)).ToUnit());

            Observable.FromEventPattern(SaveButton, nameof(SaveButton.TouchUpInside))
                .SetCommand(ViewModel.CommitChange);
        }
    }
}
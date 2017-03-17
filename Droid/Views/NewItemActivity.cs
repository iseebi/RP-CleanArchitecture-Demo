
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MasterDetail.ViewModels;
using Reactive.Bindings;

namespace MasterDetail.Droid.Views
{
    [Activity(Label = "NewItemActivity")]
    public class NewItemActivity : BaseActivity<NewItemViewModel>
    {
        /*
        public NewItemActivity()
        {
        }

        public NewItemActivity(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        */

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewItem);
            var titleTextField = FindViewById<EditText>(Resource.Id.edittext_title);
            var descriptionTextField = FindViewById<EditText>(Resource.Id.edittext_description);
            var addButton = FindViewById<Button>(Resource.Id.button_add);

            titleTextField.SetBinding(v => v.Text, ViewModel.Title,
                v => Observable.FromEventPattern(v, nameof(v.TextChanged)).Select(_ => Unit.Default));
            descriptionTextField.SetBinding(v => v.Text, ViewModel.Description,
                v => Observable.FromEventPattern(v, nameof(v.TextChanged)).Select(_ => Unit.Default));
            Observable.FromEventPattern(addButton, nameof(addButton.Click))
                .SetCommand(ViewModel.CommitChange);
        }
    }
}

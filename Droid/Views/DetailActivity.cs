
using System;
using System.Collections.Generic;
using System.Linq;
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
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : BaseActivity<DetailViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Detail);
            var titleTextView = FindViewById<TextView>(Resource.Id.textview_title);
            var descriptionTextView = FindViewById<TextView>(Resource.Id.textview_description);

            titleTextView.SetBinding(v => v.Text, ViewModel.Title);
            descriptionTextView.SetBinding(v => v.Text, ViewModel.Description);
        }
    }
}

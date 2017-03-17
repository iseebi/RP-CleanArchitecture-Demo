using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Android.App;
using Android.Widget;
using MasterDetail.ViewModels;
using Reactive.Bindings;

namespace MasterDetail.Droid.Views
{
    [Activity(Label = "MasterDetail", MainLauncher = true, Icon = "@mipmap/icon")]
    public class BrowseActivity : BaseActivity<BrowseViewModel>
    {
        List<Models.Item> Items = new List<Models.Item>();
        public BrowseActivity()
        {
        }

        public BrowseActivity(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Browse);

            var weakThis = new WeakReference<BrowseActivity>(this);

            var listView = FindViewById<ListView>(Resource.Id.listView);
            var button = FindViewById<Button>(Resource.Id.button);

            Observable.FromEventPattern(button, nameof(button.Click))
                      .SetCommand(ViewModel.NavigateToNew);
            Observable.FromEventPattern<AdapterView.ItemClickEventArgs>(listView, nameof(listView.ItemClick))
                      .Select(arg => weakThis.Resolve()?.Items[arg.EventArgs.Position])
                      .SetCommand(ViewModel.NavigateToDetail);

            var adapter = new ListAdapter<Models.Item>(Items, (row, item) =>
            {
                var strongThis = weakThis.Resolve();
                if (strongThis == null) { return null; }
                var view = strongThis.LayoutInflater.Inflate(Resource.Layout.ListItem_Browse, listView);
                var holder = new ViewHolder
                {
                    TitleTextView = view.FindViewById<TextView>(Resource.Id.text_title),
                    DescriptionTextView = view.FindViewById<TextView>(Resource.Id.text_description)
                };
                view.Tag = holder;
                return view;
            }, (row, item, view) =>
            {
                var holder = view.Tag as ViewHolder;
                holder.TitleTextView.Text = item.Title;
                holder.DescriptionTextView.Text = item.Description;
            });

            ViewModel.Items.Subscribe(items =>
            {
                var strongThis = weakThis.Resolve();
                if (strongThis == null) { return; }
                strongThis.Items.Clear();
                strongThis.Items.AddRange(items);
                adapter.NotifyDataSetChanged();
            });
        }

        class ViewHolder : Java.Lang.Object
        {
            public TextView TitleTextView { get; set; }
            public TextView DescriptionTextView { get; set; }
        }
    }
}

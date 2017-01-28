using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Foundation;
using MasterDetail.Models;
using MasterDetail.ViewModels;
using Reactive.Bindings;
using UIKit;

namespace MasterDetail.iOS.Views
{
    public partial class BrowserViewController : BaseTableViewController<BrowseViewModel>
    {
        private TableViewSource Source { get; set; }

        public BrowserViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Source = new TableViewSource(TableView);
            TableView.Source = Source;

            var weakThis = new WeakReference<BrowserViewController>(this);
            ViewModel.Items.Subscribe(i => weakThis.Use(t => t.Source.DataSoruce = i));
            Observable.FromEventPattern<ItemSelectedEventArgs>(Source, nameof(Source.ItemSelected))
                .Select(v => v.EventArgs.Item)
                .SetCommand(ViewModel.NavigateToDetail);
            Observable.FromEventPattern(AddNewBarButton, nameof(AddNewBarButton.Clicked))
                .SetCommand(ViewModel.NavigateToNew);
        }

        private class TableViewSource : UITableViewSource
        {
            private List<Item> _dataSoruce;

            public List<Item> DataSoruce
            {
                get { return _dataSoruce; }
                set
                {
                    _dataSoruce = value;
                    TableView.Resolve()?.ReloadData();
                }
            }

            public event EventHandler<ItemSelectedEventArgs> ItemSelected;

            private WeakReference<UITableView> TableView { get; }

            public TableViewSource(UITableView tableView)
            {
                TableView = new WeakReference<UITableView>(tableView);
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return DataSoruce?.Count ?? 0;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell("Cell", indexPath) as ItemCell;
                var item = _dataSoruce[indexPath.Row];
                if (cell == null) { return null; }
                cell.TitleText = item.Title;
                cell.DescriptionText = item.Description;
                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var item = _dataSoruce[indexPath.Row];
                ItemSelected?.Invoke(this, new ItemSelectedEventArgs { Item = item });
            }
        }

        private class ItemSelectedEventArgs : EventArgs
        {
            public Item Item { get; set; }
        }
    }
}
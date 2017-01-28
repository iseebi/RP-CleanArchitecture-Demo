using Foundation;
using System;
using UIKit;

namespace MasterDetail.iOS.Views
{
    public partial class ItemCell : UITableViewCell
    {
        public string TitleText
        {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        public string DescriptionText
        {
            get { return DescriptionLabel.Text; }
            set { DescriptionLabel.Text = value; }
        }

        public ItemCell (IntPtr handle) : base (handle)
        {
        }
    }
}
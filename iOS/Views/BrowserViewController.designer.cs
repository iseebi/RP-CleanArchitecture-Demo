// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MasterDetail.iOS.Views
{
    [Register ("BrowserViewController")]
    partial class BrowserViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem AddNewBarButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddNewBarButton != null) {
                AddNewBarButton.Dispose ();
                AddNewBarButton = null;
            }
        }
    }
}
﻿
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
using NativeAndForms.Views;

namespace NativeAndForms.Droid
{
    [Activity(Label = "TabOneActivity")]
    public class TabOneActivity : XamarinFormsActivity<TabOnePage>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}

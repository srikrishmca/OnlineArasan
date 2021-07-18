﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OnlineArasan.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Icon = "@mipmap/icon", MainLauncher = true,
        LaunchMode = Android.Content.PM.LaunchMode.SingleInstance, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));

            // Create your application here
        }
    }
}
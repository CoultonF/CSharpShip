﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharp;
using Auth0;
using Xamarin.Auth;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WhetherU
{
    [Activity(Label = "WhetherU", MainLauncher = true, Icon = "@drawable/icon")]
    public class StartUpActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPrompt);

            // Get our button from the layout resource,
            // and attach an event to it
            Button withInsta = FindViewById<Button>(Resource.Id.UseInstagram);
            Button withoutInsta = FindViewById<Button>(Resource.Id.NoInstagram);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            withInsta.Click += delegate {
                Console.Write("Button Clicked");
              StartActivity(typeof(InstagramLogin));
            };
            withoutInsta.Click += delegate {
                Console.Write("Button Clicked");
                StartActivity(typeof(MainScreen));
            };
        }
       
    }
}

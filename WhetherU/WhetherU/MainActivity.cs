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
    public class MainActivity : Activity
    {

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            var clientId = "33665d08e62942c6b1f484f422bbb7c1";
            var clientSecret = "ae94378b39e747b0ac5d6f8640636b02 ";
            var redirectUri = "";
            var realtimeUri = "";

            InstagramConfig config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);


            var scopes = new List<OAuth.Scope>();
            scopes.Add(InstaSharp.OAuth.Scope.Likes);
            scopes.Add(InstaSharp.OAuth.Scope.Comments);

            var link = InstaSharp.OAuth.AuthLink(config.OAuthUri + "authorize", config.ClientId, config.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Code);
        }
    }
}


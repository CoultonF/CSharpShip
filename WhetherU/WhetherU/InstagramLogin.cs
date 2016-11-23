using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharp;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using Instagram.Client;

namespace WhetherU
{
    [Activity(Label = "InstagramSignIn")]
    public class InstagramLogin : Activity
    {

        public static OAuthSettings XamarinAuthSettings { get; private set; }

        /// <summary>
        /// A static intance of IInstagramClient.
        /// </summary>
        /// <value>The Instagram client.</value>
        /// <remarks>Not the cleanest way to use the Instagram, but it works. IoC/DI would be better (and recommended), but this app is meant to be simple.</remarks>
        public static IInstagramClient InstagramClient { get; private set; }

        public static bool IsAuthenticated
        {
            get { return (!String.IsNullOrWhiteSpace(Token)); }
        }
        

        /// <summary>
        /// The Instagram API token returned from a successful login. This token is unique to each Instagram user.
        /// </summary>
        /// <value>The Instagram API token.</value>
        public static string Token { get; private set; }

        public async void SaveCredentials(string token)
        {

            if (!string.IsNullOrWhiteSpace(token))
            {
                var InstagramClient = new InstagramClient(token);

                var info = await InstagramClient.GetMyUserAsync();

                Console.WriteLine(info.Data.Username);

                Account account = new Account
                {
                    Username = info.Data.Username
                };
                account.Properties.Add("Token", token);
                AccountStore.Create(this).Save(account, "WhetherU");
            }
        }
        

        public async void runApp(String token)
        {

            var InstagramClient = new InstagramClient(token);

            await InstagramClient.GetMyUserAsync();

            Console.Write(InstagramClient.GetType());
            var mainActivity = new Intent(this, typeof(MainScreen));
            mainActivity.PutExtra("UserToken", token);
            StartActivity (mainActivity);
            SetContentView(Resource.Layout.Main);


        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {

                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.Main);
                // Set our view from the "main" layout resource
                
                var clientId = "33665d08e62942c6b1f484f422bbb7c1";
                var clientSecret = "ae94378b39e747b0ac5d6f8640636b02 ";
                var redirectUri = "https://elfsight.com/service/generate-instagram-access-token/";
                var realtimeUri = "";

                var auth = new OAuth2Authenticator(
                    clientId: clientId,
                    scope: "basic",
                    authorizeUrl: new Uri("https://api.instagram.com/oauth/authorize/"),
                    redirectUrl: new Uri(redirectUri));
                var token = "";
                auth.Completed += (s, ee) => {
                    token = ee.Account.Properties["access_token"];

                };
                StartActivity(auth.GetUI(this));

                auth.Completed += (sender, eventArgs) =>
                {
                    // We presented the UI, so it's up to us to dimiss it on iOS.
                    //DismissViewController(true, null);

                    if (eventArgs.IsAuthenticated)
                    {

                        // Use eventArgs.Account to do wonderful things
                        SaveCredentials(token);
                        runApp(token);


                    }
                    else
                    {
                        // The user cancelled

                        StartActivity(typeof(StartUpActivity));

                    }
                };

            }
            
            
        }

    }
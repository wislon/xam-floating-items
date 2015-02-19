using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace FloatingItems.Android
{
    [Activity(Label = "FloatingItems.Android", 
        AlwaysRetainTaskState = true,

        MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AndroidGameActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var application = new CCApplication();


            application.ApplicationDelegate = new GameAppDelegate();
            SetContentView(application.AndroidContentView);
            application.StartGame();

        }
    }

    public class GameAppDelegate : CCApplicationDelegate
    {
    }
}


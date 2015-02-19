using System;
using System.Linq;
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
    [Activity(Label = "FloatingItems.Android", AlwaysRetainTaskState = true, MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AndroidGameActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string[] assetImageNames = Assets.List("Content").Where(a => a.ToLowerInvariant().EndsWith(".png")).ToArray();

            var application = new CCApplication { ApplicationDelegate = new FloatingAppDelegate(assetImageNames) };

            SetContentView(application.AndroidContentView);
            application.StartGame();

        }
    }
}


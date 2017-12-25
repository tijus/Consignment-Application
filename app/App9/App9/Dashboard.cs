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
using Android.Webkit;
using Android.Content.PM;
using Android.Net;

namespace App9
{
    [Activity(Label = "Dashboard", Icon = "@drawable/xs", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Dashboard : Activity
    {
        public WebView web_view;
        private Boolean doubleBackPress = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {  
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.dashboard);
                Dependencies netconn = new Dependencies();
                var conncheck = netconn.CheckInternetConnection();
                if (conncheck)
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    SlidingTabsFragment fragment = new SlidingTabsFragment();
                    transaction.Replace(Resource.Id.sample_content_fragment, fragment);
                    transaction.Commit();
                }
                 else
                {
                    Toast.MakeText(this, "Check your Internet Connection", ToastLength.Long).Show();
                }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.help:
                    //Toast.MakeText(this, "Help item", ToastLength.Long).Show();
                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.Clear();
                    editor.Apply();
                    StartActivity(typeof(MainActivity));
                    return true;
                    
            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnBackPressed()
        {
            if (doubleBackPress)
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

            }


            this.doubleBackPress = true;
            Toast.MakeText(this, "Please click BACK again to exit", ToastLength.Long).Show();
            //
        }
    }
}
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

namespace App9
{
    [Activity(Label = "WebViewActivity")]
    public class WebViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.dashboard);

            String url = Intent.GetStringExtra("url");
            String group = Intent.GetStringExtra("group");
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("parameterpassing", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("PASSEDURL", url);
            editor.PutString("Group", group);
            editor.Apply();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            WebViewFragment fragment = new WebViewFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();
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
                case Resource.Id.menu:
                    Toast.MakeText(this, "Menu item", ToastLength.Long).Show();
                    //do something
                    return true;
                case Resource.Id.new_game:
                    Toast.MakeText(this, "Game item", ToastLength.Long).Show();
                    //do something
                    return true;
                case Resource.Id.help:
                    Toast.MakeText(this, "Help item", ToastLength.Long).Show();
                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.Clear();
                    editor.Apply();
                    StartActivity(typeof(MainActivity));
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
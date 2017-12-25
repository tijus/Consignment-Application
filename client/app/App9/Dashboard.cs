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
    [Activity(Label = "Dashboard",Icon = "@drawable/xs")]
    public class Dashboard : Activity
    {
        /*TextView company_code;
        TextView login_id;
        TextView password;*/

        private string storevalue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.dashboard);

            
            //String companycode = Intent.GetStringExtra("company_code");
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment, storevalue);
            transaction.Commit();

            
            

        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return true;
        }
/*
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);

            return base.OnPrepareOptionsMenu(menu);
        }*/

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu:
                    Toast.MakeText(this, "About Us", ToastLength.Long).Show();
                    //do something
                    return true;
                case Resource.Id.new_game:
                    Toast.MakeText(this, "Contact Us", ToastLength.Long).Show();
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
        
        

         
         /*       company_code = FindViewById<TextView>(Resource.Id.company_code);
                login_id = FindViewById<TextView>(Resource.Id.login_id);
                password = FindViewById<TextView>(Resource.Id.password);
                company_code.Text = Intent.GetStringExtra("company_code");
                login_id.Text = Intent.GetStringExtra("login_id");
                password.Text = Intent.GetStringExtra("password");*/

        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        
        

    }
}
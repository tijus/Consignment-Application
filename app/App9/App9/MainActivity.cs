using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Collections.Specialized;
using Android.Media;
using System.IO;
using Android.Preferences;
using Android.Content.PM;
using Newtonsoft.Json;
using System.Collections.Generic;
using App9.Models;

namespace App9
{
    [Activity(Label = "RLogic", Icon = "@drawable/xs", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        private static EditText company_code;
        private static EditText login_id;
        private static EditText password;
        private static WebClient client;
        private static Uri uri;
        private static CheckBox rm;
        
        private Boolean doubleBackPress=false;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.loginbtn);
            company_code = FindViewById<EditText>(Resource.Id.editView1);
            login_id = FindViewById<EditText>(Resource.Id.login_id);
            password = FindViewById<EditText>(Resource.Id.password);
            rm = FindViewById<CheckBox>(Resource.Id.rm_chk);

            button.Click +=button_Click;
        }


        private void button_Click(object sender, EventArgs e)
        {
            Dependencies netconn = new Dependencies();
            var conncheck = netconn.CheckInternetConnection();
            if (conncheck)
            {
                client = new WebClient();
                string uri = "http://192.168.0.9/rlogicmob/UserAuthentication/ValidateUser?CompanyCode=" + company_code.Text + "&Login=" + login_id.Text + "&Password=" + password.Text;
                string url = client.DownloadString(uri);
                
                   
                    //auth = JsonConvert.DeserializeObject<List<Models.MApp>>(res);
                    //RootObject obj = JsonConvert.DeserializeObject<RootObject>(res);
                    RootObject obj = JsonConvert.DeserializeObject<RootObject>(url);

                    int result = obj.MessageId;

                    if (result == 0)
                    {
                        if (rm.Checked)
                        {
                            ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = prefs.Edit();
                            editor.PutString("CompanyCode", company_code.Text);
                            editor.PutString("username", login_id.Text);
                            editor.PutString("password", password.Text);
                            editor.Apply();
                        }
                        else
                        {
                            ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                            ISharedPreferencesEditor editor = prefs.Edit();
                            editor.PutString("CompanyCode", company_code.Text);
                            editor.Apply();
                        }

                        Intent intent = new Intent(this, typeof(Dashboard));
                        this.StartActivity(intent);

                    }
                    else
                    {
                        Toast.MakeText(this, obj.Message, ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Check your Internet Connection", ToastLength.Long).Show();
                }

            }
        

        
        public override void OnBackPressed()
        {
            if (doubleBackPress)
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());  
                
            }
            

            this.doubleBackPress=true;
            Toast.MakeText(this, "Please click BACK again to exit", ToastLength.Long).Show();
            //
        }
    }
}


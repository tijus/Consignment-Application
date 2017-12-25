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

namespace App9
{
    [Activity(Label = "RLogic", Icon = "@drawable/xs")]
    public class MainActivity : Activity
    {

        private static EditText company_code;
        private static EditText login_id;
        private static EditText password;
        private static WebClient client;
        private static Uri uri;
        private static CheckBox rm;


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

            button.Click += button_Click;
        }

        
        private void button_Click(object sender, EventArgs e)
        {

            client = new WebClient();
            uri = new Uri("http://projectlogic.esy.es/Authenticator.php");
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("company_code", company_code.Text );
            parameters.Add("username", login_id.Text);
            parameters.Add("password", password.Text);       
            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parameters);            
        }

        private void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            client = new WebClient();
            String text = client.DownloadString("http://projectlogic.esy.es/Authenticator.php?company_code=" + company_code.Text + "&username=" + login_id.Text + "&password=" + password.Text);
            if (text.Contains("success"))
            {
                Toast.MakeText(this, "Login Successful", ToastLength.Long).Show();
                if (rm.Checked)
                {
                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("CompanyCode", company_code.Text);
                    editor.Apply();
                }
                else
                {
                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("CompanyCode", company_code.Text);
                    editor.PutString("username", login_id.Text);
                    editor.PutString("password", password.Text);
                    editor.Apply();
                }
                
                //Toast.MakeText(this, text, ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(Dashboard));
                /*
                intent.PutExtra("company_code", company_code.Text);
                intent.PutExtra("login_id", login_id.Text);
                intent.PutExtra("password", password.Text);*/
                this.StartActivity(intent);
                
            }
            else if (text.Contains("failure"))
            {
                Toast.MakeText(this, "Your company is not registered", ToastLength.Long).Show();

            }
            else {
                Toast.MakeText(this, text, ToastLength.Long).Show();
            }
                 
     
        }
        public override void OnBackPressed()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}


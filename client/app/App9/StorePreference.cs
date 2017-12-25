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
    [Activity(Label = "RLogic", MainLauncher=true)]
    public class StorePreference : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            


            ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            String companycode = prefs.GetString("CompanyCode", String.Empty);
            String uname = prefs.GetString("username", String.Empty);
            String pass = prefs.GetString("password", String.Empty);


            if (uname == String.Empty || pass == String.Empty)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(Dashboard));
                
                this.StartActivity(intent);
                

            }

        }
    }
}


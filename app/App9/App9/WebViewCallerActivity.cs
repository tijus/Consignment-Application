using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Webkit;
using Java.IO;
using Android.Graphics;
using Android.Content.PM;


namespace App9
{
    [Activity(Label = "RLogic", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class WebViewCallerActivity : Activity
    {
        IValueCallback mUploadMessage;
        private static int FILECHOOSER_RESULTCODE = 1;
        private ProgressBar mProgressBar;
        #region Override Methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.WebViewFrame);
            Dependencies netconn = new Dependencies();
            var conncheck = netconn.CheckInternetConnection();
            if (conncheck)
            {
                // Get our button from the layout resource,
                // and attach an event to it
                mProgressBar = FindViewById<ProgressBar>(Resource.Id.mProgressBar);

                var chrome = new FileChooserWebChromeClient((uploadMsg, acceptType, capture) =>
                {

                    mUploadMessage = uploadMsg;

                    File imageStorageDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "RlogicImageGallery");
                    if (!imageStorageDir.Exists())
                    {
                        // Create AndroidExampleFolder at sdcard
                        imageStorageDir.Mkdir();
                    }
                    File file = new File(
                                        imageStorageDir + File.Separator + "IMG_"
                                        + System.Environment.TickCount.ToString()
                                        + ".jpg");
                    var mCapturedImageURI = Android.Net.Uri.FromFile(file);
                    Intent captureIntent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                    captureIntent.PutExtra(Android.Provider.MediaStore.ExtraOutput, mCapturedImageURI);
                    var i = new Intent(Intent.ActionGetContent);
                    i.AddCategory(Intent.CategoryOpenable);
                    i.SetType("image/*");

                    Intent chooserIntent = Intent.CreateChooser(i, "Image Chooser");


                    chooserIntent.PutExtra(Intent.ExtraInitialIntents, captureIntent);

                    StartActivityForResult(chooserIntent, FILECHOOSER_RESULTCODE);
                });

                String url = Intent.GetStringExtra("url");
                var webview = this.FindViewById<WebView>(Resource.Id.webView);
                webview.SetWebViewClient(new WebViewClient());
                webview.SetWebChromeClient(chrome);
                webview.Settings.JavaScriptEnabled = true;
                webview.LoadUrl(url);
                mProgressBar.Visibility = ViewStates.Gone;
            }
            else 
            {
                Toast.MakeText(this, "Check your Internet Connection", ToastLength.Long).Show();
            }
            
        }
        
        public void onProgressChanged()
        {
            mProgressBar.Visibility = ViewStates.Gone;
        }
        #endregion

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            if (requestCode == FILECHOOSER_RESULTCODE)
            {
                if (null == mUploadMessage)
                    return;
                Java.Lang.Object result = intent == null || resultCode != Result.Ok
                    ? null
                    : intent.Data;
                mUploadMessage.OnReceiveValue(result);
                mUploadMessage = null;
            }
            
        }
    }

    partial class FileChooserWebChromeClient : WebChromeClient
    {
        Action<IValueCallback, Java.Lang.String, Java.Lang.String> callback;

        public FileChooserWebChromeClient(Action<IValueCallback, Java.Lang.String, Java.Lang.String> callback)
        {
            this.callback = callback;
        }

        //For Android 4.1
        [Java.Interop.Export]
        public void openFileChooser(IValueCallback uploadMsg, Java.Lang.String acceptType, Java.Lang.String capture)
        {
            
            callback(uploadMsg, acceptType, capture);
        }
    }
}
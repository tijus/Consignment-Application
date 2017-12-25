using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Webkit;
using System.Net;
using Newtonsoft.Json;

namespace App9
{
    public class WebViewFragment : Fragment
    {
        private WebViewScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.webviewfragment, container, false);


        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<WebViewScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class Groups
        {
            public string Group { get; set; }
        }
        public class Pages
        {
            public string Menu { get; set; }
            public string Url { get; set; }
            public string Group { get; set; }
        }

        public class HelloWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }
        }

        public class SamplePagerAdapter : PagerAdapter
        {
            public List<string> items = new List<string>();
            public List<string> urllist = new List<string>();
            private WebClient client;
            private WebClient clientu;
            private Uri uri;
            
            private List<Pages> menu;
            private List<Groups> grp;
            
            private WebView web_view;

            public string companycode;

            public SamplePagerAdapter()
                : base()
            {


                ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                String companycode = prefs.GetString("CompanyCode", String.Empty);
                ISharedPreferences pref = Application.Context.GetSharedPreferences("parameterpassing", FileCreationMode.Private);
                String group = pref.GetString("Group", String.Empty);

                client = new WebClient();
                uri = new Uri("http://projectlogic.esy.es/GetData.php?company_group=" + companycode + "&group=" + group);
                string res = client.DownloadString(uri);
                menu = JsonConvert.DeserializeObject<List<Pages>>(res);
                clientu = new WebClient();
                uri = new Uri("http://projectlogic.esy.es/GetGroup.php?company_code=" + companycode);
                string result = client.DownloadString(uri);
                grp = JsonConvert.DeserializeObject<List<Groups>>(result);
                foreach (var item_grp in grp)
                {
                    items.Add(item_grp.Group.ToString());
                }

                foreach (var url_item in menu)
                {
                    urllist.Add(url_item.Url.ToString());
                }


            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {

                string [] urls = urllist.ToArray();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.WebViewFrame, container, false);
                container.AddView(view);
                //ISharedPreferences prefs = Application.Context.GetSharedPreferences("parameterpassing", FileCreationMode.Private);
                //String url = prefs.GetString("PASSEDURL", String.Empty);
                web_view = view.FindViewById<WebView>(Resource.Id.webView);
                web_view.Settings.JavaScriptEnabled = true;
                web_view.LoadUrl(urls[position]);
                web_view.SetWebViewClient(new HelloWebViewClient());
                
                return view;
            }
            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }
    }
}
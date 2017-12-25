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
using System.Net;
using Newtonsoft.Json;
using Android.Webkit;

namespace App9
{
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();
            mViewPager.OffscreenPageLimit = 10;
            mSlidingTabScrollView.ViewPager = mViewPager;
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
            private WebClient client;
            private Uri uri;
            private List<Models.Groups> group;
            private List<Models.Menus> menu;
            private ListView mListView;
            public string companycode;
            
            public SamplePagerAdapter()
                : base()
            {


                ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                String companycode = prefs.GetString("CompanyCode", String.Empty);
                client = new WebClient();
                uri = new Uri("http://projectlogic.esy.es/GetGroup.php?company_code=" + companycode);
                string res=client.DownloadString(uri);
                group = JsonConvert.DeserializeObject<List<Models.Groups>>(res);
                
                foreach(var item_group in group)
                {
                    items.Add(item_group.Group.ToString());
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
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                container.AddView(view);
                string[] grouparr = items.ToArray();
                
                List<string> menus = new List<string>();
                List<string> urls = new List<string>();
                mListView = view.FindViewById<ListView>(Resource.Id.listView);
                client = new WebClient();
                ISharedPreferences prefs = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                String companycode = prefs.GetString("CompanyCode", String.Empty);

                string menuResult = client.DownloadString("http://projectlogic.esy.es/OldGetData.php?company_group=" + companycode + "&group=" + grouparr[position]);
                menu = JsonConvert.DeserializeObject<List<Models.Menus>>(menuResult);
                foreach (var menuList in menu)
                {
                    menus.Add(menuList.Menu.ToString());
                    urls.Add(menuList.Url.ToString());
                }
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(container.Context, Android.Resource.Layout.SimpleListItem1, menus);
                mListView.Adapter = adapter;
                string[] urlsarr = urls.ToArray();

                mListView.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e)
                {
                    int urllink = e.Position;
                    Intent intent = new Intent(container.Context, typeof(WebViewCallerActivity));
                    intent.PutExtra("url", urlsarr[urllink]);
                    intent.PutExtra("group", grouparr[position]);
                    container.Context.StartActivity(intent);
                };
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
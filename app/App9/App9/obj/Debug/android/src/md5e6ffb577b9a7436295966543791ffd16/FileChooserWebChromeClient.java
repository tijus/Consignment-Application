package md5e6ffb577b9a7436295966543791ffd16;


public class FileChooserWebChromeClient
	extends android.webkit.WebChromeClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_openFileChooser:(Landroid/webkit/ValueCallback;Ljava/lang/String;Ljava/lang/String;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("App9.FileChooserWebChromeClient, App9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FileChooserWebChromeClient.class, __md_methods);
	}


	public FileChooserWebChromeClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FileChooserWebChromeClient.class)
			mono.android.TypeManager.Activate ("App9.FileChooserWebChromeClient, App9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void openFileChooser (android.webkit.ValueCallback p0, java.lang.String p1, java.lang.String p2)
	{
		n_openFileChooser (p0, p1, p2);
	}

	private native void n_openFileChooser (android.webkit.ValueCallback p0, java.lang.String p1, java.lang.String p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

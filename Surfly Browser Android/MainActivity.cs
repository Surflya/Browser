using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace Surfly_Browser_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private WebView mwebView;
        private EditText mTxtURL;
        WebViewClient mWebViewClient = new WebViewClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mwebView = FindViewById<WebView>(Resource.Id.webView1);
            mTxtURL = FindViewById<EditText>(Resource.Id.editText1);

            mwebView.Settings.JavaScriptEnabled = true;
            mwebView.LoadUrl("https://www.google.com");
            mwebView.SetWebViewClient(mWebViewClient);

            mTxtURL.Click += MTxtURL_Click;
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.Back)
            {
                mwebView.GoBack();
            }
            return true;
        }

        private void MTxtURL_Click(object sender, System.EventArgs e)
        {
#pragma warning disable CS0618 // 'WebViewClient.ShouldOverrideUrlLoading(WebView?, string?)' está obsoleto: 'deprecated'
            mWebViewClient.ShouldOverrideUrlLoading(mwebView, mTxtURL.Text);
#pragma warning restore CS0618 // 'WebViewClient.ShouldOverrideUrlLoading(WebView?, string?)' está obsoleto: 'deprecated'
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class WebClient : WebViewClient
        {
#pragma warning disable CS0672 // El miembro 'MainActivity.WebClient.ShouldOverrideUrlLoading(WebView, string)' invalida el miembro obsoleto 'WebViewClient.ShouldOverrideUrlLoading(WebView?, string?)'. Agregue el atributo Obsolete a 'MainActivity.WebClient.ShouldOverrideUrlLoading(WebView, string)'.
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
#pragma warning restore CS0672 // El miembro 'MainActivity.WebClient.ShouldOverrideUrlLoading(WebView, string)' invalida el miembro obsoleto 'WebViewClient.ShouldOverrideUrlLoading(WebView?, string?)'. Agregue el atributo Obsolete a 'MainActivity.WebClient.ShouldOverrideUrlLoading(WebView, string)'.
            {
                view.LoadUrl(url);
                return true;
            }
        }
    }
}
namespace Kapta.Droid
{
    using System;

    using Android.App;
    using Android.Content.PM;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using Android.OS;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using ImageCircle.Forms.Plugin.Droid;

    [Activity(Label = "Kapta", Icon = "@drawable/iconokapta", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            ImageCircleRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(
             int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(
                requestCode,
                permissions,
                grantResults);
        }

    }
}


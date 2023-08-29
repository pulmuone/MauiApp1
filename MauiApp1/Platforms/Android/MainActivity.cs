using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace MauiApp1
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
        , ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : MauiAppCompatActivity
    {
        static string[] PERMISSIONS = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.Window.AddFlags(Android.Views.WindowManagerFlags.KeepScreenOn);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) //23이상부터
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS, 0);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Platform.OnNewIntent(intent);
        }

    }
}
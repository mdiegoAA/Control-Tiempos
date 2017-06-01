using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Droid
{
    [Activity (Label = "Amezquita Control de Tiempos", Theme = "@android:style/Theme.Holo.Light", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Forms.Init (this, bundle);
			LoadApplication (new App());
		}
	}
}
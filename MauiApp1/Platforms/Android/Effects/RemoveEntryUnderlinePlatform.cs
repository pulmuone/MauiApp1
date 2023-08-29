using Android.Widget;
using Microsoft.Maui.Controls.Platform;
using Color = Android.Graphics.Color;

namespace MauiApp1.Platforms.Android.Effects
{
    public class RemoveEntryUnderlinePlatform : PlatformEffect
    {
        protected override void OnAttached()
        {
            var editText = this.Control as EditText;

            if (editText is null)
                throw new NotImplementedException();

            editText.SetBackgroundColor(Color.Transparent);
        }

        protected override void OnDetached()
        {
        }
    }
}

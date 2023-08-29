using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MauiApp1.Platforms.iOS.Effects
{
    public class RemoveEntryBordersEffectPlatform : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = this.Control as UITextField;

            if (textField is null)
                throw new NotImplementedException();

            textField.BorderStyle = UITextBorderStyle.None;
            textField.BackgroundColor = Colors.Transparent.ToUIColor();
        }

        protected override void OnDetached()
        {
        }
    }
}

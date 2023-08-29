using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;
using MauiApp1.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Platforms.Android.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer, Interfaces.IVirtualKeyboard
    {
        public ExtendedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if ((e.OldElement == null) && (Control != null))
            {
                var edittext = (EditText)Control;

                //edittext.SetPadding(0, 0, 0, 0);
                edittext.SetTextIsSelectable(true);
                edittext.SetSelectAllOnFocus(true);
                edittext.ShowSoftInputOnFocus = false; //true: 키보드 보임, false: 키보드 안보임

                var view = (ExtendedEntry)Element;

                view.VirtualKeyboardHandler = this;

                if (view.IsReadOnly == true)
                {
                    edittext.Enabled = false;
                }
                else
                {
                    edittext.Enabled = true;
                }
            }
        }

        public void ShowKeyboard()
        {
            try
            {
                if (Control.RequestFocus())
                {
                    var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    inputMethodManager.ShowSoftInput(Control, ShowFlags.Implicit);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void HideKeyboard()
        {
            try
            {
                if (Control.RequestFocus())
                {
                    var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    inputMethodManager.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

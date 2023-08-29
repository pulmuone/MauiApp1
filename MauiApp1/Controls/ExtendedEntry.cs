using MauiApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Controls
{
    public class ExtendedEntry : Entry
    {

        /// <summary>
        /// The ShowVirtualKeyboardOnFocus property
        /// </summary>
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty =
            BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);


        public static readonly BindableProperty TagNameProperty = BindableProperty.Create(
            propertyName: nameof(TagName),
            returnType: typeof(string),
            declaringType: typeof(ExtendedEntry),
            null,
            BindingMode.TwoWay);

        public IVirtualKeyboard VirtualKeyboardHandler { get; set; }

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public string TagName
        {
            get => (string)this.GetValue(TagNameProperty);
            set => this.SetValue(TagNameProperty, string.Empty);
        }

        public ExtendedEntry()
        {
            this.Focused += OnFocused;
            this.Unfocused -= OnFocused;
        }

        public new bool Focus()
        {
            if (ShowVirtualKeyboardOnFocus)
            {
                ShowKeyboard();
            }
            else
            {
                HideKeyboard();
            }

            this.CursorPosition = 0;
            this.SelectionLength = this.Text == null ? 0 : this.Text.Length;

            return true;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                if (ShowVirtualKeyboardOnFocus)
                {
                    ShowKeyboard();
                }
                else
                {
                    HideKeyboard();
                }
            }
        }

        public void ShowKeyboard()
        {
            VirtualKeyboardHandler?.ShowKeyboard();

        }

        public void HideKeyboard()
        {
            VirtualKeyboardHandler?.HideKeyboard();
        }
    }
}

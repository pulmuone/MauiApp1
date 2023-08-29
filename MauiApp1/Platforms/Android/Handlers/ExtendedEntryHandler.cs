using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using MauiApp1.Controls;
using MauiApp1.Interfaces;
using Microsoft.Maui.Handlers;
using static Android.Views.View;
using content = Android.Content;
using TextChangedEventArgs = Android.Text.TextChangedEventArgs;
using view = Android.Views;

namespace MauiApp1.Platforms.Android.Handlers
{
    public class ExtendedEntryHandler : EntryHandler, IVirtualKeyboard
    {
        public static PropertyMapper<ExtendedEntry, ExtendedEntryHandler> PropertyMapper = new PropertyMapper<ExtendedEntry, ExtendedEntryHandler>(ViewHandler.ViewMapper)
        {
            //[nameof(ExtendedEntry.ShowKeyboard)] = MapVirtualKeyboardToggle
        };

        //동작하지 않음.
        public static new CommandMapper<ExtendedEntry, ExtendedEntryHandler> CommandMapper = new(ViewCommandMapper)
        {
            //[nameof(ExtendedEntry.ShowKeyboard)] = MapShowKeyboardRequested,
            //[nameof(ExtendedEntry.HideKeyboard)] = MapHideKeyboardRequested
        };

        //#0
        public ExtendedEntryHandler(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {

        }

        //#0
        public ExtendedEntryHandler() : base(PropertyMapper)
        {

        }

        //핸들러 기본 실행 #1
        protected override AppCompatEditText CreatePlatformView()
        {
            return new AppCompatEditText(base.Context);
        }

        //#2
        public override void SetVirtualView(IView view)
        {
            base.SetVirtualView(view);

            var entry = (ExtendedEntry)view;

            entry.VirtualKeyboardHandler = this;
        }

        //핸들러 기본 실행 #3
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(PlatformView);

            //VirtualView : Cross-platform Control 접근

            //platformView.SetPadding(0, 0, 0, 0);
            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            //platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
            platformView.SetSingleLine(true);
            platformView.InputType = InputTypes.ClassText;
            //platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));
            platformView.EditorAction += PlatformView_EditorAction;
            //platformView.TextChanged += OnTextChanged;
            //platformView.FocusChange += OnFocusedChange;
            //platformView.Touch += OnTouch;

            if (this.VirtualView.IsReadOnly)
            {
                platformView.Enabled = false;
            }
            else
            {
                platformView.Enabled = true;
            }

            if (VirtualView.Keyboard == Keyboard.Numeric)
            {
                //platformView.SetRawInputType(InputTypes.ClassNumber);
                platformView.InputType = InputTypes.ClassNumber;
            }
            else if (VirtualView.Keyboard == Keyboard.Text)
            {
                //platformView.SetRawInputType(InputTypes.ClassText);
                platformView.InputType = InputTypes.ClassText;
            }
            else
            {
                platformView.InputType = InputTypes.ClassText;
            }
        }

        //핸들러 기본 실행 #3
        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            platformView.EditorAction -= PlatformView_EditorAction;
            //platformView.TextChanged -= OnTextChanged;
            //platformView.FocusChange -= OnFocusedChange;
            //platformView.Touch -= OnTouch;

            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        void OnTextChanged(object? sender, TextChangedEventArgs e) =>
         VirtualView?.UpdateText(e);

        // This will eliminate additional native property setting if not required.
        void OnFocusedChange(object? sender, FocusChangeEventArgs e)
        {
            if (VirtualView?.ClearButtonVisibility == ClearButtonVisibility.WhileEditing)
                UpdateValue(nameof(IEntry.ClearButtonVisibility));
        }

        // Check whether the touched position inbounds with clear button.
        //void OnTouch(object? sender, TouchEventArgs e) =>
        //    e.Handled =
        //        VirtualView?.ClearButtonVisibility == ClearButtonVisibility.WhileEditing &&
        //        PlatformView.HandleClearButtonTouched(VirtualView.FlowDirection, e, GetClearButtonDrawable);


        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                VirtualView?.Completed();
            }

            e.Handled = true;
        }

        public void ShowKeyboard()
        {
            this.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(this.PlatformView, ShowFlags.Forced);
        }

        public void HideKeyboard()
        {
            this.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        class MyOnKeyListener : Java.Lang.Object, IOnKeyListener
        {
            IView vv;
            public MyOnKeyListener(IView view)
            {
                vv = view;
            }

            ///handler.PlatformView.RequestFocus(); 하면 한번 더 진입한다.
            public bool OnKey(view.View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
            {
                if (e.Action == KeyEventActions.Down && keyCode == Keycode.Enter)
                {
                    (vv as ExtendedEntry).SendCompleted();

                    return true;
                }

                return false;
            }
        }
    }
}

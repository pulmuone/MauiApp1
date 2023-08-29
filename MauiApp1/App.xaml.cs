using MauiApp1.Views;
using System.Text;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            //System.Text.Encoding.CodePages 초기화
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //밝은테마만 지원
            Application.Current.UserAppTheme = AppTheme.Light;

            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new MainView());
        }
    }
}
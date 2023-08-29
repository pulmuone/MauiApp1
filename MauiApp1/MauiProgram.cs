using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Handlers;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .UseMauiCompatibility() //AddCompatibilityRenderer을 사용 할 경우만
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers((handlers) =>
                {
#if ANDROID
				    ButtonHandler.Mapper.AppendToMapping(nameof(View.BackgroundColor), (handler, View) => handler.UpdateValue(nameof(IView.Background)));
                    handlers.AddCompatibilityRenderer(typeof(MauiApp1.Controls.ExtendedEntry), typeof(MauiApp1.Platforms.Android.Renderers.ExtendedEntryRenderer));
                    //handlers.AddHandler(typeof(MauiApp1.Controls.ExtendedEntry), typeof(MauiApp1.Platforms.Android.Handlers.ExtendedEntryHandler));
#endif

#if IOS

#endif
                })
                .ConfigureEssentials(essentials =>
                {
                    essentials.UseVersionTracking();
                })
                .ConfigureEffects(effects =>
                {
#if ANDROID
				    effects.Add<MauiApp1.Effects.RemoveEntryBordersEffect, MauiApp1.Platforms.Android.Effects.RemoveEntryBordersEffectPlatform>();
				    effects.Add<MauiApp1.Effects.RemoveEntryUnderline, MauiApp1.Platforms.Android.Effects.RemoveEntryUnderlinePlatform>();
#endif

#if IOS
				    effects.Add<MauiApp1.Effects.RemoveEntryBordersEffect, MauiApp1.Platforms.iOS.Effects.RemoveEntryBordersEffectPlatform>();
				    effects.Add<MauiApp1.Effects.RemoveEntryUnderline, MauiApp1.Platforms.iOS.Effects.RemoveEntryUnderlinePlatform>();
#endif
                });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
using Android.App;
using Android.Content.Res;
using Android.Runtime;
using Microsoft.Maui.Handlers;

namespace StudentTaskTechVegas
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            EntryHandler.Mapper.AppendToMapping("CustomEntryHandler", (handler, view) =>
            {
                // Custom mapping for Entry if needed
                 handler.PlatformView.BackgroundTintList=ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            });
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}

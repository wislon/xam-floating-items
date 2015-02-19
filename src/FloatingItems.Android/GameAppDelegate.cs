using CocosSharp;

namespace FloatingItems.Android
{
    public class GameAppDelegate : CCApplicationDelegate
    {
        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            //base.ApplicationDidFinishLaunching(application, mainWindow);
            application.PreferMultiSampling = false;
            application.ContentRootDirectory = "Content";

            var bounds = mainWindow.WindowSizeInPixels;
            CCScene.SetDefaultDesignResolution(bounds.Width, bounds.Height, CCSceneResolutionPolicy.ShowAll);

            // TODO game scene init goes here.

        }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            base.ApplicationDidEnterBackground(application);
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            base.ApplicationWillEnterForeground(application);
        }
    }
}
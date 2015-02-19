using CocosSharp;

namespace FloatingItems.Android
{
    public class FloatingAppDelegate : CCApplicationDelegate
    {
        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            //base.ApplicationDidFinishLaunching(application, mainWindow);
            application.PreferMultiSampling = false;
            application.ContentRootDirectory = "Content";

            var bounds = mainWindow.WindowSizeInPixels;
            CCScene.SetDefaultDesignResolution(bounds.Width, bounds.Height, CCSceneResolutionPolicy.ShowAll);

            var gameScene = new FloatingScene(mainWindow);
            mainWindow.RunWithScene(gameScene);

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
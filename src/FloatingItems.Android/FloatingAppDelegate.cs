using CocosSharp;

namespace FloatingItems.Android
{
    public class FloatingAppDelegate : CCApplicationDelegate
    {
        private readonly string[] assetImageNames;

        public FloatingAppDelegate(string[] assetImageNames)
        {
            this.assetImageNames = assetImageNames;
        }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            application.PreferMultiSampling = false;
            application.ContentRootDirectory = "Content";

            var bounds = mainWindow.WindowSizeInPixels;
            CCScene.SetDefaultDesignResolution(bounds.Width, bounds.Height, CCSceneResolutionPolicy.ShowAll);

            var gameScene = new FloatingScene(mainWindow, assetImageNames);
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
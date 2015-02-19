using CocosSharp;

namespace FloatingItems.Android
{
    public class GameScene : CCScene
    {
        private CCLayer mainLayer;

        public GameScene(CCWindow window) : base(window)
        {
            mainLayer = new CCLayer();
            AddChild(mainLayer);
        }
    }
}
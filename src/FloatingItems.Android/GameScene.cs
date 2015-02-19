using CocosSharp;

namespace FloatingItems.Android
{
    public class GameScene : CCScene
    {
        private CCLayer mainLayer;
        private CCSprite headphoneSprite;

        public GameScene(CCWindow window) : base(window)
        {
            mainLayer = new CCLayer();
            AddChild(mainLayer);

            headphoneSprite = new CCSprite("red_beats") {PositionX = 100, PositionY = 100, Scale = 0.5f};
            mainLayer.AddChild(headphoneSprite);
        }
    }
}
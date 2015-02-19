using CocosSharp;
using Java.Lang;

namespace FloatingItems.Android
{
    public class GameScene : CCScene
    {
        private float xVelocity;
        private float yVelocity;
        private readonly CCSprite headphoneSprite;
        private float screenRight;
        private float screenLeft;
        private float screenTop;
        private float screenBottom;

        const float MinXVelocity = -300;
        const float MaxXVelocity = 300;
        const float MinYVelocity = - 300;
        const float MaxYVelocity = 300;

        public GameScene(CCWindow window) : base(window)
        {
            var mainLayer = new CCLayer();
            AddChild(mainLayer);

            headphoneSprite = new CCSprite("red_beats") {PositionX = 100, PositionY = 100, Scale = 0.5f};
            mainLayer.AddChild(headphoneSprite);

            xVelocity = CCRandom.GetRandomFloat(MinXVelocity, MaxXVelocity);
            yVelocity = CCRandom.GetRandomFloat(MinYVelocity, MaxYVelocity);

            // get screen edges
            screenRight = mainLayer.VisibleBoundsWorldspace.MaxX;
            screenLeft = mainLayer.VisibleBoundsWorldspace.MinX;
            screenTop = mainLayer.VisibleBoundsWorldspace.MaxY;
            screenBottom = mainLayer.VisibleBoundsWorldspace.MinY;


            Schedule(RunFloatingLogic);
        }

        private void RunFloatingLogic(float frameTimeInSeconds)
        {
            float currentX = xVelocity * frameTimeInSeconds;
            float currentY = yVelocity * frameTimeInSeconds;
            headphoneSprite.Position = new CCPoint(currentX, currentY);
        }
    }
}
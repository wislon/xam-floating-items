using CocosSharp;

namespace FloatingItems.Android
{
    public class FloatingScene : CCScene
    {
        private float xVelocity;
        private float yVelocity;
        private readonly CCSprite headphoneSprite;
        private readonly float screenRight;
        private readonly float screenLeft;
        private readonly float screenTop;
        private readonly float screenBottom;
        private CCRotateBy rotateSprite;

        const float MinXVelocity = -300;
        const float MaxXVelocity = 300;
        const float MinYVelocity = -300;
        const float MaxYVelocity = 300;

        public FloatingScene(CCWindow window) : base(window)
        {
            var mainLayer = new CCLayer();
            AddChild(mainLayer);

            // get screen edges
            screenRight = mainLayer.VisibleBoundsWorldspace.MaxX;
            screenLeft = mainLayer.VisibleBoundsWorldspace.MinX;
            screenTop = mainLayer.VisibleBoundsWorldspace.MaxY;
            screenBottom = mainLayer.VisibleBoundsWorldspace.MinY;

            headphoneSprite = new CCSprite("red_beats") {PositionX = screenRight / 2, PositionY = screenTop / 2, Scale = 0.5f};
            mainLayer.AddChild(headphoneSprite);
            rotateSprite = new CCRotateBy(0.8f, 20);
            headphoneSprite.RepeatForever(rotateSprite);

            xVelocity = CCRandom.GetRandomFloat(MinXVelocity, MaxXVelocity);
            yVelocity = CCRandom.GetRandomFloat(MinYVelocity, MaxYVelocity);



            Schedule(RunFloatingLogic);
        }

        private void RunFloatingLogic(float frameTimeInSeconds)
        {
            float currentX = headphoneSprite.PositionX + xVelocity * frameTimeInSeconds;
            float currentY = headphoneSprite.PositionY + yVelocity * frameTimeInSeconds;

            //if (currentX < screenLeft) currentX = screenRight;
            //if (currentX > screenRight) currentX = screenLeft;
            //if (currentY < screenBottom) currentY = screenTop;
            //if (currentY > screenTop) currentY = screenBottom;

            headphoneSprite.Position = new CCPoint(currentX, currentY);

            float headphoneSpriteRight = headphoneSprite.BoundingBoxTransformedToParent.MaxX;
            float headphoneSpriteLeft = headphoneSprite.BoundingBoxTransformedToParent.MinX;
            float headphoneSpriteTop = headphoneSprite.BoundingBoxTransformedToParent.MaxY;
            float headphoneSpriteBottom = headphoneSprite.BoundingBoxTransformedToParent.MinY;

            bool shouldReflectXVelocity = headphoneSpriteRight >= screenRight && xVelocity > 0 ||
                                          headphoneSpriteLeft <= screenLeft && xVelocity < 0;

            bool shouldReflectYVelocity = headphoneSpriteTop >= screenTop && yVelocity > 0 ||
                                          headphoneSpriteBottom <= screenBottom && xVelocity < 0;


            xVelocity = xVelocity * (shouldReflectXVelocity ? -1 : 1);
            yVelocity = yVelocity * (shouldReflectYVelocity ? -1 : 1);


        }
    }
}
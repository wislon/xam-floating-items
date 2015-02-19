using System.Collections.Generic;
using CocosSharp;

namespace FloatingItems.Android
{
    public class FloatingScene : CCScene
    {
        const float MinXVelocity = -10;
        const float MaxXVelocity = 10;
        const float MinYVelocity = -10;
        const float MaxYVelocity = 10;

        private readonly List<SpriteObject> spritesList;

        public FloatingScene(CCWindow window, string[] assetImageNames) : base(window)
        {
            var mainLayer = new CCLayer();
            AddChild(mainLayer);

            // get screen edges
            float screenRight = mainLayer.VisibleBoundsWorldspace.MaxX;
            float screenLeft = mainLayer.VisibleBoundsWorldspace.MinX;
            float screenTop = mainLayer.VisibleBoundsWorldspace.MaxY;
            float screenBottom = mainLayer.VisibleBoundsWorldspace.MinY;

            var screenBounds = new float[] {screenTop, screenLeft, screenRight, screenBottom};

            spritesList = new List<SpriteObject>(assetImageNames.Length);
            foreach (var assetImageName in assetImageNames)
            {
                var positionX = CCRandom.GetRandomFloat(0, screenRight);
                var positionY = CCRandom.GetRandomFloat(0, screenTop);
                var velocityX = CCRandom.GetRandomFloat(MinXVelocity, MaxXVelocity);
                var velocityY = CCRandom.GetRandomFloat(MinYVelocity, MaxYVelocity);
                var scaleFactor = CCRandom.GetRandomFloat(0.5f, 0.8f);
                var rotationFactor = CCRandom.GetRandomFloat(-10f, 10f);
                var sprite = new SpriteObject(assetImageName, positionX, positionY, screenBounds, velocityX, velocityY, scaleFactor, rotationFactor);
                mainLayer.AddChild(sprite.Sprite);
                spritesList.Add(sprite);
            }

            Schedule(RunFloatingLogic);
        }

        private void RunFloatingLogic(float frameTimeInSeconds)
        {
            foreach (var spriteObject in spritesList)
            {
                spriteObject.DoAllInternalUpdates(frameTimeInSeconds);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Android.Animation;
using CocosSharp;

namespace FloatingItems.Android
{
    public class FloatingScene : CCScene
    {
        private readonly float screenRight;
        private readonly float screenLeft;
        private readonly float screenTop;
        private readonly float screenBottom;

        const float MinXVelocity = -300;
        const float MaxXVelocity = 300;
        const float MinYVelocity = -300;
        const float MaxYVelocity = 300;

        private List<SpriteObject> sprites;

        public FloatingScene(CCWindow window, string[] assetImageNames) : base(window)
        {
            var mainLayer = new CCLayer();
            AddChild(mainLayer);

            // get screen edges
            screenRight = mainLayer.VisibleBoundsWorldspace.MaxX;
            screenLeft = mainLayer.VisibleBoundsWorldspace.MinX;
            screenTop = mainLayer.VisibleBoundsWorldspace.MaxY;
            screenBottom = mainLayer.VisibleBoundsWorldspace.MinY;

            var screenBounds = new float[] {screenTop, screenLeft, screenRight, screenBottom};

            sprites = new List<SpriteObject>(assetImageNames.Length);
            foreach (var assetImageName in assetImageNames)
            {
                var positionX = CCRandom.GetRandomFloat(0, screenRight);
                var positionY = CCRandom.GetRandomFloat(0, screenTop);
                var velocityX = CCRandom.GetRandomFloat(MinXVelocity, MaxXVelocity);
                var velocityY = CCRandom.GetRandomFloat(MinYVelocity, MaxYVelocity);
                var scaleFactor = CCRandom.GetRandomFloat(0.2f, 1.2f);
                var rotationFactor = CCRandom.GetRandomFloat(-20f, 20f);
                var sprite = new SpriteObject(assetImageName, positionX, positionY, screenBounds, velocityX, velocityY, scaleFactor, rotationFactor);
                mainLayer.AddChild(sprite.Sprite);
                sprites.Add(sprite);
            }


            //CCSprite headphoneSprite = new CCSprite("red_beats") {PositionX = screenRight / 2, PositionY = screenTop / 2, Scale = 0.5f};
            //mainLayer.AddChild(headphoneSprite);

            //var rotateSprite = new CCRotateBy(0.8f, 20);
            //headphoneSprite.RepeatForever(rotateSprite);

            Schedule(RunFloatingLogic);
        }

        private void RunFloatingLogic(float frameTimeInSeconds)
        {
            foreach (var spriteObject in sprites)
            {
                spriteObject.DoAllInternalUpdates(frameTimeInSeconds);
            }
        }
    }

    public class SpriteObject
    {
        readonly float screenRight;
        readonly float screenLeft;
        readonly float screenTop;
        readonly float screenBottom;

        float spriteLeft;
        float spriteRight;
        float spriteTop;
        float spriteBottom;

        public CCSprite Sprite { get; set; }

        public float CurrentX { get; set; }
        public float CurrentY { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }


        public SpriteObject(string assetName, float positionX, float positionY, float[] screenBounds, float velocityX = 0, float velocityY = 0, float scaleFactor = 1, float rotationFactor = 0)
        {
            Sprite = new CCSprite(assetName) {PositionX = positionX, PositionY = positionY, Scale = scaleFactor};
            screenTop = screenBounds[0]; screenLeft = screenBounds[1]; screenRight = screenBounds[2]; screenBottom = screenBounds[3];

            if (Math.Abs(rotationFactor) > 0.00)
            {
                var rotation = new CCRotateBy(0.8f, rotationFactor);
                Sprite.RepeatForever(rotation);
            }

            VelocityX = velocityX;
            VelocityY = velocityY;
            DoAllInternalUpdates(0f);
        }

        /// <summary>
        /// Causes this object to run all its internal updates, to determine position, velocity,
        /// position in relation to parent, etc.
        /// Will be called each time a new animation refresh occurs, so keep this
        /// as quick as you can!
        /// </summary>
        public void DoAllInternalUpdates(float frameTimeInSeconds)
        {
            UpdateBoundingBoxInTermsOfParent(frameTimeInSeconds);

            if (Math.Abs(VelocityX) > 0.00 || Math.Abs(VelocityY) > 0.00)
            {
                CalculateVelocities(frameTimeInSeconds);
            }
            CalculateCurrentPosition(frameTimeInSeconds);
        }

        private void CalculateVelocities(float frameTimeInSeconds)
        {
            bool shouldReflectXVelocity = spriteRight >= screenRight && VelocityX > 0 ||
                                          spriteLeft <= screenLeft && VelocityX < 0;

            bool shouldReflectYVelocity = spriteTop >= screenTop && VelocityY > 0 ||
                                          spriteBottom <= screenBottom && VelocityY < 0;


            VelocityX = VelocityX * (shouldReflectXVelocity ? -1 : 1);
            VelocityY = VelocityY * (shouldReflectYVelocity ? -1 : 1);
        }

        void UpdateBoundingBoxInTermsOfParent(float frameTimeInSeconds)
        {
            spriteLeft = Sprite.BoundingBoxTransformedToParent.MinX;
            spriteRight = Sprite.BoundingBoxTransformedToParent.MaxX;
            spriteTop = Sprite.BoundingBoxTransformedToParent.MaxY;
            spriteBottom = Sprite.BoundingBoxTransformedToParent.MinY;
        }
        private void CalculateCurrentPosition(float frameTimeInSeconds)
        {
            CurrentX = Sprite.PositionX + VelocityX * frameTimeInSeconds;
            CurrentY = Sprite.PositionY + VelocityY * frameTimeInSeconds;
            Sprite.Position = new CCPoint(CurrentX, CurrentY);
        }
    }
}
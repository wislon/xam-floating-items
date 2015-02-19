using System;
using CocosSharp;

namespace FloatingItems.Android
{
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
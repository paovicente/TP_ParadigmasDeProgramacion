using System;

namespace EngineGDI
{
    public static class Collision
    {
        public static bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(positionA.X - positionB.X);
            float distanceY = Math.Abs(positionA.Y - positionB.Y);

            float sumHalfWidths = sizeA.X / 2 + sizeB.X / 2;
            float sumHalfHeights = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHalfWidths &&
                   distanceY <= sumHalfHeights;
        }
    }
}
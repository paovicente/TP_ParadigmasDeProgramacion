using System;

namespace EngineGDI
{
    public class Animation
    {
        private readonly Renderer renderer;
        private readonly string[] frames;

        private int currentFrame;
        private float timer;
        private readonly float frameDuration;

        public Animation(Renderer renderer, string[] frames, float frameDuration)
        {
            this.renderer = renderer;
            this.frames = frames;
            this.frameDuration = frameDuration;

            currentFrame = 0;
            timer = 0f;

            if (frames.Length > 0)
                renderer.TexturePath = frames[0];
        }

        public void Update(float deltaTime)
        {
            if (frames.Length <= 1)
                return;

            timer += deltaTime;

            if (timer >= frameDuration)
            {
                timer -= frameDuration;

                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                renderer.TexturePath = frames[currentFrame];
            }
        }
    }
}
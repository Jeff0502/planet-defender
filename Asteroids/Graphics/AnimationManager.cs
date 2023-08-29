using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids.Graphics
{
    class AnimationManager
    {
        public Rectangle sourceRectangle = new();

        public Rectangle destinationRect = new();

        public bool clutterTexture = false;

        private Dictionary<string, Animation> animations;

        public Vector2 position;

        public Animation currentAnimation;

        private Texture2D texture;

        public float timer;

        public float rotation = 0;

        public Vector2 origin;

        public AnimationManager(Dictionary<string, Animation> Animations)
        {
            animations = new Dictionary<string, Animation>(Animations);

            Play(animations.First().Value);

            origin = new Vector2(position.X + currentAnimation.texture.Width / 2, position.Y + currentAnimation.texture.Height / 2);
        }

        public AnimationManager(Texture2D Texture)
        {
            texture = Texture;

            origin = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);

            sourceRectangle = new(0, 0, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            if(currentAnimation != null)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer >= currentAnimation.FrameSpeed && currentAnimation.FrameCount < currentAnimation.FrameAmount)
                {
                    currentAnimation.FrameCount++;

                    timer = 0;
                }

                if(currentAnimation.FrameCount >= currentAnimation.FrameAmount && currentAnimation.isLooped)
                {
                    currentAnimation.FrameCount = 0;
                }


            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(currentAnimation != null)
            {
                DrawAnimation(spriteBatch);
            }

            else
            {
                DrawTexture(spriteBatch);
            }
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation.texture, new Rectangle((int)position.X, (int)position.Y, currentAnimation.texture.Width / currentAnimation.FrameAmount, currentAnimation.FrameHeight), new Rectangle(currentAnimation.FrameCount * (currentAnimation.texture.Width / currentAnimation.FrameAmount), 0, currentAnimation.texture.Width / currentAnimation.FrameAmount, currentAnimation.FrameHeight), Color.White, -rotation, origin, SpriteEffects.None, 0);

        }

        public void DrawTexture(SpriteBatch spriteBatch)
        {
            if(!clutterTexture)
                destinationRect = new((int)position.X, (int)position.Y, texture.Width, texture.Height);

            spriteBatch.Draw(texture, destinationRect, sourceRectangle, Color.White, rotation, origin, SpriteEffects.None, 0);
        }

        public void Play(string AnimationName)
        {
            if (currentAnimation == animations[AnimationName])
                return;

            currentAnimation = animations[AnimationName];
            currentAnimation.FrameCount = 0;

            timer = 0;
        }

        private void Play(Animation animation)
        {
            if (currentAnimation == animation)
                return;

            currentAnimation = animation;

            timer = 0;
        }

        public bool IsFinished()
        {
            if (currentAnimation.FrameCount >= currentAnimation.FrameAmount)
                return true;

            else
                return false;
        }

        public void Stop()
        {

        }
    }
}

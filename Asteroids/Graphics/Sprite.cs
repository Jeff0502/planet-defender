using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Audio;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids.Graphics
{
    class Sprite
    {
        public AnimationManager animationManager;

        public AudioManager _audioManager;

        public Texture2D texture;

        public Rectangle DestinationRect
        {
            get
            {
                return animationManager.destinationRect;
            }

            set
            {
                animationManager.destinationRect = value;
            }
        }

        public Rectangle SourceRectangle
        {
            get
            {
                return animationManager.sourceRectangle;
            }

            set
            {
                animationManager.sourceRectangle = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return animationManager.origin;
            }

            set
            {
                animationManager.origin = value;
            }
        }

        public float Rotation
        {
            get
            {
                return animationManager.rotation;
            }

            set
            {
                animationManager.rotation = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return animationManager.position;
            }

            set
            {
                animationManager.position = value;
            }
        }

        public bool ClutterTexture
        {
            get
            {
                return animationManager.clutterTexture;
            }

            set
            {
                animationManager.clutterTexture = value;
            }
        }

        public Sprite(Texture2D Texture)
        {
            texture = Texture;

            animationManager = new AnimationManager(texture);
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            animationManager = new AnimationManager(animations);
        }

        public Sprite(Dictionary<string, Animation> animations, Dictionary<string, SoundEffect> soundEffects)
        {
            animationManager = new AnimationManager(animations);

            _audioManager = new AudioManager(soundEffects);
        }

        public Sprite()
        {
            
        }

        public virtual void Initialize()
        {
            animationManager = new(texture);

            Origin = new(0, 0);
        }

        public void PlayAudio(string SoundEffectName)
        {
            _audioManager.PlayAudio(SoundEffectName);
        }

        public void PlayAudio(SoundEffect soundEffect)
        {
            _audioManager.PlayAudio(soundEffect);
        }


        public virtual void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

    }

    class Collidable : Sprite
    {
        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int)Position.X - animationManager.currentAnimation.texture.Width / 2, (int)Position.Y - animationManager.currentAnimation.texture.Height / 2, animationManager.currentAnimation.texture.Width / animationManager.currentAnimation.FrameAmount, animationManager.currentAnimation.FrameHeight);

                return box;
            }
        }

        public Rectangle CollisionBox2
        {
            get
            {
                Rectangle box = new Rectangle((int)Position.X - SourceRectangle.Width / 2, (int)Position.Y - SourceRectangle.Height / 2, DestinationRect.Width, DestinationRect.Height);

                return box;
            }
        }

        public Collidable(Dictionary<string, Animation> animations) : base(animations)
        {
            
        }

        public Collidable(Texture2D texture) : base(texture)
        {
            
        }

        public Collidable(Dictionary<string, Animation> animations, Dictionary<string, SoundEffect> soundEffects) : base(animations, soundEffects)
        {
           
        }
    }
}

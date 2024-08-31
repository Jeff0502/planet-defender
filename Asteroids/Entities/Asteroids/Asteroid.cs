using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Asteroids.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Asteroids.Entities
{
    class Asteroid : Collidable, IGameEntity
    {
        public Vector2 direction;

        public float veloctiy;
        public Asteroid(Dictionary<string, Animation> animations, Dictionary<string, SoundEffect> soundEffects) : base(animations)
        { 
            veloctiy = 0.0025f;

            _audioManager = new(soundEffects);
            animationManager.rotation = this.Rotation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move();
        }

        public void Move()
        {
            Position += direction * veloctiy;
        }

        public void PlayExplosion()
        {
            animationManager.Play("Explosion");
            _audioManager.PlayAudio("Explosion");
        }

        public bool IsFinished()
        {
           return animationManager.IsFinished();
        }
    }
}

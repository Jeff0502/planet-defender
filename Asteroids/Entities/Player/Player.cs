using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Asteroids.Attributes;
using Asteroids.Graphics;
using Asteroids.Input;

namespace Asteroids.Entities
{
    class Player : Collidable, IGameEntity
    {
        private readonly KeyboardInput input = new();

        private float angularAccl = 0, angularVel = 0;

        private Vector2 direction;

        private PlayerHealthManager healthManager = new(2);

        private BulletManager bulletManager;

        public PlayerState state = PlayerState.isAlive;

        public List<Bullet> _bulletsFired
        {
            get
            {
                return bulletManager.bullets;
            }
        }

        public Player(Dictionary<string, Animation> animations, Dictionary<string, SoundEffect> soundEffects) : base(animations, soundEffects)
        {
            Rotation = MathHelper.ToRadians(180);
        }

        public void LoadContent(ContentManager Content)
        {
            bulletManager = new BulletManager(Content.Load<Texture2D>("Bullet"));

            healthManager.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            healthManager.Update(gameTime);

            if (healthManager.Dead == true)
            {
                state = PlayerState.isDead;

                return;
            }

            direction = new Vector2((float)Math.Sin(Rotation), (float)Math.Cos(Rotation));

            Rotation += angularVel;
            angularVel += angularAccl;
            angularVel *= 0.9f;
            angularAccl = 0;

            kbhit();

            base.Update(gameTime);
            
            if(input.IsKeyPressed(Keys.Space))
                Shoot();

            bulletManager.Update(gameTime);

            input.FixedUpdate(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthManager.Draw(spriteBatch);

            bulletManager.Draw(spriteBatch);

            var displacedPosition = new Vector2(AsteroidsGame.midpt.X + 150 * (float)Math.Sin(Rotation), AsteroidsGame.midpt.Y + 150 * (float)Math.Cos(Rotation));

            Position = displacedPosition;

            base.Draw(spriteBatch);
        }

        private void Shoot()
        {
            bulletManager.SpawnBullet(Position, -Rotation, direction);

            PlayAudio("ShootSFX");
        }

        private void kbhit()
        {
            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left))
                angularAccl += MathHelper.ToRadians(0.5f);

            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
                angularAccl -= MathHelper.ToRadians(0.5f);
            
        }

        public void RemoveBullet(Bullet bullet)
        {
            bulletManager.RemoveBullet(bullet);
        }

        public void Hit()
        {
            healthManager.TakeDamage();
        }
    }
}

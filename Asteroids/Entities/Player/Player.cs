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

        #region Updates
        public override void Update(GameTime gameTime)
        {
            healthManager.Update(gameTime);

            if (healthManager.Dead == true)
            {
                state = PlayerState.isDead;

                return;
            }

            direction = new Vector2((float)Math.Sin(Rotation), (float)Math.Cos(Rotation));

            kbhit();

            base.Update(gameTime);
            
            if(input.IsKeyPressed(Keys.Space))
            {
                Shoot();
            }

            bulletManager.Update(gameTime);

            input.FixedUpdate(gameTime);

        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthManager.Draw(spriteBatch);

            bulletManager.Draw(spriteBatch);

            Vector2 midpt = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

            var displacedPosition = new Vector2(midpt.X + 150 * (float)Math.Sin(Rotation), midpt.Y + 150 * (float)Math.Cos(Rotation));

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
                Rotation += MathHelper.ToRadians(3f);

            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
                Rotation -= MathHelper.ToRadians(3f);
            
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

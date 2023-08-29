using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Asteroids.Graphics;


namespace Asteroids.Entities
{
    class BulletManager
    {
        public List<Bullet> bullets = new List<Bullet>();

        public List<Bullet> bulletsToRemove = new List<Bullet>();

        private Texture2D bulletTexture;


        public BulletManager(Texture2D BulletTexture)
        {
            bulletTexture = BulletTexture;
        }

        public void Update(GameTime gameTime)
        {

            foreach(Bullet bullet in bullets)
            {
                bullet.Update(gameTime);

                CheckBulletLife(bullet);
            }

            foreach (Bullet bulletToRemove in bulletsToRemove)
            {
                bullets.Remove(bulletToRemove);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void SpawnBullet(Vector2 Position, float Rotation, Vector2 Direction)
        {
            Bullet bullet = new Bullet(bulletTexture, Direction)
            {
                Position = Position,

                Rotation = Rotation
            };

            AddBullet(bullet);
        }

        void CheckBulletLife(Bullet bulletToCheck)
        {
            if(bulletToCheck.lifeTime >= 900)
            {
                bulletsToRemove.Add(bulletToCheck);
            }
        }

        public void AddBullet(Bullet BulletToAdd)
        {
            bullets.Add(BulletToAdd);
        }

        public void RemoveBullet(Bullet BulletToRemove)
        {
            bulletsToRemove.Add(BulletToRemove);
        }

    }
}

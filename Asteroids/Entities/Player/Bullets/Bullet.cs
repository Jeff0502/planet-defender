using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Asteroids.Entities;
using Asteroids.Graphics;

namespace Asteroids
{
    class Bullet : Collidable, IGameEntity
    {
        private Vector2 direction;

        private float moveSpeed = 10f;

        public float lifeTime
        { get; private set; }

        public Bullet(Texture2D texture, Vector2 Direction) : base(texture)
        {
            direction = Direction;

            SourceRectangle = new(0, 0, 18, 36);
        }

        public override void Update(GameTime gameTime)
        {
            Position += direction * moveSpeed;

            base.Update(gameTime);

            lifeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.Graphics;

namespace Asteroids.Entities
{
    abstract class Powerup : Sprite
    {
        public Powerup(Texture2D Texture) : base(Texture)
        {
            Initialize();
        }

        public abstract void PowerUp();
    }
}

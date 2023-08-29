using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Asteroids.Graphics
{
    class Animation
    {
        public int FrameHeight;

        public int FrameAmount;

        public int FrameSpeed = 100;

        public int FrameCount = 0;

        public bool isLooped = true;

        public Texture2D texture;

        public Animation(Texture2D SpriteSheet)
        {
            texture = SpriteSheet;

            FrameHeight = texture.Height;

        }



    }
}

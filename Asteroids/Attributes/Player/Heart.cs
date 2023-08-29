using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Asteroids.Graphics;

namespace Asteroids
{
    enum HeartState 
    { 
        FULL,

        HALF,

        EMPTY
    }

    class Heart
    {
        public HeartState state = HeartState.FULL;

        public Vector2 position;

        private readonly Texture2D texture;

        private Rectangle destinationRect;

        public Heart(Texture2D Texture)
        {
            texture = Texture;
        }

        public void Update(GameTime gameTime)
        {
            CheckState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, destinationRect, Color.White);
        }

        public void CheckState()
        {
            switch(state)
            {
                case HeartState.FULL:
                    destinationRect = new Rectangle(0, 0, 48, 48);
                    break;

                case HeartState.HALF:
                    destinationRect = new Rectangle(48, 0, 48, 48);
                    break;

                case HeartState.EMPTY:
                    destinationRect = new Rectangle(0, 48, 48, 48);
                    break;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Entities
{
    class ScoreManager
    {
        public static int score = 0;

        private string strScore = "";

        private SpriteFont font = AsteroidsGame.spritefont;

        public void Update(GameTime gameTime)
        {
            CheckScore();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, strScore, new(1360, 0), AsteroidsGame.fontColor, 0, new(font.MeasureString(strScore).X, 0), 3, SpriteEffects.None, 0);
        }

        private void CheckScore()
        {
            strScore = score.ToString();
            
        }
    }
}

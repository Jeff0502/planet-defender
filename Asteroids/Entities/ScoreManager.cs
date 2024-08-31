using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Entities
{
    class ScoreManager
    {
        public static int score = 0;

        private int topScore = 0;

        private string strScore = "";

        private SpriteFont font = AsteroidsGame.spritefont;

        private string fileName = "score";

        private string path;

        public ScoreManager()
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            score = 0;

            if(File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;

                    if((s = sr.ReadLine()) != null)
                    {
                        topScore = Int32.Parse(s);
                    }
                }
            }

            else
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(0);
                }
            }
        }

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
            strScore = $"Score: {score}\nTop Score: {topScore}";            
        }

        public void WriteBest()
        {
            if(score > topScore)
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(score);
                }
            }
        }
    }
}

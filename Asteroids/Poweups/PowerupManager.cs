using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Entities
{
    class PowerUpManager
    {
        private List<Powerup> powerupsToRemove = new();

        public List<Powerup> powerups = new();

        private Dictionary<string, Texture2D> textures = new();

        public void LoadContent(ContentManager Content)
        {
            Texture2D speedUp = Content.Load<Texture2D>("");

            textures.Add("SpeedUp", speedUp);
        }

        public void Update(GameTime gameTime)
        {
            RemoveMarked();

            UpdateAvailable(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Powerup powerup in powerups)
            {
                powerup.Draw(spriteBatch);
            }
        }

        public void UpdateAvailable(GameTime gameTime)
        {
            //TODO: Update the current power-ups
            foreach(Powerup powerup in powerups)
            {
                powerup.Update(gameTime);
            }
        }

        public void RemoveMarked()
        {
            //TODO: Remove all powerups from list
            foreach (Powerup powerup in powerupsToRemove)
            {
                powerups.Remove(powerup);
            }
        }

        public void GeneratePowerUp()
        {
            if (ScoreManager.score > 200)
            {
                Random random = new();

                float genRot = 0;

                Vector2 genPos;

                genRot = random.Next(0, 360);

                Vector2 midpt = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

                var displacedPosition = new Vector2(midpt.X + 800 * (float)Math.Sin(genRot), midpt.Y + 800 * (float)Math.Cos(genRot));

                genPos = displacedPosition;

            }
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Attributes
{
    class PlanetHealthbar : HealthBar
    {
        public SpriteFont drawFont;

        public int renderHeight;

        public int renderWidth;

        public override void Initialize()
        {
            
            fillPosition.Y = Position.Y + 1;

            fillPosition.X = Position.X;

            renderWidth = BarFrame.Width / StartingHealth * Health;

            renderHeight = BarFrame.Height - 2;
            
        }

        public override void Update(GameTime gameTime)
        {
            renderWidth = BarFrame.Width / StartingHealth * Health;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BarFrame, Position, Color.White);
            spriteBatch.Draw(BarFill, new Rectangle((int)fillPosition.X, (int)fillPosition.Y, renderWidth, renderHeight), Color.White);

            DrawHealth(spriteBatch);
        }

        public void DrawHealth(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(drawFont, $"Planet health: {Health}", new Vector2(Position.X, Position.Y - 50), new Color(89, 193, 53, 255), 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager Content)
        {
            drawFont = AsteroidsGame.spritefont;
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Asteroids.Attributes
{
    abstract class HealthBar
    {

        public int Health { get; set; }

        public int StartingHealth { get; set; }
        
        public Texture2D BarFrame { get; set; }

        public Texture2D BarFill { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 fillPosition;

        public abstract void LoadContent(ContentManager Content);

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

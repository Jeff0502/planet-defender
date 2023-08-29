using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Attributes
{
    class HealthManager
    {
        public HealthBar healthBar;

        public HealthManager()
        {

        }

        public HealthManager(HealthBar HealthBar)
        {
            healthBar = HealthBar;
        }

        public int Health
        {
            get { return healthBar.Health; }

            set { healthBar.Health = value; }
        }

        public int StartingHealth
        {
            get { return healthBar.StartingHealth; }

            set { healthBar.StartingHealth = value; }
        }

        public Vector2 Position
        {
            get { return healthBar.Position; }

            set { healthBar.Position = value; }
        }

        public virtual void LoadContent(ContentManager Content)
        {
            healthBar.LoadContent(Content);

            Health = StartingHealth;

            healthBar.Initialize();
        }

        public virtual void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
        }

    }
}

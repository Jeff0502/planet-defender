using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Asteroids.Attributes
{
    class PlanetHealthManager : HealthManager
    {
        public PlanetHealthManager(HealthBar healthbar) : base(healthbar)
        {

        }

        public void LoadContent(ContentManager Content, string BarFrameName, string BarFillName)
        {
            Texture2D barFrame = Content.Load<Texture2D>(BarFrameName);

            Texture2D barFill = Content.Load<Texture2D>(BarFillName);

            healthBar.BarFill = barFill;

            healthBar.BarFrame = barFrame;

            base.LoadContent(Content);
        }
    }
}

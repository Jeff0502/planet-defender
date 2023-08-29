using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Menus
{
    class LoadMenu : Menu
    {
        private TimeSpan lastTimeUpdated;

        public event EventHandler OnLoadComplete;

        private readonly SpriteFont font = AsteroidsGame.spritefont;

        private Vector2 fontOrigin;

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("LoadMenu");

            fontOrigin = new(font.MeasureString("Loading...").X / 2, font.MeasureString("Loading...").Y / 2);

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            CheckDelay(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(AsteroidsGame.spritefont, "Loading...", new(texture.Width / 2, texture.Height / 2), AsteroidsGame.fontColor, 0, fontOrigin, 2f, SpriteEffects.None, 0);

        }

        private void CheckDelay(GameTime gameTime)
        {
            if(gameTime.TotalGameTime - lastTimeUpdated > new TimeSpan(0, 0, 0, 5))
            {
                OnLoadComplete(this, EventArgs.Empty);
                lastTimeUpdated = gameTime.TotalGameTime;
            }
        }
    }
}

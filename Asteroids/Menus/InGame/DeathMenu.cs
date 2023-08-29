using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.Menus
{
    class DeathMenu : Menu
    {
        public event EventHandler OnExit;

        private Input.KeyboardInput input = new();

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("DeathScreen");

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (input.IsKeyPressed(Keys.Enter))
                OnExit(this, EventArgs.Empty);

            base.Update(gameTime);
        }
    }
}

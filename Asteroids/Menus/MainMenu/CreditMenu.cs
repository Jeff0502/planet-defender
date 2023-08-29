using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Menus
{
    class CreditMenu : Menu
    {
        Input.KeyboardInput input = new();

        public event EventHandler OnExit;

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("CreditsScreen");

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            input.FixedUpdate(gameTime);

            if (input.IsKeyPressed(Keys.Escape))
                OnExit(this, EventArgs.Empty);

            base.Update(gameTime);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids.Graphics;
using Asteroids.Audio;
using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Asteroids.Entities;

namespace Asteroids.Menus
{
    class Menu : Sprite
    {
        public List<Button> buttons = new();

        public List<Button> buttonsToRemove = new();

        string textureName;

        public Menu(string TextureName)
        {
            textureName = TextureName;
        }

        public Menu()
        {

        }

        public virtual void LoadContent(ContentManager Content)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Button button in buttonsToRemove)
            {
                buttons.Remove(button);
            }

            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Asteroids.Graphics;
using System.Text;

namespace Asteroids.Menus
{
    class PauseMenu : Menu
    {
        private Input.KeyboardInput input = new();

        private Texture2D pauseMenuTextures;

        public event EventHandler Continue;

        public event EventHandler OnReturnToMain;

        public override void LoadContent(ContentManager Content)
        {
            pauseMenuTextures = Content.Load<Texture2D>("PauseMenuButtons");

            texture = Content.Load<Texture2D>("PauseMenu");

            base.LoadContent(Content);
        }

        private void QuitButton_OnReturnToMain(object sender, EventArgs e)
        {
            Clear();
            OnReturnToMain(this, EventArgs.Empty);
        }

        public override void Update(GameTime gameTime)
        {
            input.FixedUpdate(gameTime);

            base.Update(gameTime);

            if (input.IsKeyPressed(Keys.Escape))
                Continue(this, EventArgs.Empty);

        }

        void Clear()
        {
            buttonsToRemove.AddRange(buttons);
        }

        public override void Initialize()
        {
            input = new();

            QuitButton quitButton = new(pauseMenuTextures);

            buttons.Add(quitButton);

            quitButton.OnReturnToMain += QuitButton_OnReturnToMain;

            base.Initialize();
        }

        public void Reload()
        {
            Initialize();
        }
    }

    class QuitButton : Button
    {
        public event EventHandler OnReturnToMain;

        public QuitButton(Texture2D texture) : base(texture)
        {
            ClutterTexture = true;

            SourceRectangle = new(27, 19, 51, 17);

            DestinationRect = new(685, 390, 51, 17);

            Position = new(DestinationRect.X, DestinationRect.Y);

            Origin = new(SourceRectangle.Width / 2, (SourceRectangle.Height / 2));

        }

        public override void OnClick()
        {
            OnReturnToMain(this, EventArgs.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionBox2, SourceRectangle, Color.Red);

            base.Draw(spriteBatch);
        }
    }
}

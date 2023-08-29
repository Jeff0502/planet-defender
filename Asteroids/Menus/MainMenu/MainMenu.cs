using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.Menus
{
    class MainMenu : Menu
    {
        private Texture2D mainMenuButtonsTexture;

        public event EventHandler OnPlay;

        public event EventHandler OnCredits;

        public override void LoadContent(ContentManager Content)
        {
            CreditMenu creditMenu = new();

            creditMenu.LoadContent(Content);

            mainMenuButtonsTexture = Content.Load<Texture2D>("MainMenuButtons");

            texture = Content.Load<Texture2D>("MainMenu");

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Initialize()
        {
            Vector2 relativePostition = new(texture.Width / 2, texture.Height / 2);

            PlayButton playButton = new(mainMenuButtonsTexture, relativePostition);
            CreditButton creditButton = new(mainMenuButtonsTexture, relativePostition);

            buttons.Add(creditButton);
            buttons.Add(playButton);

            playButton.OnPlay += PlayButton_OnPlay;

            creditButton.OnPressed += CreditButton_OnPressed;

            base.Initialize();

        }

        private void CreditButton_OnPressed(object sender, EventArgs e)
        {
            OnCredits(this, EventArgs.Empty);
        }

        void Clear()
        {
            buttonsToRemove.AddRange(buttons);
        }

        public void Reload()
        {
            Initialize();
        }

        private void PlayButton_OnPlay(object sender, EventArgs e)
        {
            OnPlay(this, EventArgs.Empty);

            Clear();
        }
    }

    class PlayButton : Button
    {
        public event EventHandler OnPlay;

        public PlayButton(Texture2D texture, Vector2 RelativePos) : base(texture)
        {
            ClutterTexture = true;

            SourceRectangle = new(0, 0, 102, 24);

            DestinationRect = new((int)RelativePos.X, (int)RelativePos.Y, 102, 24);

            Origin = new(SourceRectangle.Width / 2, SourceRectangle.Height / 2);

            Position = new(DestinationRect.X, DestinationRect.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnClick()
        {
            OnPlay(this, EventArgs.Empty);
        }
    }

    class CreditButton : Button
    {
        public event EventHandler OnPressed;

        public CreditButton(Texture2D texture, Vector2 RelativePos) : base(texture)
        {
            ClutterTexture = true;

            SourceRectangle = new(0, 28, 168, 23);

            DestinationRect = new((int)RelativePos.X, (int)RelativePos.Y + 27, 168, 23);

            Origin = new(SourceRectangle.Width / 2, SourceRectangle.Height / 2);

            Position = new(DestinationRect.X, DestinationRect.Y);
        }

        public override void OnClick()
        {
            OnPressed(this, EventArgs.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionBox2, SourceRectangle, Color.Red);
            base.Draw(spriteBatch);
        }
    }
}

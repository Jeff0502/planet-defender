using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Asteroids.Entities;
using Asteroids.Audio;

namespace Asteroids.Menus
{
    class GameMenu : Menu
    {
        private EntityManager entityManager;

        public event EventHandler OnPause;

        public event EventHandler OnDeath;

        private Texture2D backgroundTexture;

        public MusicManager musicManager = new();

        public override void LoadContent(ContentManager Content)
        {
            string[] tracks = { "Crossing-the-Light-Years", "Dizzybot" };

            entityManager = new() { content = Content };

            musicManager.LoadTracks(tracks, Content);

            backgroundTexture = Content.Load<Texture2D>("Background");

            entityManager.LoadContent();

            entityManager.Initialize();

            entityManager.OnPlayerDeath += EntityManager_OnPlayerDeath;

            entityManager.OnPause += Pause;
        }

        private void EntityManager_OnPlayerDeath(object sender, EventArgs e)
        {
            musicManager.Stop();
            OnDeath(this, EventArgs.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            entityManager.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            musicManager.Update(gameTime);
            entityManager.Update(gameTime);
        }

        public void KillAll()
        {
            entityManager.KillAll();
        }

        public void Restart()
        {
            musicManager.Restart();
            entityManager.Restart();
        }


        public void Pause(object sender, EventArgs e)
        {
            OnPause(this, e);
            musicManager.Pause();
        }

    }
}

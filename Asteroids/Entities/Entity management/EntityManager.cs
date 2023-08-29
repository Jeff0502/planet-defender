using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Asteroids.Graphics;
using Asteroids.Menus;

namespace Asteroids.Entities
{
    class EntityManager : IGameEntity
    {
        public event EventHandler OnPlayerDeath;

        private Input.KeyboardInput input = new();

        public event EventHandler OnPause;

        private List<IGameEntity> entities = new();

        private Planet planet;

        private AsteroidManager _asteroidManager = new();

        public ContentManager content;

        ScoreManager scoreManager = new();

        public Player Player { get; private set; }

        public void LoadContent()
        {
            Dictionary<string, SoundEffect> playerSoundEffects = new()
            {
                { "ShootSFX", content.Load<SoundEffect>("ShootSFX") }
            };

            Dictionary<string, Animation> playerAnimations = new()
            {
                { "Idle", new Animation(content.Load<Texture2D>("Player")){ FrameAmount = 1 } }
            };

            Dictionary<string, Animation> planetAnimations = new()
            {
                { "Idle", new Animation(content.Load<Texture2D>("Planet")) { FrameAmount = 1, FrameSpeed = 100 } }
            };

            planet = new Planet(planetAnimations);

            planet.LoadContent(content);

            Player = new Player(playerAnimations, playerSoundEffects);

            Player.LoadContent(content);
            _asteroidManager.LoadContent(content);
        }

        public void Initialize()
        {
            entities.Add(_asteroidManager);
            entities.Add(Player);
            entities.Add(planet);
        }

        public void Update(GameTime gameTime)
        {
            scoreManager.Update(gameTime);

            if (Player.state == PlayerState.isDead)
                OnPlayerDeath(this, EventArgs.Empty);

            if (input.IsKeyPressed(Keys.Escape))
                OnPause(this, EventArgs.Empty);

            foreach (IGameEntity entity in entities)
            {
                    entity.Update(gameTime);
            }

                _asteroidManager.CheckCollision(planet.CollisionBox);

            foreach (Bullet bullet in Player._bulletsFired)
            {
                if (_asteroidManager.CheckCollision(bullet))
                {
                    Player.RemoveBullet(bullet);
                    ScoreManager.score += 13;

                }

            }

             if (_asteroidManager.CheckCollision(planet.CollisionBox))
                    planet.healthManager.Health -= 11;

             if (_asteroidManager.CheckCollision(Player.CollisionBox))
                    Player.Hit();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                foreach (IGameEntity entity in entities)
                {
                    entity.Draw(spriteBatch);
                }

            scoreManager.Draw(spriteBatch);
        }

        public void Clear()
        {
            entities.Clear();
            _asteroidManager.Clear();
        }


        public void Restart()
        {
            Clear();
            LoadContent();
            Initialize();
        }
    }

}

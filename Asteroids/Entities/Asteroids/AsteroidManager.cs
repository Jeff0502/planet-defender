using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Asteroids.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Asteroids.Entities
{
    class AsteroidManager : IGameEntity
    {
        public List<Asteroid> asteroids = new(3);

        public List<Asteroid> asteroidExplosions = new(3);

        public List<Asteroid> asteroidExplosionsToRemove = new(3);

        public List<Asteroid> asteroidsToRemove = new();

        private Dictionary<string, Animation> asteroidAnimations;

        private Dictionary<string, SoundEffect> asteroidSfx = new();

        private int SELECT_MOD = 1;

        public void LoadContent(ContentManager Content)
        {
            asteroidSfx.Clear();

            asteroidAnimations = new()
            {
                { "Idle", new Animation(Content.Load<Texture2D>("Asteroid")) { FrameAmount = 1, FrameSpeed = 100 } },

                { "Explosion", new Animation(Content.Load<Texture2D>("AsteroidExplosionTexture")) { FrameAmount = 6, FrameSpeed = 100, isLooped = false } }
            };

            SoundEffect asteroidExplosion = Content.Load<SoundEffect>("AsteroidExplosion");

            asteroidSfx.Add("Explosion", asteroidExplosion);
            
        }

        public void Update(GameTime gameTime)
        {
            foreach(Asteroid asteroid in asteroidsToRemove)
            {
                asteroids.Remove(asteroid);
            }

            foreach (Asteroid asteroid in asteroidExplosionsToRemove)
            {
                asteroidExplosions.Remove(asteroid);
            }

            foreach (Asteroid asteroid in asteroidExplosions)
            {
                if (asteroid.IsFinished())
                    asteroidExplosionsToRemove.Add(asteroid);

                asteroid.Update(gameTime);

            }

            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Update(gameTime);
            }

            GenerateAsteroid();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Draw(spriteBatch);
            }

            foreach (Asteroid asteroid in asteroidExplosions)
            {
                asteroid.Draw(spriteBatch);
            }
        }

        public void GenerateAsteroid()
        {
            if(asteroids.Count < 4)
            {
                Random random = new();

                float genRot = 0;

                Vector2 genPos;

                switch(SELECT_MOD)
                {
                    case 1:
                        genRot = MathHelper.ToRadians(random.Next(0, 80));
                        break;

                    case 2:
                        genRot = MathHelper.ToRadians(random.Next(90, 170));
                        break;

                    case 3:
                        genRot = MathHelper.ToRadians(random.Next(180, 260));
                        break;

                    case 4:
                        genRot = MathHelper.ToRadians(random.Next(270, 340));
                        break;

                    case 5:
                        SELECT_MOD = 1;
                        break;

                }

                SELECT_MOD++;

                Vector2 midpt = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

                var displacedPosition = new Vector2(midpt.X + 800 * (float)Math.Sin(genRot), midpt.Y + 800 * (float)Math.Cos(genRot));

                genPos = displacedPosition;


                Asteroid asteroid = new(asteroidAnimations, asteroidSfx)
                {
                    Rotation = MathHelper.ToRadians(random.Next(0, 360)),

                    Position = genPos,

                    direction = new Vector2(midpt.X - genPos.X, midpt.Y - genPos.Y),

                };

                asteroids.Add(asteroid);
            }
        }

        public bool CheckCollision(Rectangle rect)
        {
            foreach (Asteroid asteroid in asteroids)
            {
                if (rect.Intersects(asteroid.CollisionBox))
                {
                    PlayExplosion(asteroid);
                    RemoveAsteroid(asteroid);
                    return true;
                }
            }

            return false;
        }

        public bool CheckCollision(Bullet bullet)
        {
            if (CheckCollision(bullet.CollisionBox2))
                return true;

            return false;
        }

        public void PlayExplosion(Asteroid Asteroid)
        {
            Asteroid asteroid = new(asteroidAnimations, asteroidSfx);

            asteroid.Position = Asteroid.Position;

            asteroid.PlayExplosion();

            asteroidExplosions.Add(asteroid);
        }

        public void RemoveAsteroid(Asteroid asteroid)
        {
            asteroidsToRemove.Add(asteroid);
        }

        public void Clear()
        {
            asteroidAnimations.Clear();
            asteroidExplosions.Clear();
            asteroidExplosionsToRemove.Clear();
            asteroids.Clear();
        }
    }
}

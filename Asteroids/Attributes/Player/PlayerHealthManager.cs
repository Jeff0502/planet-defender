using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Attributes
{
    class PlayerHealthManager : HealthManager
    {
        private List<Heart> hearts = new();

        public bool Dead { get; private set; }

        private Texture2D heartTexture;

        private int heartToDec;

        public int heartAmount;

        public PlayerHealthManager(int HeartAmount)
        {
            heartAmount = HeartAmount;

            heartToDec = heartAmount - 1;
        }

        public void Initialize()
        {
            GenerateHearts();
        }

        public override void LoadContent(ContentManager Content)
        {
            heartTexture = Content.Load<Texture2D>("PlayerHealth");
            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Heart heart in hearts)
            {
                heart.Update(gameTime);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Heart heart in hearts)
            {
                heart.Draw(spriteBatch);
            }
        }

        public void GenerateHearts()
        {
            Vector2 genPos = Vector2.Zero;

            for(int i = 0; i < heartAmount; i++)
            {
                Heart heart;
                if (i == 0)
                {
                    heart = new(heartTexture) { position = genPos };
                }

                else
                {
                    genPos = new Vector2(hearts[i - 1].position.X + 56, 0);

                    heart = new(heartTexture) { position = genPos };
                }

                hearts.Add(heart);
            }
        }

        public void TakeDamage()
        {
            if (heartToDec != 0)
            {
                switch (hearts[heartToDec].state)
                {
                    case HeartState.FULL:
                        hearts[heartToDec].state = HeartState.HALF;
                        break;

                    case HeartState.HALF:
                        hearts[heartToDec].state = HeartState.EMPTY;
                        break;

                    case HeartState.EMPTY:
                        heartToDec--;
                        hearts[heartToDec].state = HeartState.HALF;
                        break;
                }
            }

            else
            {
                Dead = true;
            }
        }


    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using Asteroids.Attributes;
using System.Collections.Generic;
using Asteroids.Entities;

namespace Asteroids.Entities
{
    class Planet : Collidable, IGameEntity
    {
        public PlanetHealthManager healthManager = new PlanetHealthManager(new PlanetHealthbar()) { StartingHealth = 683, Position = new Vector2(0, 746) };

        public Planet(Dictionary<string, Animation> animations) : base(animations)
        {
            Vector2 midpt = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

            Position = midpt;

        }

        public void LoadContent(ContentManager Content)
        {
            healthManager.LoadContent(Content, "PlanetHealthbar", "PlanetHealthFill");
        }

        public override void Update(GameTime gameTime)
        {
            healthManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            healthManager.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

    }
}

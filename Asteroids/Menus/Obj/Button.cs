using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Asteroids.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Menus
{
    abstract class Button : Collidable
    {
        public Button(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public Button(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (CheckClick())
                OnClick();
        }

        public bool CheckClick()
        {
            Rectangle mouseRectangle = new(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);

            return (CollisionBox2.Intersects(mouseRectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public abstract void OnClick();

    }
}

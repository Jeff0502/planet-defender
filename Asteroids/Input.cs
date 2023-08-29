using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Asteroids.Input
{
    class KeyboardInput
    {
        Keys keyToCheck;

        TimeSpan lastTimeUpdated;

        bool oldKeyState = false;

        bool newKeyState = true;

        public void FixedUpdate(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - lastTimeUpdated > new TimeSpan(0, 0, 0, 0, 300))
            {
                lastTimeUpdated = gameTime.TotalGameTime;
                oldKeyState = newKeyState;
            }
        }

        public  bool IsKeyDown(Keys Key)
        {
            keyToCheck = Key;

            return Keyboard.GetState().IsKeyDown(keyToCheck);
        }

        public bool IsKeyPressed(Keys Key)
        {
            keyToCheck = Key;

            bool wasKeyPressed = false;

            newKeyState = IsKeyDown(keyToCheck);

            if (!oldKeyState && newKeyState)
            {
                wasKeyPressed = true;
            }

            oldKeyState = newKeyState;

            return wasKeyPressed;
        }

        public void ResetKeys()
        {
            oldKeyState = true;
            newKeyState = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Menus
{
    class MenuManager
    {
        private Dictionary<string, Menu> menus = new();

        private Menu currentMenu;

        private GameState currentGameState;

        private event EventHandler OnStateChange;

        private GameState delayedState;

        public void LoadContent(ContentManager Content)
        {
            GameMenu gameMenu = new();

            PauseMenu pauseMenu = new();

            MainMenu mainMenu = new();

            LoadMenu loadMenu = new();

            CreditMenu creditMenu = new();

            DeathMenu deathMenu = new();

            deathMenu.LoadContent(Content);

            creditMenu.LoadContent(Content);

            loadMenu.LoadContent(Content);

            mainMenu.LoadContent(Content);

            pauseMenu.LoadContent(Content);

            gameMenu.LoadContent(Content);

            menus.Add("GameMenu", gameMenu);

            menus.Add("PauseMenu", pauseMenu);

            menus.Add("MainMenu", mainMenu);

            menus.Add("LoadMenu", loadMenu);

            menus.Add("CreditMenu", creditMenu);

            menus.Add("DeathMenu", deathMenu);

            currentGameState = GameState.inMain;

            delayedState = currentGameState;

            OnStateChange += MenuManager_OnStateChange;

            deathMenu.OnExit += DeathMenu_OnExit;

            mainMenu.OnCredits += MainMenu_OnCredits;

            loadMenu.OnLoadComplete += LoadMenu_OnLoadComplete;

            mainMenu.OnPlay += MainMenu_OnPlay;

            pauseMenu.Continue += PauseMenu_Continue;

            creditMenu.OnExit += CreditMenu_OnExit;

            gameMenu.OnPause += GameMenu_OnPause;

            pauseMenu.OnReturnToMain += PauseMenu_OnQuit;

            gameMenu.OnDeath += GameMenu_OnDeath;

            CheckMenu();
        }

        public void Update(GameTime gameTime)
        {
            CheckMenu();
            currentMenu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentMenu.Draw(spriteBatch);
        }

        #region MenuChanges
        private void CheckMenu()
        {
            //TODO: Change the Menu according to current game-state
            CheckState();

            switch (currentGameState)
            {
                case GameState.inMain:
                    currentMenu = menus["MainMenu"];
                    break;

                case GameState.inGame:
                    currentMenu = menus["GameMenu"];
                    break;

                case GameState.inCredits:
                    currentMenu = menus["CreditMenu"];
                    break;

                case GameState.Loading:
                    currentMenu = menus["LoadMenu"];
                    break;

                case GameState.Paused:
                    currentMenu = menus["PauseMenu"];
                    break;

                case GameState.inDeathScreen:
                    currentMenu = menus["DeathMenu"];
                    break;
            }
        }

        private void CheckState()
        {
            //TODO: Check if the loading screen should be called
            if(delayedState != currentGameState)
            {
                OnStateChange(this, EventArgs.Empty);
            }
        }

        #endregion

        #region MenuEventHandlers
        private void GameMenu_OnDeath(object sender, EventArgs e)
        {
            delayedState = GameState.inDeathScreen;
            GameMenu gameMenu = (GameMenu)menus["GameMenu"];

            gameMenu.KillAll();
        }

        private void DeathMenu_OnExit(object sender, EventArgs e)
        {
            GameMenu gameMenu = (GameMenu)menus["GameMenu"];

            gameMenu.Restart();

            PauseMenu pauseMenu = (PauseMenu)menus["PauseMenu"];

            pauseMenu.Reload();

            delayedState = GameState.inGame;
        }

        private void CreditMenu_OnExit(object sender, EventArgs e)
        {
            delayedState = GameState.inMain;
        }

        private void MainMenu_OnCredits(object sender, EventArgs e)
        {
            delayedState = GameState.inCredits;
        }

        private void LoadMenu_OnLoadComplete(object sender, EventArgs e)
        {
            currentGameState = delayedState;
        }

        private void MenuManager_OnStateChange(object sender, EventArgs e)
        {
            currentGameState = GameState.Loading;
        }

        public void PauseMenu_OnQuit(object sender, EventArgs e)
        {
            MainMenu mainMenu = (MainMenu)menus["MainMenu"];

            GameMenu gameMenu = (GameMenu)menus["GameMenu"];

            gameMenu.musicManager.Stop();

            mainMenu.Reload();

            delayedState = GameState.inMain;
        }

        private void MainMenu_OnPlay(object sender, EventArgs e)
        {
            GameMenu gameMenu = (GameMenu)menus["GameMenu"];

            gameMenu.Restart();

            PauseMenu pauseMenu = (PauseMenu)menus["PauseMenu"];

            pauseMenu.Reload();

            delayedState = GameState.inGame;
        }

        private void GameMenu_OnPause(object sender, EventArgs e)
        {
            currentGameState = GameState.Paused;
            delayedState = currentGameState;
        }

        private void PauseMenu_Continue(object sender, EventArgs e)
        {
            currentGameState = GameState.inGame;
            delayedState = currentGameState;
        }
        #endregion
    }
}

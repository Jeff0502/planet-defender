using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.Menus;
using System.Reflection.Metadata;

namespace Asteroids
{
    public class AsteroidsGame : Game
    {
        public static int RENDER_WIDTH = 1366, RENDER_HEIGHT = 768;

        public static Vector2 midpt = new Vector2(RENDER_WIDTH / 2, RENDER_HEIGHT / 2);

        public static int WINDOW_WIDTH, WINDOW_HEIGHT;

        private RenderTarget2D renderTarget2D;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Color fontColor = new Color(89, 193, 53, 255);
        public static SpriteFont spritefont;

        private MenuManager menuManager = new();

        public AsteroidsGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
           
            Content.RootDirectory = "Content//";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WINDOW_WIDTH = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            WINDOW_HEIGHT = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spritefont = Content.Load<SpriteFont>("UI font");

            menuManager.LoadContent(Content);
            renderTarget2D = new RenderTarget2D(GraphicsDevice, RENDER_WIDTH, RENDER_HEIGHT, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            menuManager.Update(gameTime);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(renderTarget2D);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            menuManager.Draw(_spriteBatch);

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(renderTarget2D, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}

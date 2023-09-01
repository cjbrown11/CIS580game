using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LilManGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private LilManSprite lilMan;
        private YellowBirdSprite[] birds;
        private SpriteFont rubikiso;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            lilMan = new LilManSprite();
            birds = new YellowBirdSprite[]
            {
                new YellowBirdSprite(){Position = new Vector2(100, 100), Direction = Direction.Down},
                new YellowBirdSprite(){Position = new Vector2(400, 400), Direction = Direction.Up},
                new YellowBirdSprite(){Position = new Vector2(200, 500), Direction = Direction.Left},
                new YellowBirdSprite(){Position = new Vector2(200, 500), Direction = Direction.Right}
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            lilMan.LoadContent(Content);
            foreach (var bird in birds) bird.LoadContent(Content);
            rubikiso = Content.Load<SpriteFont>("rubikiso");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                Exit();

            // TODO: Add your update logic here
            lilMan.Update(gameTime);
            foreach (var bird in birds) bird.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            lilMan.Draw(gameTime, spriteBatch);
            foreach (var bird in birds) bird.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(rubikiso, "Press SPACE to Exit The Game", new Vector2(30, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
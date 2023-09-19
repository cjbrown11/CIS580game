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
        private CoinSprite[] coins;
        private SpriteFont skranji;

        private int coinsCollected = 0;
        private int birdsCollected = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            System.Random random = new System.Random();
            lilMan = new LilManSprite();
            birds = new YellowBirdSprite[]
            {
                new YellowBirdSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new YellowBirdSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new YellowBirdSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new YellowBirdSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height))
            };
            coins = new CoinSprite[]
            {
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)random.NextDouble() * GraphicsDevice.Viewport.Width, (float)random.NextDouble() * GraphicsDevice.Viewport.Height))
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            lilMan.LoadContent(Content);
            foreach (var bird in birds) bird.LoadContent(Content);
            foreach (var coin in coins) coin.LoadContent(Content);
            rubikiso = Content.Load<SpriteFont>("rubikiso");
            skranji = Content.Load<SpriteFont>("skranji");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                Exit();

            // TODO: Add your update logic here
            lilMan.Update(gameTime);
            foreach (var bird in birds) bird.Update(gameTime);
            foreach (var bird in birds)
            {
                if (!bird.Collected && bird.Bounds.CollidesWith(lilMan.Bounds))
                {
                    bird.Collected = true;
                    birdsCollected++;
                }
            }
            foreach (var coin in coins)
            {
                if (!coin.Collected && coin.Bounds.CollidesWith(lilMan.Bounds))
                {
                    coin.Collected = true;
                    coinsCollected++;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            lilMan.Draw(gameTime, spriteBatch);
            foreach (var bird in birds) bird.Draw(gameTime, spriteBatch);
            foreach (var coin in coins) coin.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(rubikiso, "Press SPACE to Exit The Game", new Vector2(30, 10), Color.White);
            spriteBatch.DrawString(skranji, $"Coins Collected: {coinsCollected}", new Vector2(10, 100), Color.Gold);
            spriteBatch.DrawString(skranji, $"Birds Collected: {birdsCollected}", new Vector2(10, 120), Color.Gold);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
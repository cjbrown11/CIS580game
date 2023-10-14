using LilManGame.Screens;
using LilManGame.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System;

namespace LilManGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private readonly ScreenManager _screenManager;

        private LilManSprite lilMan;
        private YellowBirdSprite[] birds;
        private SpriteFont rubikiso;
        private CoinSprite[] coins;
        private SpriteFont skranji;
        private Texture2D background;
        private SoundEffect birdKillSound;
        private Song backgroundMusic;
        BirdkillParticleSystem _birdKill;
        bool shakeScreen = false;
        Vector2 shakeOffset = new Vector2(15, 0);
        int shakeCount = 0;
        int maxShakes = 1;

        private int birdsCollected = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            var menuBackground = new BackgroundScreen();
            var mainMenuScreen = new MainMenuScreen(menuBackground);
            _screenManager.AddScreen(menuBackground, null);
            _screenManager.AddScreen(mainMenuScreen, null);
        }

        protected override void Initialize()
        {
            System.Random random = new System.Random();
            lilMan = new LilManSprite();
            birds = new YellowBirdSprite[]
            {
                new YellowBirdSprite(new Vector2(graphics.PreferredBackBufferWidth * 0.4f, 0), Direction.Down),
                new YellowBirdSprite(new Vector2(graphics.PreferredBackBufferWidth * 0.8f, 200), Direction.Left),
                new YellowBirdSprite(new Vector2(graphics.PreferredBackBufferWidth * 0.2f, 200), Direction.Right),
                new YellowBirdSprite(new Vector2(graphics.PreferredBackBufferWidth * 0.6f, 200), Direction.Up)
            };

            _birdKill = new BirdkillParticleSystem(this, 20);
            Components.Add(_birdKill);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            lilMan.LoadContent(Content);
            foreach (var bird in birds) bird.LoadContent(Content);
            rubikiso = Content.Load<SpriteFont>("rubikiso");
            skranji = Content.Load<SpriteFont>("skranji");
            background = Content.Load<Texture2D>("background");
            // Sound from Zapsplat.com
            birdKillSound = Content.Load<SoundEffect>("birdkill");
            // Pixelland by Kevin MacLeod | https://incompetech.com/
            // Music promoted by https://www.chosic.com/free-music/all/
            // Creative Commons CC BY 3.0
            // https://creativecommons.org/licenses/by/3.0/
            backgroundMusic = Content.Load<Song>("backgroundmusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {            
            lilMan.Update(gameTime);
            foreach (var bird in birds) bird.Update(gameTime);
            foreach (var bird in birds)
            {
                if (!bird.Collected && bird.Bounds.CollidesWith(lilMan.Bounds))
                {
                    bird.Collected = true;
                    birdsCollected++;
                    birdKillSound.Play();
                    shakeScreen = true;
                    _birdKill.PlaceFirework(bird.position);
                    _birdKill.PlaceFirework(bird.position);
                    _birdKill.PlaceFirework(bird.position);
                    _birdKill.PlaceFirework(bird.position);
                    _birdKill.PlaceFirework(bird.position);
                }
            }

            if (shakeScreen && shakeCount < maxShakes)
            {
                shakeOffset *= -1;
                shakeCount++;
            }
            else
            {
                shakeCount = 0;
                shakeScreen = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (shakeScreen)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0));
            }       
            else
            {
                spriteBatch.Begin();
            }
            spriteBatch.Draw(background,  new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            lilMan.Draw(gameTime, spriteBatch);
            foreach (var bird in birds) bird.Draw(gameTime, spriteBatch);
            if (birdsCollected == 4)
            {
                spriteBatch.DrawString(skranji, $"You Win!", new Vector2(250, 50), Color.Gold, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0); 
            }
                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
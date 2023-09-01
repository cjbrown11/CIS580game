using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;

namespace LilManGame
{
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    /// <summary>
    /// A class representing a yellow bird sprite
    /// </summary>
    public class YellowBirdSprite
    {
        private Texture2D birdUp;

        private Texture2D birdMid;

        private Texture2D birdDown;

        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 0;

        private Texture2D[] birdFrame;

        private bool flipped;

        /// <summary>
        /// The direction of the bird
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// The position of the bird
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the bird sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            birdFrame = new Texture2D[]
            {
                birdUp = content.Load<Texture2D>("yellowbirdup"),
                birdMid = content.Load<Texture2D>("yellowbirdmid"),
                birdDown = content.Load<Texture2D>("yellowbirddown")
            };          
        }

        /// <summary>
        /// Updates the bat to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (directionTimer > 2.0)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Right;
                        flipped = false;
                        break;
                    case Direction.Right:
                        Direction = Direction.Left;
                        flipped = true;
                        break;
                    case Direction.Left:
                        Direction = Direction.Up;
                        break;
                }
                directionTimer -= 2.0;
            }

            //Move the bat
            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }

        /// <summary>
        /// Draws the animated sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Update animation fram
            if (animationTimer > 0.2)
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= 0.2;
            }

            // Draw the sprite
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(birdFrame[animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 1.5f, spriteEffects, 0);
        }

    }
}

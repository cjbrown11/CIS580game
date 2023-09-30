using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LilManGame.StateManagement
{
    public abstract class GameScreen
    {
        public bool IsPopup { get; protected set; }

        protected TimeSpan TransitionOnTime { get; set; } = TimeSpan.Zero;

        protected TimeSpan TransitionOffTime { get; set; } = TimeSpan.Zero;

        protected float TransitionPosition { get; set; } = 1;

        public float TransitionAlpha => 1f - TransitionPosition;

        public ScreenState ScreenState { get; set; } = ScreenState.TransitionOn;

        public bool IsExiting { get; protected internal set; }

        public bool IsActive => !_otherScreenHasFocus && (
            ScreenState == ScreenState.TransitionOn ||
            ScreenState == ScreenState.Active);

        private bool _otherScreenHasFocus;

        public ScreenManager ScreenManager { get; internal set; }

        public PlayerIndex? ControllingPlayer { protected get; set; }

        public virtual void Activate() { }

        public virtual void Deactivate() { }

        public virtual void Unload() { }

        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _otherScreenHasFocus = otherScreenHasFocus;

            if (IsExiting)
            {
                // If the screen is going away forever, it should transition off
                ScreenState = ScreenState.TransitionOff;

                if (!UpdateTransitionPosition(gameTime, TransitionOffTime, 1))
                    ScreenManager.RemoveScreen(this);
            }
            else if (coveredByOtherScreen)
            {
                // if the screen is covered by another, it should transition off
                ScreenState = UpdateTransitionPosition(gameTime, TransitionOffTime, 1)
                    ? ScreenState.TransitionOff
                    : ScreenState.Hidden;
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                ScreenState = UpdateTransitionPosition(gameTime, TransitionOnTime, -1)
                    ? ScreenState.TransitionOn
                    : ScreenState.Active;
            }
        }

        private bool UpdateTransitionPosition(GameTime gameTime, TimeSpan time, int direction)
        {
            // How much should we move by?
            float transitionDelta = (time == TimeSpan.Zero)
                ? 1
                : (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            // Update the transition time
            TransitionPosition += transitionDelta * direction;

            // Did we reach the end of the transition?
            if (direction < 0 && TransitionPosition <= 0 || direction > 0 && TransitionPosition >= 0)
            {
                TransitionPosition = MathHelper.Clamp(TransitionPosition, 0, 1);
                return false;
            }

            return true;
        }

            public virtual void HandleInput(GameTime gameTime, InputState input) { }

            public virtual void Draw(GameTime gameTime) { }

        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
                ScreenManager.RemoveScreen(this);    // If the screen has a zero transition time, remove it immediately
            else
                IsExiting = true;    // Otherwise flag that it should transition off and then exit.
        }

    }
}

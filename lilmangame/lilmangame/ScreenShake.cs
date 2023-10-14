using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LilManGame
{
    public class ScreenShake
    {
        float xOffset = 0;
        float yOffset = 0;
        int shakeTimer = 0;

        public Vector2 actual;
        public Vector2 logical;

        //Shaking will take 200ms
        public float SHAKE_TIME_MS = 200;

        //The screen will be moved 20 pixels forth and back
        public float SHAKE_OFFSET = 20;

        //Indicates the shake move direction
        public bool shakeDirection = false;

        //Indicates if the screen is currently shaking
        public bool shaking;

        //delta is the ms passed since last update
        public void Update(int delta)
        {
            if (shaking)
            {
                UpdateShake(delta);
            }
            else
            {
                actual.X = logical.X;
                actual.Y = logical.Y;
            }
        }

        private void UpdateShake(int delta)
        {
            if (shakeTimer == 0)
            {
                //If its the first run from the current shake, initialise x and y offset to 0
                xOffset = 0;
                yOffset = 0;
            }

            //Add passed milliseconds to timer... If timer exceeds configuration, shaking ends
            shakeTimer += delta;
            if (shakeTimer > SHAKE_TIME_MS)
            {
                //Shaking ends 
                shakeTimer = 0;
                shaking = false;
                xOffset = 0;
                yOffset = 0;
            }
            else
            {
                ApplyScreenShake(delta);
            }
        }

        private void ApplyScreenShake(int delta)
        {
            //Depending on shake direction, the screen is moved either to the top left, or the bottom right
            if (shakeDirection)
            {
                xOffset -= 1.5f * delta;
                if (xOffset < -SHAKE_OFFSET)
                {
                    //SWITCH DIRECTION
                    xOffset = -SHAKE_OFFSET;
                    shakeDirection = !shakeDirection;
                }
                yOffset = xOffset;
            }
            else
            {
                xOffset += 1.5f * delta;
                if (xOffset > SHAKE_OFFSET)
                {
                    //SWITCH DIRECTION
                    xOffset = SHAKE_OFFSET;
                    shakeDirection = !shakeDirection;
                }
                yOffset = xOffset;
            }
            actual.X = logical.X + xOffset;
            actual.Y = logical.Y + yOffset;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilManGame
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects a collison between two bounding circle
        /// </summary>
        /// <param name="a">circle a</param>
        /// <param name="b">circle b</param>
        /// <returns>True when circles collide</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.Center.X - b.Center.X, 2) +
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        /// <summary>
        /// Detects a collison between two bounding rectangles
        /// </summary>
        /// <param name="a">rectangle a</param>
        /// <param name="b">rectangle b</param>
        /// <returns>True if collision</returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right ||
                     a.Top > b.Bottom || a.Bottom < b.Top);
        }

        /// <summary>
        /// Detects a collison between a rectangle and a circle
        /// </summary>
        /// <param name="c">circle</param>
        /// <param name="r">rectangle</param>
        /// <returns>True if collision</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) +
                Math.Pow(c.Center.Y - nearestY, 2);
        }

        public static bool Collides(BoundingRectangle r, BoundingCircle c) => Collides(c, r);
    }
}

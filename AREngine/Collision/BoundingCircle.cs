using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Collision
{
    /// <summary>
    /// 碰撞圆
    /// </summary>
    public struct BoundingCircle
    {
        Vector2 origin;
        /// <summary>
        /// 圆心
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        float radius;
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                radiussquared = radius * radius;
            }
        }

        private float radiussquared;

        public BoundingCircle(Vector2 origin, float Radius)
        {
            this.origin = origin;
            this.radius = Radius;
            radiussquared = radius * radius;
        }
        /// <summary>
        /// 圆和点的碰撞检测
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Intersects(Vector2 point)
        {
            if (Vector2.DistanceSquared(origin, point) <= radiussquared)
            {
                return true;
            }
            return false;
        }
    }
}

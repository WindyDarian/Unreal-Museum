using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AREngine.Helpers
{
    /// <summary>
    /// 矩阵运算助手，大地无敌2011/8/16
    /// </summary>
    public static class MatrixHelper
    {
        /// <summary>
        /// 将旋转矩阵转换回Yaw,Pitch,Roll，参考文献http://planning.cs.uiuc.edu/node103.html
        /// </summary>
        /// <param name="rotation">旋转矩阵</param>
        /// <returns>X是Yaw，Y是Pitch，Z是Roll</returns>
        public static Vector3 MatrixToYawPitchRoll(Matrix rotation)
        {
            float yaw = 0;
            float pitch = 0;
            float roll = 0;

            //求yaw
            if (rotation.M11 != 0)
            {

                roll = -(float)Math.Atan2(rotation.M21 , rotation.M11);
            }
            else
            {
                roll = -Math.Sign(rotation.M21) * MathHelper.PiOver2;
            }

            //求Pitch
            if (rotation.M32 != 0 || rotation.M33 != 0)
            {
                double x = Math.Sqrt(Math.Pow(rotation.M32, 2) + Math.Pow(rotation.M33, 2));
                yaw = -(float)Math.Atan2(-rotation.M31, x);
            }
            else
            {
                yaw = -Math.Sign(rotation.M31) * MathHelper.PiOver2 * (-1);
            }

            //求roll
            if (rotation.M33 != 0)
            {
                pitch = -(float)Math.Atan2(rotation.M32, rotation.M33);
            }
            else
            {
                pitch = -Math.Sign(rotation.M32) * MathHelper.PiOver2;
            }

            return new Vector3(yaw, pitch, roll);
        }
    }
}

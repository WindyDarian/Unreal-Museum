using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Illusion
{
    /// <summary>
    /// 大地无敌-范若余
    /// 2011/12/20
    /// </summary>
    public static class MatrixHelper
    {
        /// <summary>
        /// 乘在View和Projection之间，实现斜投影
        /// </summary>
        /// <param name="direction">必须是单位向量</param>
        /// <returns></returns>
        public static Matrix CreateIlluMatrix(Vector3 direction)
        {
            Matrix m = Matrix.Identity;
            float cosa = Vector3.Dot(Vector3.Forward, direction);
            if (cosa!=0)
            {
                m.M31 = direction.X / cosa;
                m.M32 = direction.Y / cosa;
            }
            return m;
        }
        /// <summary>
        /// 创建XNA投影矩阵
        /// </summary>
        /// <param name="direction">方向，必须是单位向量</param>
        /// <returns></returns>
        public static Matrix CreateXNAProjection(Vector3 direction,float screendepth,float screenwidth,float aspectRatio,float nearPlaneDistance,float farPlaneDistance)
        {
            float cosa = Vector3.Dot(Vector3.Forward, direction);
            Vector3 screenCenter = direction * screendepth / cosa;
            float a = nearPlaneDistance / screendepth;
            float left = (screenCenter.X - screenwidth / 2) * a;
            float right = (screenCenter.X + screenwidth / 2) * a;
            float screenHeight = screenwidth / aspectRatio;
            float top = (screenCenter.Y + screenHeight / 2) * a;
            float bottom = (screenCenter.Y - screenHeight / 2) * a;
            return Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);

        }
    }
}

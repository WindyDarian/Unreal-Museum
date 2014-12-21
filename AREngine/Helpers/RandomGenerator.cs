using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Helpers
{
    /// <summary>
    /// 随机数
    /// 范若余 2011/12/8从AOD中导入
    /// </summary>
    public static class RandomGenerator
    {
        private static Random random = new Random();
        /// <summary>
        /// 随机数生成器
        /// </summary>
        public static Random Random
        {
            get { return random; }
        }

        public static float RandomNext(float min, float max)
        {
            return (min + (float)((max - min) * Random.NextDouble()));
        }
        /// <summary>
        /// 得到球状区域内的随机一点
        /// </summary>
        /// <param name="center">球心</param>
        /// <param name="radius">半径</param>
        /// <returns>结果</returns>
        public static Vector3 RandomPointInBall(Vector3 center, float radius)
        {
            Vector3 a;
            do
            {
                a = new Vector3(RandomNext(-radius, radius), RandomNext(-radius, radius), RandomNext(-radius, radius));

            } while (Vector3.Distance(a, Vector3.Zero) > radius);//直到随机出的点在球内..
            return a + center;

        }
        public static Vector3 RandomDirection()
        {
            return Vector3.Normalize(Vector3.TransformNormal(Vector3.Forward, Matrix.CreateRotationX(RandomNext(0, MathHelper.TwoPi)) * Matrix.CreateRotationY(RandomNext(0, MathHelper.TwoPi))));
        }
        public static Vector2 RandomDirection2()
        {
            return Vector2.Normalize(Vector2.TransformNormal(Vector2.UnitX, Matrix.CreateRotationZ(RandomNext(0, MathHelper.TwoPi))));
        }
        //public static Vector3 RandomPointInRegion(Vector3 center, float radius)
        //{
        //    return center + RandomDirection() * RandomNext(0, radius);
        //}
        /// <summary>
        /// 判断两个点是否在距离内
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool WithinRange(Vector3 v1, Vector3 v2, float f)
        {
            return Vector3.DistanceSquared(v1, v2) <= f * f;
        }
        /// <summary>
        /// 把一个XYZ分别代表绕三个轴旋转角度的向量转化为矩阵
        /// </summary>
        /// <returns></returns>
        public static Matrix RotationVector3ToMatrix(Vector3 v)
        {
            Matrix result = Matrix.CreateRotationX(MathHelper.ToRadians(v.X));
            result = Matrix.CreateRotationY(MathHelper.ToRadians(v.Y)) * result;
            result = Matrix.CreateRotationZ(MathHelper.ToRadians(v.Z)) * result;
            return result;
        }
        /// <summary>
        /// 返回一个随机整数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            return (int)RandomNext(min, max + 1);
        }

        /// <summary>
        /// 得到一个在给定的角内向前的向量
        /// </summary>
        /// <param name="maxAngle">最大角（角度）</param>
        /// <returns></returns>
        public static Vector3 RandomUnitVector3WithinAngle(float maxAngle)
        {
            float angel = ((float)random.NextDouble()) * maxAngle;
            float rZ = ((float)random.NextDouble()) * MathHelper.Pi;

            return Vector3.TransformNormal(Vector3.Forward
                , Matrix.CreateRotationY(MathHelper.ToRadians(angel)) * Matrix.CreateRotationZ(rZ));
        }
    }
}

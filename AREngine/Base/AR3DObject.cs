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

namespace AREngine.Base
{
    /// <summary>
    /// 一般的3D对象
    /// </summary>
    public abstract class AR3DObject:ARObject,IAR3D
    {
        Vector3 position = Vector3.Zero;
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// 上方向量
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return Vector3.TransformNormal(Vector3.Up, Rotation);
            }
        }


        /// <summary>
        /// 面向
        /// </summary>
        public Vector3 Face
        {
            get
            {
                return Vector3.TransformNormal(Vector3.Forward,Rotation);
            }
            set
            {
                SetFace(value);

            }
        }



        //Vector3 velocity = Vector3.Zero;
        //public Vector3 Velocity
        //{
        //    get
        //    {
        //        return velocity;
        //    }
        //    set
        //    {
        //        velocity = value;
        //    }
        //}

        Matrix rotation = Matrix.Identity;
        /// <summary>
        /// 旋转矩阵
        /// </summary>
        public Matrix Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        Vector3 scale = new Vector3(1,1,1);
        /// <summary>
        /// 三轴缩放
        /// </summary>
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Matrix World
        {
            get
            {
                return Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(Position) ;
            }
        }



        /// <summary>
        /// 使旋转矩阵正常，避免误差累积，只对存在时间长且重要的对象调用
        /// </summary>
        public virtual void RenewRotation()
        {

        }

        /// <summary>
        /// 设置面向的方法
        /// </summary>
        /// <param name="val"></param>
        void SetFace(Vector3 val)
        {
            if (val!= Vector3.Zero)
            {
                Vector3 face = Vector3.Normalize(val);
                if (face != Vector3.Forward)
                {
                    rotation = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(face, Vector3.Forward)), -(float)Math.Acos(Vector3.Dot(Vector3.Forward, face)));

                }
                else rotation = Matrix.Identity;
            }
            else
            {
                ARCommandPad.AddErrorMessage("设置AR3DObject面向中给了一个零向量", false);
                //加错误信息
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AREngine.Base;

namespace AREngine.Cameras
{
    [Serializable]
    /// <summary>
    /// 静止的基本摄像机
    /// 范若余-大地无敌 2011/10/21
    /// </summary>
    public class ARBaseCamera:IARCamera
    {

        Matrix view;
        /// <summary>
        /// 相机目前的视觉矩阵
        /// </summary>
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        Matrix projection;
        /// <summary>
        /// 相机目前的投影矩阵
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        Vector3 up;
        /// <summary>
        /// 上方向量
        /// </summary>
        public Vector3 Up
        {
            get { return up; }
            set { up = value; }
        }
        Vector3 lookAt;
        /// <summary>
        /// 注视点
        /// </summary>
        public Vector3 LookAt
        {
            get { return lookAt; }
            set { lookAt = value; }
        }
        Vector3 position;
        /// <summary>
        /// 相机位置
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// 定义一个基本摄像机
        /// </summary>
        /// <param name="position">相机位置</param>
        /// <param name="lookAt">注视点</param>
        /// <param name="up">上方向量</param>
        public ARBaseCamera(Vector3 position, Vector3 lookAt, Vector3 up,Matrix projection)
        {
            this.position = position;
            this.lookAt = lookAt;
            this.up = up;
            this.view = Matrix.CreateLookAt(position, lookAt, up);
            this.projection = projection;

        }



        /// <summary>
        /// 定义一个基本摄像机，上方向量为(0,1,0)
        /// </summary>
        /// <param name="position">相机位置</param>
        /// <param name="lookAt">注视点</param>
        public ARBaseCamera(Vector3 position, Vector3 lookAt,Matrix projection) : this(position, lookAt, Vector3.Up,projection) { }
        /// <summary>
        /// 更新视觉矩阵
        /// </summary>
        public void UpdateView()
        {
            this.view = Matrix.CreateLookAt(position, lookAt, up);
        }
        
    }
}

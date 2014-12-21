using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Cameras
{
    
    /// <summary>
    /// 摄像机 
    /// BY:范若余-大地无敌 2011/10/21
    /// </summary>
    public interface IARCamera
    {
        /// <summary>
        /// 相机目前的视觉矩阵
        /// </summary>
        Matrix View
        {
            get;
            //set;
        }

        /// <summary>
        /// 相机目前的投影矩阵
        /// </summary>
        Matrix Projection
        {
            get;
            //set;
        }

        Vector3 Position
        {
            get;
        }

        Vector3 LookAt
        {
            get;
        }

        Vector3 Up
        {
            get;
        }

    }
}

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
    /// 3D物体接口
    /// </summary>
    public interface IAR3D:IARWithWorldMatrix
    {
        /// <summary>
        /// 位置（相对）
        /// </summary>
        Vector3 Position { get; set; }
        ///// <summary>
        ///// 速度
        ///// </summary>
        //Vector3 Velocity { get; set; }
        /// <summary>
        /// 旋转矩阵（相对）
        /// </summary>
        Matrix Rotation { get; }
        /// <summary>
        /// 三轴缩放，若为负则对称
        /// </summary>
        Vector3 Scale { get; set; }
        ///// <summary>
        ///// 世界矩阵（绝对）
        ///// </summary>
        //Matrix World { get; }
        
      

    }
}

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

namespace AREngine.Physics
{
    /// <summary>
    /// 3D物理物件的接口
    /// </summary>
    public interface IAR3DPhysical:IARPhysical
    {
        /// <summary>
        /// 速度
        /// </summary>
        Vector3 Velocity { get; set; }
        /// <summary>
        /// 加速度
        /// </summary>
        Vector3 Acceleration { get; set; }
        ////加速度
        //Vector3 acceleration;
        ///// <summary>
        ///// 加速度
        ///// </summary>
        //public Vector3 Acceleration
        //{
        //    get { return acceleration; }
        //    set { acceleration = value; }
        //}
    }
}

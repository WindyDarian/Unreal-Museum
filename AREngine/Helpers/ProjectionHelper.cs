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
using AREngine.Cameras;
using AREngine.Graphs;

namespace AREngine.Helpers
{
    /// <summary>
    /// 投影帮助器
    /// </summary>
    public static class ProjectionHelper
    {
        /// <summary>
        /// 将三维的点投影在二维
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="viewport">视点</param>
        /// <param name="v">三维点</param>
        /// <returns></returns>
        public static Vector2 Project(this Vector3 v,IARCamera camera,Viewport viewport)
        {
            Vector3 t = viewport.Project(v, camera.Projection, camera.View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }
    }
}

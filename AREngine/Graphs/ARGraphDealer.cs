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
using AREngine.Base;
using AREngine.Graphs.Graphs3D;
using AREngine.Cameras;

namespace AREngine.Graphs
{
    /// <summary>
    /// Draw方法携带的gameTime，2011/6/15 大地无敌-范若余
    /// </summary>
    public class ARGraphDealer:ARUpdateDealer
    {

        private ARModelDrawer modelDrawer;
        public ARModelDrawer ModelDrawer
        {
            get { return modelDrawer; }
        }
        private IARCamera camera ;
        private Matrix projection;
        /// <summary>
        /// 投影矩阵
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }
        /// <summary>
        /// 相机
        /// </summary>
        public IARCamera Camera
        {
            get { return camera; }
            //set { camera = value; }
        }

        private ARPainter painter;

        /// <summary>
        /// 调用的ARPainter，绘制2D
        /// </summary>
        public ARPainter Painter
        {
            get { return painter; }
        }

        public ARGraphDealer(ARXNAGame game)
            : base(game)
        {
           
            painter = new ARPainter(game);
            modelDrawer = new ARModelDrawer(game);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), //待编辑
                                                             1.6f,
                                                             1.0f,
                                                             5000.0f);
            camera = new ARBaseCamera(new Vector3(0, 45, 35), Vector3.Zero, projection);

        }

        public void SetCamera(IARCamera Camera)
        {
            this.camera = Camera;
        }
        
        
    }
}

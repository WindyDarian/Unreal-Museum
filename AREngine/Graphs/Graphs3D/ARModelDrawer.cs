using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AREngine.Base;
using AREngine.Cameras;

namespace AREngine.Graphs.Graphs3D
{
    public class ARModelDrawer:ARDealer
    {
        public ARModelDrawer(ARXNAGame game)
            : base(game)
        {

        }
        /// <summary>
        /// 画一个标准模型
        /// </summary>
        /// <param name="model">XNA模型</param>
        /// <param name="camera">摄像机</param>
        /// <param name="world">世界矩阵</param>
        public void DrawXNAModel(Model model, IARCamera camera, Matrix world)
        {
            DrawXNAModel(model, camera, world, 1);
        }

        public void DrawXNAModel(Model model, IARCamera camera, Matrix world, float alpha)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            //game.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            //game.GraphicsDevice.RenderState.AlphaTestEnable = true;
            Game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();

                    effect.World = transforms[mesh.ParentBone.Index]
                            * world;
                    effect.View = camera.View;
                    //effect.FogEnabled = true;
                    //effect.FogEnd = 12000;
                    //effect.FogStart = 2000;
                    effect.Alpha = alpha;

                    effect.Projection = camera.Projection;

                }
                mesh.Draw();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AREngine.Base;
using AREngine.Graphs;

namespace Illusion.Scene
{
    /// <summary>
    /// 可绘出的模型 
    /// 范若余-大地无敌 2011/10/21
    /// </summary>
    public class IllModel
    {
        string modelAssetName;
        /// <summary>
        /// 模型字符串
        /// </summary>
        public string ModelAssetName
        {
            get { return modelAssetName; }
            set { modelAssetName = value; }
        }

        /// <summary>
        /// 位置
        /// </summary>
        Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        Vector3 velocity;
        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        


        /// <summary>
        /// YawPitchRaw
        /// </summary>
        Vector3 yawPitchRaw = new Vector3(0, 0, 0);

        public Vector3 YawPitchRaw
        {
            get { return yawPitchRaw; }
            set { yawPitchRaw = value; }
        }


        private Game game;

        public Matrix Rotation
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(yawPitchRaw.X, yawPitchRaw.Y, yawPitchRaw.Z);
            }
        }

        


        /// <summary>
        /// 获得该模型的世界矩阵
        /// </summary>
        public Matrix World
        {
            get
            {
                Matrix mt1 = Matrix.CreateScale(scale);
                Matrix mt2 = Rotation;
                Matrix mt3 = Matrix.CreateTranslation(position);
                return mt1 * mt2 * mt3;
            }
        }


        public IllModel(string model, Vector3 position, Vector3 yawPitchRaw, float scale,Game game)
        {
            this.position = position;
            this.yawPitchRaw = yawPitchRaw;
            this.scale = new Vector3(scale, scale, scale);
            this.modelAssetName = model;
            this.game = game;
        }

        


        public void Draw(GameTime gameTime,Matrix view,Matrix projection)
        {
            Model model = game.Content.Load<Model>(modelAssetName);
            Matrix[] transforms = new Matrix[model.Bones.Count];
            
            model.CopyAbsoluteBoneTransformsTo(transforms);

            //game.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            //game.GraphicsDevice.RenderState.AlphaTestEnable = true;
            game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();

                    effect.World = transforms[mesh.ParentBone.Index]
                            * World;
                    effect.View = view;

                    effect.Projection = projection;

                }
                mesh.Draw();
            }
        }



    }
}

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
using AREngine.Graphs.Graphs3D;
using Illusion.Scene;
using AREngine.Graphs;
using AREngine.Base;
using AREngine.Cameras;
using Illusion.KinectSupport;
using BloomPostprocess;
using Microsoft.Kinect;

namespace Illusion
{
    /// <summary>
    /// 3D效果演示主类
    /// 创建BY 大地无敌-范若余  2011/10/21
    /// 修改BY 黎健成 2011/12/14
    /// 加入模型的切换 Bloom效果和天空包等 大地无敌-范若余 2011/12/27
    /// </summary>
    public class IllusionMain : ARXNAGame
    {
        ARBaseCamera camera1;
        Model model;
        ARGraphDealer graphDealer;
        ARUpdateDealer updateDealer;

        KinectComponent kinect;
        public static bool kinectbool = true;
        
        BloomComponent bloom;
        List<IllModel> models = new List<IllModel>(3);
        int currentIndex = 0;
        Matrix projection;
        Matrix illuMatrix;
        Texture2D skyTexture;

        public static float ScreenWidth = 0.34544f;
        public static float AspectRatio = 1.333f;

        ProjectMethod pMethod = ProjectMethod.IlluProjection;

        float rotation = 0;

        float rotateSpeed = MathHelper.PiOver2;

        public IllusionMain():base()
        {
            Content.RootDirectory = "Content";
            GraphicsDeviceManager.PreferredBackBufferWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //GraphicsDeviceManager.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            models.Add(new IllModel(@"Combined\Combined", new Vector3(0, 0, 0), new Vector3(0, MathHelper.ToRadians(0), 0), ScreenWidth / 0.34f
, this));
            models.Add(new IllModel(@"chahu\chahu", new Vector3(0, 0, 0), new Vector3(0, MathHelper.ToRadians(0), 0), ScreenWidth / 0.34f
, this));
            models.Add(new IllModel(@"huaping\huaping", new Vector3(0, 0, 0), new Vector3(0, MathHelper.ToRadians(0), 0), 0.3f * ScreenWidth / 0.34f
, this));
            models.Add(new IllModel(@"floor\floor", new Vector3(0, 0, 0), new Vector3(0, MathHelper.ToRadians(0), 0), 0.2f * ScreenWidth / 0.34f
, this));
     
            graphDealer = new ARGraphDealer(this);
            camera1 = new ARBaseCamera(new Vector3(0, 0, 1), new Vector3(0, 0, -2),graphDealer.Projection);
            camera1.Position = new Vector3(0, 0, 0.4f);
            graphDealer.SetCamera(camera1);
            updateDealer = new ARUpdateDealer(this);
            //GraphicsDeviceManager.ToggleFullScreen();

            if (kinectbool && KinectSensor.KinectSensors.Count > 0)
            {
                kinect = new KinectComponent(this);
                Components.Add(kinect);
            }

            bloom = new BloomComponent(this);
            bloom.Settings = BloomSettings.PresetSettings[0];
            //bloom.Visible = false;
            Components.Add(bloom);
            
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            skyTexture = Content.Load<Texture2D>(@"SkyBox\Sky1");
            model = Content.Load<Model>(@"SkyBox\Skybox");
            base.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            PlayerControl.UpdateInput(this);
            updateDealer.Update(gameTime);

            if (PlayerControl.IsKeyDown(Keys.Escape)) this.Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            if (PlayerControl.IsKeyDown(Keys.W))
            {
                camera1.Position += new Vector3(0, 0.5f * updateDealer.ElapsedTime, 0);

            }
            if (PlayerControl.IsKeyDown(Keys.S))
            {
                camera1.Position += new Vector3(0, -0.5f * updateDealer.ElapsedTime, 0);
            }
            if (PlayerControl.IsKeyDown(Keys.A))
            {
                camera1.Position += new Vector3(-0.5f * updateDealer.ElapsedTime, 0, 0);
            }
            if (PlayerControl.IsKeyDown(Keys.D))
            {
                camera1.Position += new Vector3(0.5f * updateDealer.ElapsedTime, 0, 0);
            }
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                camera1.Position += new Vector3(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * 0.5f * updateDealer.ElapsedTime, 0);
                rotation -= (GamePad.GetState(PlayerIndex.One).Triggers.Left - GamePad.GetState(PlayerIndex.One).Triggers.Right) * rotateSpeed * updateDealer.ElapsedTime;
            }
            if (PlayerControl.IsKeyPressed(Keys.Space))
            {
                SwitchModel();
            }
            if (PlayerControl.IsKeyDown(Keys.Q))
            {
                rotation -= rotateSpeed * updateDealer.ElapsedTime;
            }
            if (PlayerControl.IsKeyDown(Keys.E))
            {
                rotation += rotateSpeed * updateDealer.ElapsedTime;
            }
            if (PlayerControl.IsKeyPressed(Keys.P))
            {
                if (pMethod == ProjectMethod.XNAProjection) pMethod = ProjectMethod.IlluProjection;
                else pMethod = ProjectMethod.XNAProjection;

            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           
            bloom.BeginDraw();
            GraphicsDevice.Clear(Color.Black);

            
            if (kinectbool && kinect != null)
            {
                camera1.Position = kinect.Headposition;
            }
            else
            {
            }
            
            camera1.LookAt = camera1.Position + new Vector3(0, 0, -3f);
           
            camera1.UpdateView();
            rotation = MathHelper.WrapAngle(rotation);
            //float angle;
           
            //angle = MathHelper.Clamp((float)Math.Atan(ScreenWidth / (2 * camera1.Position.Z)) * 2, 0.0001f, MathHelper.Pi - 0.0001f);//获得头部角度
//angle = 0.35877067f;

#region 通过投影方式计算投影矩阵
            switch (pMethod)
            {
                case ProjectMethod.XNAProjection:
                    graphDealer.Projection = MatrixHelper.CreateXNAProjection(Vector3.Normalize(Vector3.Zero - camera1.Position)
                        , camera1.Position.Z
                        , ScreenWidth
                        , AspectRatio
                        , 0.01f
                        , 7000f);

                    break;
                case ProjectMethod.IlluProjection:


                    #region 得到偏斜矩阵，详情请访问我们的参赛博客。
                    illuMatrix = MatrixHelper.CreateIlluMatrix(Vector3.Normalize(Vector3.Zero - camera1.Position));
                    #endregion

                    //graphDealer.Projection =
                    //    illuMatrix *
                    //    Matrix.CreatePerspectiveFieldOfView(angle, //待编辑
                    //    AspectRatio,
                    //    0.01f,
                    //    7000.0f);
                    float a = 0.01f / camera1.Position.Z;
                    graphDealer.Projection = illuMatrix * Matrix.CreatePerspective(ScreenWidth * a, ScreenWidth / AspectRatio * a, 0.01f, 7000f);
                    break;
                default:
                    break;
            }
#endregion
         
          
            graphDealer.Update(gameTime);

            
            #region 画天空包
            Matrix[] skytransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(skytransforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    if (skyTexture != null)
                    {
                        effect.Texture = skyTexture;
                    }
                    effect.AmbientLightColor = new Vector3(1, 1, 1);
                    effect.World = skytransforms[mesh.ParentBone.Index]
                        * Matrix.CreateScale(100.0f)
                        * Matrix.CreateRotationY(MathHelper.ToRadians(-60))
                         * Matrix.CreateRotationY(rotation)
                        * Matrix.CreateTranslation(camera1.Position);
                    effect.View = camera1.View;
                    effect.Projection = graphDealer.Projection;
                }
                mesh.Draw();
            }
            #endregion

            models[currentIndex].YawPitchRaw = new Vector3(rotation,0 , 0);
            models[currentIndex].Draw(gameTime, graphDealer.Camera.View , graphDealer.Projection);

            base.Draw(gameTime);

            SpriteBatch.Begin();
            string s = "Head Position: " + camera1.Position.ToString()
                + "\nRotation: " + rotation
                + "\nKinect Position: " + KinectComponent.KinectOffset.ToString()
                //+ "FieldofView: " 
                //+ MathHelper.ToDegrees(angle).ToString() 
                + "\nScreen Width: " + ScreenWidth
                + "\nAspectRatio: " + AspectRatio
                + "\nPress Space to switch to the next scene.";
            if (!kinectbool)
            {
                s += "\nPress W, S, A, D, Q, E to change view.";
            }
            SpriteBatch.DrawString(Content.Load<SpriteFont>("defaultFont"), s , new Vector2(20, 20), Color.White);
            SpriteBatch.End();
        }

        void SwitchModel()
        {
            currentIndex++;
            currentIndex %= models.Count;
            rotation = 0;
        }

        /// <summary>
        /// 投影方式，是用我们开发的投影还是用Kinect自带的斜投影矩阵
        /// </summary>
        enum ProjectMethod
        {
            XNAProjection,
            IlluProjection,
        }
    }

}

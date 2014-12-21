using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;
using AREngine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AREngine
{
    /// <summary>
    /// AR游戏的基本结构,继承自ARXNAGame，拥有一个界面管理器ARSceneManager、UpdateDealer和GraphDealer，会调用当前界面Update 、Draw 和 SUpdate
    /// 在SceneManager中加入新界面后即可正常运行游戏！！
    /// 大地无敌 2011.7.3
    /// </summary>
    public class ARGameBase:ARXNAGame
    {
        FpsShower fps;

     
        int sUpdateTimer = 5;
        /// <summary>
        /// 执行SUpdate时Update的次数
        /// </summary>
        public int SUpdateTimer
        {
            get { return sUpdateTimer; }
            set { sUpdateTimer = value;
            updateCount = 0;
            }
        }
        int updateCount = 0;

        TimeSpan sUpdateElapsedTime;

   
        ARSceneManager sceneManager;
        /// <summary>
        /// 界面管理器
        /// </summary>
        protected  ARSceneManager SceneManager
        {
            get { return sceneManager; }
        }


        ARUpdateDealer updateDealer;

        protected ARUpdateDealer UpdateDealer
        {
            get { return updateDealer; }
        }

        ARUpdateDealer supdateDealer;

        protected ARUpdateDealer SUpdateDealer
        {
            get { return supdateDealer; }
        }


        ARGraphDealer graphDealer;

        public ARGraphDealer GraphDealer
        {
            get { return graphDealer; }
            set { graphDealer = value; }
        }

        public ARGameBase()
            : base()
        {
            sceneManager = new ARSceneManager(this);
            fps = new FpsShower(this);
            Components.Add(fps);
            
            
        }
        protected override void Initialize()
        {
            updateDealer = new ARUpdateDealer(this);
            graphDealer = new ARGraphDealer(this);
            supdateDealer = new ARUpdateDealer(this);
           

            base.Initialize();
        }
        protected override void LoadContent()
        {

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            
            updateDealer.Update(gameTime);
            InputState.UpdateInput(this);
            SceneManager.Update(updateDealer);

            //SUpdate未加上！
            base.Update(gameTime);

            updateCount++;
            sUpdateElapsedTime+=gameTime.ElapsedGameTime;
            if (updateCount >= SUpdateTimer)
            {
                supdateDealer.Update(new GameTime(gameTime.TotalGameTime, sUpdateElapsedTime, false));
                sUpdateElapsedTime = TimeSpan.Zero;
                sceneManager.SUpdate(supdateDealer);
                updateCount = 0;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            graphDealer.Update(gameTime);
            SceneManager.Draw(graphDealer);

            base.Draw(gameTime);
        }
    }
}

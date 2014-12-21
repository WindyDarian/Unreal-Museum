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
using AREngine.Stage;


namespace AREngine
{
    [Serializable]
    /// <summary>
    /// 游戏世界的基本,大地无敌2011.3.2
    /// </summary>
    public  class ARWorldBase:ARObject,IARDrawable
    {
        
        ARStage currentStage = new ARStage();
        /// <summary>
        /// 当前关卡
        /// </summary>
        public ARStage CurrentStage
        {
            get { return currentStage; }
            set { currentStage = value; }
        }

        /// <summary>
        /// 得到该世界的game
        /// </summary>
        private ARGameBase game;
        public ARGameBase Game
        {
            get { return game; }
        }

        /// <summary>
        /// SpriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return Game.SpriteBatch; }
        }
        /// <summary>
        /// SpriteBatch
        /// </summary>
        public ContentManager Content
        {
            get { return Game.Content; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get {return Game.GraphicsDevice;}
        }

        /// <summary>
        /// 新的游戏世界底层
        /// </summary>
        public ARWorldBase(ARGameBase game)
        {
            this.game = game;
        }
        public override void Update(ARUpdateDealer dealer)
        {
            currentStage.Update(dealer);
        }
        public override void SUpdate(ARUpdateDealer dealer)
        {
            currentStage.SUpdate(dealer);
        }
        public virtual void Draw(ARGraphDealer dealer)
        {
            currentStage.Draw(dealer);
        }


#region 序列化
        private ARGameBase tempGame;
        public virtual void PreSerialize()
        {
            game = null;
            tempGame = game;
        }
        public virtual void AfterSerialize()
        {
            if (tempGame == null)
            {
                throw new ApplicationException("tempGame为空，可能未运行PreSerialize");
            }
            else
            {
                game = tempGame;
                tempGame = null;
             
            }
        }
#endregion
    }
}

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
    /// Update或Draw的管理器，2011/6/15大地无敌
    /// </summary>
    public class ARUpdateDealer:ARDealer
    {
        private GameTime gameTime;
        /// <summary>
        /// 游戏时间
        /// </summary>
        public GameTime GameTime
        {
            get { return gameTime; }
        }

        float elapsedTime;
        /// <summary>
        /// 与上一帧的时间间隔（秒）
        /// </summary>
        public float ElapsedTime
        {
            get { return elapsedTime; }
        }

        public ARUpdateDealer(ARXNAGame game)
            : base(game)
        {

        }

        /// <summary>
        /// 每次Update和Draw之前应运行一次，使其时间更新
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            this.elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
    }
}

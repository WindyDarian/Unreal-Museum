using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;

namespace AREngine
{

    /// <summary>
    /// 一个游戏界面，如主菜单界面和游戏界面，2010.6.10-大地无敌-范若余，不可序列化
    /// </summary>
    public class ARScene:ARObject,IARDrawable
    {
        public EventHandler Closed;
        public EventHandler Acivated;
        bool isActive = false;
        /// <summary>
        /// 得到是否激活，然后可决定是否绘出
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
        }
        ARGameBase game;
        /// <summary>
        /// 返回该界面所属game
        /// </summary>
        public ARGameBase Game
        {
            get { return game; }
        }
        
        /// <summary>
        /// 创建一个新界面
        /// </summary>
        /// <param name="game">所属Game</param>
        public ARScene(ARGameBase game)
        {
            this.game = game;
            Initialize();
        }

        /// <summary>
        /// 私有化
        /// </summary>
        private ARScene()
        { }


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// 激活
        /// </summary>
        public virtual void Activate()
        {
            isActive = true;
            Acivated(this, EventArgs.Empty);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            isActive = false;
            Closed(this, EventArgs.Empty);
        }

        /// <summary>
        /// 重置状态时进行的方法，刚创建时也会进行该方法！
        /// </summary>
        public virtual void Reset()
        {
            ;
        }
        public virtual void Draw(ARGraphDealer dealer)
        {
            //待编辑
        }
    }
}

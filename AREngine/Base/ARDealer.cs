using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Base
{
    /// <summary>
    /// 管理操作的类的基本，非ARObject，不可序列化
    /// </summary>
    public class ARDealer
    {
        ARXNAGame game;
        /// <summary>
        /// 获得其所属的游戏
        /// </summary>
        public ARXNAGame Game
        {
            get { return game; }
        }
        public ARDealer(ARXNAGame game)
        {
            this.game = game;
        }
    }
}

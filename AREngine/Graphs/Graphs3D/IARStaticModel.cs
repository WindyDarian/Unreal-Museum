using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Graphs.Graphs3D
{
    /// <summary>
    /// AR静态模型接口 
    /// 2011/11/1 
    /// </summary>
    public interface IARStaticModel
    {
        /// <summary>
        /// 在给定的世界矩阵 绘出
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="world">世界矩阵</param>
        void Draw(ARGraphDealer dealer, Matrix world);
    }
}

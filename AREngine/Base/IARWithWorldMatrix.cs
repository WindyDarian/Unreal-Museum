using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Base
{
    /// <summary>
    /// 有世界矩阵的物件
    /// 大地无敌-范若余 2011/12/7
    /// </summary>
    public interface IARWithWorldMatrix
    {
        Matrix World
        {
            get;
        }
    }
}

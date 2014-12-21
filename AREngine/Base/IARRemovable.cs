using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Base
{
    /// <summary>
    /// 表示可移除物件的接口,大地无敌2011/8/23
    /// </summary>
    public interface IARRemovable
    {
        bool Removing
        {
            get;
        }

        /// <summary>
        /// 可将Removing设为True以便Manager移除
        /// </summary>
        void Remove();
    }
}

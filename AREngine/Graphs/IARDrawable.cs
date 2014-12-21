using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Graphs
{
    public interface IARDrawable
    {
        /// <summary>
        /// 绘出!!
        /// </summary>
        /// <param name="dealer">处理者</param>
        void Draw(ARGraphDealer dealer);
    }
}

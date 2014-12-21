using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Physics
{
    /// <summary>
    /// 可以自行移动的单位
    /// </summary>
    public interface IARMoveable
    {
        void MoveOneFrame();
    }
}

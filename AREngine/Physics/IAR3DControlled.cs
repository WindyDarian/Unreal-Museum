using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AREngine.Physics
{
    /// <summary>
    /// 可以控制移动旋转等的物体;
    /// </summary>
    interface IAR3DControlled
    {
        void MoveOneFrame();
        void RotateFrame(Vector3 target);
    }
}

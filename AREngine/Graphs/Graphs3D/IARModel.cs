using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;

namespace AREngine.Graphs.Graphs3D
{
    /// <summary>
    /// 3D模型物件的接口
    /// 大地无敌 范若余 2011/12/6
    /// </summary>
    public interface IARModel:IARWithWorldMatrix,IARDrawable
    {
        /// <summary>
        /// 和父模型或主物件的位置同步
        /// </summary>
        /// <param name="obj">父物体</param>
        void Sync(IARWithWorldMatrix obj);
    }
}

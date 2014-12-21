using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Base
{
    /// <summary>
    /// 表示可更新的部件
    /// </summary>
    public interface IARUpdateable
    {
        void Update(ARUpdateDealer dealer);
        void SUpdate(ARUpdateDealer dealer);
    }
}

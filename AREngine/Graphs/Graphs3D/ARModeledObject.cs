using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;

namespace AREngine.Graphs.Graphs3D
{
    /// <summary>
    /// 有模型的物体
    /// </summary>
    public class ARModeledObject:AR3DObject,IARDrawable
    {
        private IARModel model;
        /// <summary>
        /// 模型
        /// </summary>
        public IARModel Model
        {
            get { return model; }
            set { model = value; }
        }

        

        public override void Update(Base.ARUpdateDealer dealer)
        {
            base.Update(dealer);
            model.Sync(this);

        }

        public virtual void Draw(ARGraphDealer dealer)
        {
            model.Draw(dealer);
        }

    }
}

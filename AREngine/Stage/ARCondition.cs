using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;

namespace AREngine.Stage
{
    [Serializable]
    /// <summary>
    /// 条件基础，继承后得到一种类型的调件，创建于2011/6/16，大地无敌-范若余
    /// 最初完成：大地无敌-范若余2011/6/23
    /// </summary>
    public class ARCondition:ARObject
    {
        ARStage stage;
        /// <summary>
        /// 所属关卡
        /// </summary>
        public ARStage Stage
        {
            get { return stage; }
        }




        /// <summary>
        /// 创建一个条件
        /// </summary>
        /// <param name="stage">所属关卡</param>
        public ARCondition(ARStage stage)
        {
            this.stage = stage;
        }

        /// <summary>
        /// 检查该条件是否符合，原始方法会返回false
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual bool Check(ARUpdateDealer dealer)
        {
            return false;
        }


    }
}

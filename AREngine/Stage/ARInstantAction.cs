using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine.Stage
{
    /// <summary>
    /// 1帧内瞬间完成的时间，避免创建该类型事件时忘记使事件结束，创建&最初完成：大地无敌2011/6/23
    /// </summary>
    public class ARInstantAction:ARAction
    {
        public ARInstantAction(ARStage stage)
            : base(stage)
        {

        }
        public override void Perform(Base.ARUpdateDealer dealer)
        {
            IsFinished = true;
            base.Perform(dealer);
        }
        public override void Check(Base.ARUpdateDealer dealer)
        {
            IsFinished = true;
            base.Check(dealer);
        }
    }
}

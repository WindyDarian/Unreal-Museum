using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using Microsoft.Xna.Framework.Content;

namespace AREngine.Stage
{
    [Serializable]
    /// <summary>
    /// 触发器中的一个动作,大地无敌-范若余2011/6/23
    /// 最初完成：大地无敌-范若余2011/6/23
    /// </summary>
    public class ARAction:ARObject
    {

        ARStage stage;
        /// <summary>
        /// 所属关卡
        /// </summary>
        public ARStage Stage
        {
            get { return stage; }
           // set { stage = value; }
        }

        bool isFinished = false;
        /// <summary>
        /// 动作是否已完成
        /// </summary>
        public bool IsFinished
        {
            get { return isFinished; }
            set { isFinished = value; }
        }

        bool isStarted = false;
        /// <summary>
        /// 动作是否已开始
        /// </summary>
        public bool IsStarted
        {
            get { return isStarted; }
           // set { isStarted = value; }
        }


        public ARAction(ARStage stage)
        {
            this.stage = stage;
        }


        /// <summary>
        /// 对执行中尚未完成的动作执行一次Check，在持续一段时间的动作中检查动作是否已完成,如果已完成则标示为完成,对于已完成的动作,触发器将开始执行它的下一个动作
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(ARUpdateDealer dealer)
        {
            Check(dealer);
            base.Update(dealer);
        }

        /// <summary>
        /// 开始执行动作，然后将IsStarted设置为True再执行一次Check，第一次检查该动作是否完成(对于刚开始执行的动作，触发器可能在一帧内检查两次它是否完成）
        /// </summary>
        public virtual void Perform(ARUpdateDealer dealer)
        {
            isStarted = true;
            Check(dealer);
        }

        /// <summary>
        /// 进行是否完成的检查，另外如果动作甚至没有开始就将isFinished设为False
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Check(ARUpdateDealer dealer)
        {
            if (isStarted == false)
            {
                isFinished = false;
            }
        }
    }
}

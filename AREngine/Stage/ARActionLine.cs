using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;

namespace AREngine.Stage
{
    [Serializable]
    /// <summary>
    /// 正在由关卡处理中的一列动作！！,大地无敌-范若余 2011/6/23
    /// 最初完成：大地无敌-范若余2011/6/23
    /// </summary>
    public class ARActionLine:ARObject 
    {
        ARStage stage;
        /// <summary>
        /// 所属关卡
        /// </summary>
        public ARStage Stage
        {
            get { return stage; }
        }

        ARTrigger trigger;
        /// <summary>
        /// 触发该动作列的触发器
        /// </summary>
        public ARTrigger Trigger
        {
            get { return trigger; }
            set { trigger = value; }
        }

        private List<ARAction> actions = new List<ARAction>(20);
        /// <summary>
        /// 动作列
        /// </summary>
        public List<ARAction> Actions
        {
            get { return actions; }
        }

        //正在进行的动作号，如果未进行则为-1
        int currentAction = -1;

        public ARActionLine(ARStage stage, ARTrigger trigger,List<ARAction> actions)
        {
            this.stage = stage;
            this.trigger = trigger;
            this.actions.AddRange(actions);
        }

        public void Start(ARUpdateDealer dealer)
        {
            if (actions.Count > 0)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].Perform(dealer);
                    if (!actions[i].IsFinished)
                    {
                        currentAction = i;
                        //如果有动作不是即时完成的话就等下一帧
                        return;
                    }

                }
                currentAction = -1;

            }
        }


        /// <summary>
        /// 如果有没完的动作则继续
        /// </summary>
        /// <param name="dealer"></param>
        void Continue(ARUpdateDealer dealer)
        {
            actions[currentAction].Update(dealer);
            if (actions[currentAction].IsFinished)
            {
                currentAction += 1;
            }
            for (int i = currentAction; i < actions.Count; i++)
            {
                actions[i].Perform(dealer);
                if (!actions[i].IsFinished)
                {
                    currentAction = i;
                    //如果有动作不是即时完成的话就等下一帧
                    return;
                }

            }
            currentAction = -1;
        }

        public override void Update(ARUpdateDealer dealer)
        {
            //如果没有动作的话就可以清理掉，清理的方法在ARObject中
            if (!this.Removing)
            {
                if (currentAction >= 0)
                {
                    this.Continue(dealer);
                }
                else
                {
                    this.Remove();
                }

            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;

namespace AREngine.Stage
{
    [Serializable]
    /// <summary>
    /// 关卡中的触发器,大地无敌-范若余,2011/6/23
    /// 事件系统最初完成：大地无敌-范若余2011/6/23
    /// </summary>
    public class ARTrigger:ARObject
    {
        
        ARStage stage;
        /// <summary>
        /// 所属关卡
        /// </summary>
        public ARStage Stage
        {
            get { return stage; }
        }

        private List<ARAction> actions = new List<ARAction>(20);
        /// <summary>
        /// 动作列表
        /// </summary>
        public List<ARAction> Actions
        {
            get { return actions; }
        }

        private List<ARCondition> conditions = new List<ARCondition>(5);
        /// <summary>
        /// 条件
        /// </summary>
        public List<ARCondition> Conditions
        {
            get { return conditions; }
        }
    
        bool closed = false;
        /// <summary>
        /// 触发器是否已关闭
        /// </summary>
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public ARTrigger(ARStage stage)
        {
            this.stage = stage;
        }



        /// <summary>
        /// 尝试触发该触发器，先验证未关闭，再逐个验证所有条件，最后逐个执行所有动作  外部调用！！
        /// </summary>
        public void Trigger(ARUpdateDealer dealer)
        {
            if (Closed)
            {
                return;
            }
            if (!Check(dealer))
            {
                return;
            }
            //触发器未关闭，条件符合！开始动作！！
            Start(dealer);
        }

        /// <summary>
        /// 开始动作，创建一个新的动作列并发送给当前关卡，然后立即开始执行
        /// </summary>
        /// <param name="dealer"></param>
        void Start(ARUpdateDealer dealer)
        {
            ARActionLine actionline = new ARActionLine(stage, this, actions);
            stage.AddActionLine(actionline);
            actionline.Start(dealer);

        }


        /// <summary>
        /// 关闭触发器，无法再触发
        /// </summary>
        public void Close()
        {
            closed = true;
        }

        /// <summary>
        /// 检查是否符合所有触发条件
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        bool Check(ARUpdateDealer dealer)
        {
            foreach (var con in conditions)
            {
                if (!con.Check(dealer))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

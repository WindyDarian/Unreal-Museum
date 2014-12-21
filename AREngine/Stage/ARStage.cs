using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Graphs;
using AREngine.Base;

namespace AREngine.Stage
{
    [Serializable]
    /// <summary>
    /// 关卡基础，创建于2011/6/16，大地无敌-范若余，待编辑
    /// 事件系统最初完成：大地无敌-范若余2011/6/23
    /// </summary>
    public class ARStage:ARObject,IARDrawable
    {
        
        List<ARTrigger> triggers = new List<ARTrigger>(12);
        /// <summary>
        /// 关卡触发器
        /// </summary>
        public List<ARTrigger> Triggers
        {
            get { return triggers; }
        }

        ARManager<ARActionLine> actionLines = new ARManager<ARActionLine>(10);
        //ARManager是AREngine中管理物件的类
        public ARManager<ARActionLine> ActionLines
        {
            get { return actionLines; }
            set { actionLines = value; }
        }

        /// <summary>
        /// 添加动作列
        /// </summary>
        /// <param name="actionLine"></param>
        public void AddActionLine(ARActionLine actionLine)
        {
            actionLines.Add(actionLine);
        }

        public override void Update(ARUpdateDealer dealer)
        {
            actionLines.Update(dealer);
            base.Update(dealer);
        }
      
        //SUpdate是每隔一定时间进行的一些比较耗资源的更新
        public override void SUpdate(ARUpdateDealer dealer)
        {
            actionLines.SUpdate(dealer);
            base.SUpdate(dealer);
        }

        public virtual void Draw(ARGraphDealer dealer)
        {
            
        }

    }
}

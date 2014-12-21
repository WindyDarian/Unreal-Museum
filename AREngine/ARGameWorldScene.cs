using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AREngine
{
    /// <summary>
    /// 有GameWorld的画面
    /// 创建 2011/11/3 大地无敌-范若余
    /// </summary>
    public class ARGameWorldScene:ARScene
    {
        ARWorldBase world;

        public ARWorldBase World
        {
            get { return world; }
            set { world = value; }
        }

        public ARGameWorldScene(ARGameBase game)
            : base(game)
        {

        }

        public override void Update(Base.ARUpdateDealer dealer)
        {
            if (world != null)
            {

                world.Update(dealer);
            }
            base.Update(dealer);
        }

        public override void SUpdate(Base.ARUpdateDealer dealer)
        {
            if (world != null)
            {

                world.SUpdate(dealer);
            }
            base.SUpdate(dealer);
        }

        public override void Draw(Graphs.ARGraphDealer dealer)
        {
            if (world != null)
            {
                world.Draw(dealer);
            }
            base.Draw(dealer);
        }
    }
}

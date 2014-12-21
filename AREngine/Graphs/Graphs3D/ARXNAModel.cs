using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AREngine.Graphs.Graphs3D
{
    /// <summary>
    /// XNA模型
    /// 创建 2011/11/3 大地无敌-范若余
    /// </summary>
    [Serializable]
    public class ARXNAModel:ARModel
    {
        string modelPath;
        
        public string ModelPath
        {
            get { return modelPath; }
            set { modelPath = value; }
        }

        Model model;

        public Model Model
        {
            get { return model; }
        }

        public ARXNAModel()
        {
            
        }

        public ARXNAModel(string modelPath)
        {
            this.modelPath = modelPath;
        }

        public ARXNAModel(string modelPath,float scale):this(modelPath)
        {
            Scale = new Vector3(scale);
        }

        public override void Draw(ARGraphDealer dealer)
        {
            if (modelPath != "")
            {
                if (model== null)
                {
                    model = dealer.Game.Content.Load<Model>(modelPath);
                }
                dealer.ModelDrawer.DrawXNAModel(model, dealer.Camera, World);
            }
            base.Draw(dealer);
        }
        
    }
}

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
using AREngine.Base;

namespace AREngine.Graphs.Graphs3D
{
    [Serializable]
    /// <summary>
    /// 模型的抽象类
    /// 注意!!如果不是顶级模型 平移 旋转 缩放 都是相对父模型或父物件的
    /// 创建 大地无敌-范若余 2011/11/2
    /// </summary>
    public abstract class ARModel:IAR3D,IARDrawable,IARModel
    {
        Vector3 position = Vector3.Zero;
        /// <summary>
        /// 位置（相对）
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }


        Matrix rotation = Matrix.Identity;
        /// <summary>
        /// 旋转矩阵（相对）
        /// </summary>
        public Matrix Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        Vector3 scale = new Vector3(1, 1, 1);
        /// <summary>
        /// 三轴缩放（相对）
        /// </summary>
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        Matrix world = Matrix.Identity;
        [ContentSerializerIgnore]
        /// <summary>
        /// 世界矩阵
        /// </summary>
        public Matrix World
        {
            get
            {
                return world;
            }
        }

        private Matrix fatherWorld = Matrix.Identity;
        [ContentSerializerIgnore]
        /// <summary>
        /// 获取父矩阵
        /// </summary>
        public Matrix FatherWorld
        {
            get { return fatherWorld; }

        }


        /// <summary>
        /// 和父模型或主物件的位置同步
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Sync(IARWithWorldMatrix obj)
        {
            fatherWorld = obj.World;
            world = Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(Position) * fatherWorld;
        }

        public virtual void Draw(ARGraphDealer dealer)
        {
        }



        public virtual IARModel CreateInstance()
        {
            return (IARModel)MemberwiseClone();
        }
    }
}

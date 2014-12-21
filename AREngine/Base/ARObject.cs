using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AREngine.Base
{
    [Serializable]
    /// <summary>
    /// AR中基础的对象,By大地无敌@2011年1月14日
    /// </summary>
    public abstract class ARObject:IARUpdateable,IARRemovable
    {
        private bool removing = false;
        /// <summary>
        /// 得到是否正在移除该对象
        /// </summary>
        public bool Removing
        {
            get { return removing; }
        }

        public ARObject()
        {

        }


        /// <summary>
        /// 下一次SUpdate时将其从所属移除并任由.Net Framework的可恶的回收器处置
        /// </summary>
        public virtual void Remove()
        {
            removing = true;
        }
        /// <summary>
        /// 基本的Update
        /// </summary>
        public virtual void Update(ARUpdateDealer dealer)
        {

        }
        /// <summary>
        /// 更大的Update
        /// </summary>
        public virtual void SUpdate(ARUpdateDealer dealer)
        {

        }

        /// <summary>
        /// 读取存档或更改设置后会进行的方法，若需要则继承并写入
        /// </summary>
        public virtual void Reload(ARXNAGame game)
        {

        }
        ///// <summary>
        ///// ARObjectManager只会调用ARDrawableObject的Draw方法
        ///// </summary>
        //public virtual void Draw(ARUpdateDealer dealer)
        //{
            
        //}

    }
}

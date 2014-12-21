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
using AREngine.Base;

namespace AREngine.Graphs
{
    [Serializable]
    /// <summary>
    /// 绘图类的底层
    /// </summary>
    public class ARPainter:ARDealer
    {
        ARGameAreaDealer gameAreaDealer;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return Game.SpriteBatch; }
        }

        SpriteFont currentFont;
        /// <summary>
        /// 当前字体
        /// </summary>
        public SpriteFont CurrentFont
        {
            get { return currentFont; }
            set { currentFont = value; }
        }



        public ARPainter(ARXNAGame game)
            : base(game)
        {
            gameAreaDealer = new ARGameAreaDealer(game);
            currentFont = game.Content.Load<SpriteFont>("defaultFont");
        }
        /// <summary>
        /// 绘制2D纹理
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="gameArea">GameArea区域（解决不同分辨率视野范围不同的问题，会换算为真实屏幕坐标）</param>
        /// <param name="sourceRectangle">图片区域,null为全部</param>
        /// <param name="color">颜色，包含透明度</param>
        /// <param name="scale">缩放</param>
        /// <param name="center">缩放和旋转中心，如(0.5f,0.5f)为图片正中央</param>
        /// <param name="rotation">旋转弧度</param>
        public void DrawTexture(string texture, Rectangle gameArea, Rectangle? sourceRectangle, Color color, float scale, Vector2 center, float rotation,SpriteEffects effects,float layerDepth)
        {
            Rectangle windowRectangle = gameAreaDealer.GameToWindow(gameArea);
            Texture2D t = Game.Content.Load<Texture2D>(texture);
            //继续
            if (sourceRectangle != null)
            {
                SpriteBatch.Draw(t, windowRectangle, sourceRectangle, color, rotation, new Vector2(windowRectangle.Width * center.X, windowRectangle.Height * center.Y), effects, layerDepth);

            }
            else
            {
                SpriteBatch.Draw(t, windowRectangle, null , color, rotation, new Vector2(t.Width * center.X,  t.Height * center.Y), effects, layerDepth);

            }
           
          

        }
        /// <summary>
        ///  绘制2D纹理,简化的
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="location">GameArea坐标</param>
        /// <param name="color">颜色</param>
        public void DrawTexture(string texture, Vector2 location,Color color)
        {
            Texture2D t = Game.Content.Load<Texture2D>(texture);
            Rectangle picRectangle = new Rectangle((int)(location.X),(int)(location.Y),t.Width,t.Height);
            Rectangle tar = gameAreaDealer.GameToWindow(picRectangle);
            SpriteBatch.Draw(t, picRectangle, color);
        }
        /// <summary>
        ///  绘制2D纹理,简化的
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="location">GameArea坐标</param>
        public void DrawTexture(string texture, Vector2 location)
        {
            DrawTexture(texture, location, Color.White);
        }
        /// <summary>
        ///  绘制2D纹理,简化的
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="location">GameArea坐标</param>
        /// <param name="opacity">不透明度</param>
        public void DrawTexture(string texture, Vector2 location,float opacity)
        {
            Color c = Color.White;
            c.A = (byte)(c.A * opacity);
            DrawTexture(texture, location, c);
        }
        ///// <summary>
        /////  绘制2D纹理,简化的
        ///// </summary>
        ///// <param name="texture">纹理</param>
        ///// <param name="location">GameArea坐标</param>
        ///// <param name="opacity">不透明度</param>
        ///// <param name="scale">缩放</param>
        //public void DrawTexture(string texture, Vector2 location, float opacity,float scale)
        //{
        //    Color c = Color.White;
        //    c.A = (byte)(c.A * opacity);

        //}

        /// <summary>
        /// 开始绘图
        /// </summary>
        public void Begin()
        {
            SpriteBatch.Begin();
        }

        /// <summary>
        /// 结束绘图
        /// </summary>
        public void End()
        {
            SpriteBatch.End();
        }

        public void DrawString(string text, Vector2 position,Color color)
        {
            SpriteBatch.DrawString(currentFont, text, position, color);
        }

        public void SetFont(string font)
        {
            //try
            //{
                currentFont = Game.Content.Load<SpriteFont>(font);
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
        }
    }
}

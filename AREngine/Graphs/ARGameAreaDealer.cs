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
    /// <summary>
    /// 将屏幕坐标和游戏显示区域坐标进行转换的类，大地无敌2011.4.23
    /// </summary>
    public class ARGameAreaDealer:ARDealer
    {
        /// <summary>
        /// 游戏最适高度，游戏内图像的实际大小将按照屏幕高度和游戏最适高度的比例进行缩放，确保不同分辨率游戏体验一致。
        /// </summary>
        public const float GameHeight = 1200f;
       
        float gameWidth = 1920f;
        /// <summary>
        /// 游戏宽度，根据使用者屏幕分辨率计算，默认为1920f
        /// </summary>
        public float GameWidth
        {
            get { return gameWidth; }
        }

        float gameAspectRatio = 1.6f;
        /// <summary>
        /// 宽高比，根据根据使用者屏幕分辨率计算，默认为16:10
        /// </summary>
        public float GameAspectRatio
        {
            get { return gameAspectRatio; }
        }

 
        float lengthScale = 1.0f;
        float inverseLengthScale = 1.0f;
        /// <summary>
        /// 屏幕中长度与游戏中长度的比,即屏幕高度和游戏高度的比
        /// </summary>
        public float InverseLengthScale
        {
            get { return inverseLengthScale; }
        }
        /// <summary>
        /// 游戏中长度与屏幕中长度的比,即游戏高度和屏幕高度的比
        /// </summary>
        public float LengthScale
        {
            get { return lengthScale; }
        }

        public ARGameAreaDealer(ARXNAGame game)
            : base(game)
        {
            Resize();
        }

        /// <summary>
        /// 重新计算当前宽高比和游戏宽度,改变分辨率时需要
        /// </summary>
        public void Resize()
        {
            Resize(Game.GraphicsDevice.Viewport.Width
               , Game.GraphicsDevice.Viewport.Height);
            
        }

        /// <summary>
        /// 重新计算当前宽高比和游戏宽度
        /// <param name="windowHeight">屏幕高度</param>
        /// <param name="windowWidth">屏幕宽度</param>
        /// </summary>
        public void Resize(int windowWidth,int windowHeight)
        {
            gameAspectRatio = ((float)windowWidth) / ((float)windowHeight);
            gameWidth = GameHeight * gameAspectRatio;
            lengthScale = GameHeight / ((float)windowHeight);
            inverseLengthScale = 1f / lengthScale;
        }

        /// <summary>
        /// 将一个适应屏幕视窗的矩形转化为适应游戏大小的矩形
        /// </summary>
        /// <param name="windowRectangle">屏幕矩形</param>
        /// <returns></returns>
        public Rectangle WindowToGame(Rectangle windowRectangle)
        {
            return new Rectangle((int)(windowRectangle.X * lengthScale), 
                (int)(windowRectangle.Y * lengthScale),
                (int)(windowRectangle.Width * lengthScale),
                (int)(windowRectangle.Height * lengthScale));
        }
        /// <summary>
        /// 将一个适应游戏大小的矩形转化为适应屏幕视窗的矩形
        /// </summary>
        /// <param name="gameRectangle">游戏矩形</param>
        /// <returns></returns>
        public Rectangle GameToWindow(Rectangle gameRectangle)
        {
            return new Rectangle((int)(gameRectangle.X * inverseLengthScale),
                (int)(gameRectangle.Y * inverseLengthScale),
                (int)(gameRectangle.Width * inverseLengthScale),
                (int)(gameRectangle.Height * inverseLengthScale));
        }

        /// <summary>
        /// 将一个适应屏幕视窗的点转化为适应游戏大小的点
        /// </summary>
        /// <param name="windowPoint">屏幕点</param>
        /// <returns></returns>
        public Vector2 WindowToGame(Vector2 windowPoint)
        {
            return windowPoint * lengthScale;
        }

        /// <summary>
        /// 将一个适应游戏大小的点转化为适应屏幕视窗的点
        /// </summary>
        /// <param name="windowPoint">屏幕点</param>
        /// <returns></returns>
        public Vector2 GameToWindow(Vector2 gamePoint)
        {
            return gamePoint * inverseLengthScale;
        }
        
    }
}

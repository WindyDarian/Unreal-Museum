using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AREngine.Base;
using Microsoft.Xna.Framework.Graphics;
using AREngine.Helpers;

namespace AREngine.Graphs.Graphs3D
{
    /// <summary>
    /// 公告板模型元件
    /// </summary>
    public class ARSpriteBillboard:IARModel,IARDrawable
    {
        Vector3 relatedPosition = Vector3.Zero;
        string texture = "";
        float scale;
        Vector3 absolutePosition;
        
        Matrix baseWorld;

        Texture2D loadedTexture;
        Vector4 projectedPosition;

        

        public ARSpriteBillboard(float scale,string texture)
        {
            this.texture = texture;
            this.scale = scale;
            
        }

        public void Sync(IARWithWorldMatrix obj)
        {
            baseWorld = obj.World;
            
            absolutePosition = Vector3.Transform(relatedPosition, baseWorld);
            
        }

        /// <summary>
        /// 返回或设置位置 相对
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return relatedPosition;
            }
            set
            {
                relatedPosition = value;
            }
        }

        public Matrix Rotation
        {
            get { throw new NotImplementedException(); }
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        public Matrix World
        {
            get 
            {
                return Matrix.CreateTranslation(relatedPosition) * baseWorld;
            
            }
        }

        public void Draw(ARGraphDealer dealer)
        {
            if (loadedTexture== null)loadedTexture = dealer.Game.Content.Load<Texture2D>(texture);
            projectedPosition = Vector4.Transform(new Vector4(absolutePosition, 1),dealer.Camera.View*dealer.Camera.Projection);
            Vector2 size = new Vector2(loadedTexture.Width * scale, loadedTexture.Height * scale);
            Vector2 screenPosition = absolutePosition.Project(dealer.Camera, dealer.Game.GraphicsDevice.Viewport);
            size = size * dealer.Camera.Projection.M11 / projectedPosition.W * dealer.Game.GraphicsDevice.Viewport.Height / 2;
            Rectangle t = new Rectangle((int)(screenPosition.X-size.X/2),(int)(screenPosition.Y-size.Y/2),(int)(size.X),(int)(size.Y));
            dealer.Painter.SpriteBatch.Begin();
            dealer.Painter.SpriteBatch.Draw(loadedTexture
                , t
                , null
                , Color.White
                , 0
                , Vector2.Zero
                , SpriteEffects.None
                , 0.5f);
            dealer.Painter.SpriteBatch.End();
        }
    }
}

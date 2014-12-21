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
    /// <summary>
    /// 继承XNA的GAME类，大地无敌2011.3.19
    /// </summary>
    public class ARXNAGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        /// <summary>
        /// GraphicsDeviceManager
        /// </summary>
        protected GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
        }
        SpriteBatch spriteBatch;
        /// <summary>
        /// SpriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public ARXNAGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
        }


    }
}

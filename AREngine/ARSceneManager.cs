using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;

namespace AREngine
{

    /// <summary>
    /// 游戏界面绘出和切换的管理器，2010.6.10-大地无敌-范若余，不可序列化，不属于ARManager
    /// </summary>
    public class ARSceneManager:ARObject,IARDrawable
    {
        List<ARScene> ARSceneList = new List<ARScene>(3);
        ARScene CurrentActiveScene;
        
        ARXNAGame game;
        /// <summary>
        /// 所属Game
        /// </summary>
        public ARXNAGame Game
        {
            get { return game; }
        }

        /// <summary>
        /// 当前界面
        /// </summary>
        public ARScene CurrentScene
        {
            get
            {
                return CurrentActiveScene;
            }
        }

        public ARSceneManager(ARGameBase game)
        {
            this.game = game;
        }
        /// <summary>
        /// 添加新界面
        /// </summary>
        /// <param name="addingscene"></param>
        public void AddScene(ARScene addingscene)
        {
            ARSceneList.Add(addingscene);
            addingscene.Acivated += new EventHandler(SceneActived);
            addingscene.Closed += new EventHandler(SceneClosed);
            foreach (var scene in ARSceneList)
            {
                if (scene.IsActive)
                {
                    return;
                }
            }
            ARSceneList[0].Activate();
        }
        void SceneActived(Object sender, EventArgs e)
        {
           
            ARScene activedScene = (ARScene)sender;
            
            foreach (var scene in ARSceneList)
            {
                if (scene!= activedScene)
                {
                    if (scene.IsActive)
                    {
                        scene.Close();
                    }
                }
            }
            CurrentActiveScene = activedScene;
        }
        void SceneClosed(Object sender, EventArgs e)
        {
            ARScene closedScene = (ARScene)sender;
            //若有激活的界面则返回，若无则激活集合最前面的界面
            foreach (var scene in ARSceneList)
            {
                if (scene.IsActive)
                {
                    return;
                }
            }
            ARSceneList[0].Activate();
        }
        public override void Update(ARUpdateDealer dealer)
        {
            if (CurrentActiveScene != null)
            {

                CurrentActiveScene.Update(dealer);
            }
            base.Update(dealer);
        }
        public override void SUpdate(ARUpdateDealer dealer)
        {
            if (CurrentActiveScene != null)
            {
                CurrentActiveScene.SUpdate(dealer);
            }
            base.SUpdate(dealer);
        }
        public virtual void Draw(ARGraphDealer dealer)
        {
            if (CurrentActiveScene != null)
            {
                CurrentActiveScene.Draw(dealer);
            }
        }

    }
}

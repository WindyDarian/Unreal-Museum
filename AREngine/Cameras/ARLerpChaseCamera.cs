using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using Microsoft.Xna.Framework;

namespace AREngine.Cameras
{
    /// <summary>
    /// 内插平面追逐相机
    /// 大地无敌-范若余 2011/12/5
    /// </summary>
    public class ARLerpChaseCamera:ARBaseCamera,IARUpdateable
    {
        float lerpScale = 3.75f;
        Vector3 cameraOffset;
        /// <summary>
        /// 相机偏移
        /// </summary>
        public Vector3 CameraOffset
        {
            get { return cameraOffset; }
            set { cameraOffset = value; }
        }
        IAR3D target;
        Matrix targetMatrix = Matrix.Identity;
        Matrix currentMatrix = Matrix.Identity;

         void CalculateWorld()
        {
            targetMatrix = Matrix.Identity;
            targetMatrix.Forward = -cameraOffset;
            targetMatrix.Translation = target.Position + cameraOffset;
        }

        public ARLerpChaseCamera(IAR3D obj, Vector3 cameraOffset, Vector3 up, Matrix projection)
            : base(obj.Position + cameraOffset, obj.Position, up, projection)
        {
            this.cameraOffset = cameraOffset;
            target = obj;
            CalculateWorld();
            currentMatrix = targetMatrix;
        }

        public void Update(ARUpdateDealer dealer)
        {
            CalculateWorld();
            currentMatrix = Matrix.Lerp(currentMatrix, targetMatrix, MathHelper.Clamp(lerpScale * dealer.ElapsedTime, 0f, 1f));
            base.LookAt = currentMatrix.Forward + currentMatrix.Translation;
            base.Position = currentMatrix.Translation;
            base.Up = currentMatrix.Up;
            UpdateView();
        }

        public void SUpdate(ARUpdateDealer dealer)
        {
            
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;
using AREngine.Physics;
using Microsoft.Xna.Framework;


namespace AREngine.Cameras
{
    /// <summary>
    /// 跟踪相机，由大地无敌-范若余在2011年12月5日从AOD中移植
    /// 未完成
    /// </summary>
    public class ARRotationChaseCamera:ARBaseCamera,IARUpdateable
    {
        #region 内切相机参数
         float aC_scale = 1.0f;
         float aC_minScale = 0.7f;
         float aC_maxScale = 1.6f;
         float aC_scaleTolerance = 0.01f;//误差允许值
         float aC_rotationlerpScale = 3.25f;
         float aC_scaleLerpScale = 1;
        #endregion

        Vector3 cameraOffset;
        Matrix cameraRotation;
        Matrix targetCameraRotation;
        IAR3D target;


        public ARRotationChaseCamera(IAR3D obj, Vector3 cameraOffset, Vector3 up, Matrix projection)
            : base(obj.Position + cameraOffset, obj.Position, up, projection)
        {
            this.target = obj;
            this.cameraOffset = cameraOffset;
            cameraRotation = Matrix.Identity;
        }

        public void Update(ARUpdateDealer dealer)
        {


            float elapsedTime = dealer.ElapsedTime;
            cameraRotation = Matrix.Lerp(cameraRotation, targetCameraRotation, MathHelper.Clamp(aC_rotationlerpScale * elapsedTime, 0, 1));
            if (cameraOffset != Vector3.Zero)
            {

                aC_scale = Vector3.Distance(base.Position, target.Position) / cameraOffset.Length();
            }

            float d = aC_scale - 1;
            if (Math.Abs(d) <= aC_scaleTolerance)
            {
                aC_scale = 1.0f;
            }
            else aC_scale -= d * elapsedTime * aC_scaleLerpScale;
            aC_scale = MathHelper.Clamp(aC_scale, aC_minScale, aC_maxScale);

            Vector3 ps = Vector3.TransformNormal(cameraOffset, cameraRotation);
            ps *= aC_scale;
            base.Position = target.Position + ps;
            base.LookAt = target.Position;
            Up = cameraRotation.Up;
            UpdateView();   
        }

        public void SUpdate(ARUpdateDealer dealer)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

namespace Illusion.KinectSupport
{
    /// <summary>
    /// 2011.11.29 黎健成
    /// 2011.12.27 范若余 修改一下
    /// </summary>
    public class KinectComponent : Microsoft.Xna.Framework.GameComponent
    {
        KinectSensor kinect;

        public Vector3 head = new Vector3();

        public Vector3 Headposition
        {
            get
            {
                return head;
            }
        }

        public float HeadDepth
        {
            get
            {
                return (float)head.Z;
            }
        }

        /// <summary>
        /// Kinect中心相对屏幕中心的位置，需要手动设置，且Kinect的方向需要与屏幕法线在同一个向量平面上，且在使用者左边以90度的夹角面对使用者
        /// </summary>
        /// <returns></returns>
        public static Vector3 KinectOffset = new Vector3(0, 0, 0);//-2.45f, 0.0f, 1.08f);
        public static bool LeftRotateBool = false;
        public static bool RightRotateBool = false;
        public static bool UpsideBool = false;

        public void LeftRotate(Vector3 kinectVector)
        {
            Vector3 head0;
            head0 = head;
            head.X = head0.Z + kinectVector.X;
            head.Y = head0.Y + kinectVector.Y;
            head.Z = -head0.X + kinectVector.Z;
        }

        public void RightRotate(Vector3 kinectVector)
        {
            Vector3 head0;
            head0 = head;
            head.X = -head0.Z + kinectVector.X;
            head.Y = head0.Y + kinectVector.Y;
            head.Z = head0.X + kinectVector.Z;
        }

        public void Translation(Vector3 kinectVector)
        {
            head.X += kinectVector.X;
            head.Y += kinectVector.Y;
            head.Z += kinectVector.Z;
        }

        public void Upside(Vector3 kinectVector)
        {
            Vector3 head0;
            head0 = head;
            head.X = head0.X + kinectVector.X;
            head.Y = -head0.Y + kinectVector.Y;
            head.Z = head0.Z + kinectVector.Z;
        }

        public KinectComponent(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            kinect = KinectSensor.KinectSensors.First();
            if (kinect.Status == KinectStatus.Connected)
            {
                kinect.SkeletonStream.Enable();
                kinect.Start();
                kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
            }
            base.Initialize();
        }

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);
                    var skeleton = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                    if (skeleton != null)
                    {
                        var pos = skeleton.Joints[JointType.Head].Position;
                        head = new Vector3(pos.X, pos.Y, pos.Z);
                        if (LeftRotateBool) LeftRotate(KinectOffset);
                        else if (RightRotateBool) RightRotate(KinectOffset);
                        else if (UpsideBool) Upside(KinectOffset);
                        else Translation(KinectOffset);
                    }
                }
            }
        }

    }
}

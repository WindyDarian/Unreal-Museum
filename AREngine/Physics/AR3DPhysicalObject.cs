using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;
using Microsoft.Xna.Framework;
using AREngine.Graphs.Graphs3D;

namespace AREngine.Physics
{
    /// <summary>
    /// 3D物理物件,有速度 加速度等属性
    /// 创建 大地无敌-范若余 2011/11/3
    /// </summary>
    public class AR3DPhysicalObject : ARModeledObject, IARPhysical
    {

        Vector3 velocity;
        /// <summary>
        /// 速度向量
        /// </summary>
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public float Speed
        {
            get
            {
                return velocity.Length();

            }
        }

        float mass = 1.0f;
        /// <summary>
        /// 质量
        /// </summary>
        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        private Vector3 thrust = Vector3.Zero;


        private Vector3 impulse;





        float rotateSpeed = 90;
        /// <summary>
        /// 旋转速度
        /// </summary>
        public float RotateSpeed
        {
            get { return rotateSpeed; }
            set { rotateSpeed = value; }
        }

        /// <summary>
        /// 正在旋转的方向（相对）
        /// </summary>
        private Vector3 rotationTarget;






        public override void Update(ARUpdateDealer dealer)
        {

            #region 移动
            {//velocity += acceleration * dealer.ElapsedTime;
                //Position += velocity * dealer.ElapsedTime;
                if (dealer.ElapsedTime != 0)
                    thrust += impulse / dealer.ElapsedTime;
                if (Mass != 0)
                {
                    Vector3 acceleration = thrust / Mass;//计算加速度
                    Velocity += acceleration * dealer.ElapsedTime;


                    Position += Velocity * dealer.ElapsedTime;
                }

                impulse = Vector3.Zero;
                thrust = Vector3.Zero;
                
            }
            #endregion

            #region 清除速度冗余
            if (velocity.LengthSquared()<0.000001f)
            {
                velocity = Vector3.Zero;
            }
            #endregion


            #region 旋转

            if (rotationTarget != Vector3.Forward && rotationTarget != Vector3.Zero)
            {

                Vector3 s;
                Vector3 rt = rotationTarget;
                float m = MathHelper.ToRadians(RotateSpeed * dealer.ElapsedTime);
                if (rt == Vector3.Backward)
                {
                    rt = Vector3.Right;
                }


                float a = (float)Math.Acos(Vector3.Dot(rt, Vector3.Forward));
                if (m >= a)
                {
                    s = rt;

                }
                else
                {
                    float b = (MathHelper.Pi - a) / 2;
                    float c = (MathHelper.Pi - m - b);

                    float h = -(float)(Math.Sin(b) / Math.Sin(c) * Math.Cos(m));
                    //2011/11/5注释 大地无敌忘记以前为什么这样算的了，从AOD里搬来的

                    //s = Vector3.Normalize((Vector3.Forward + (h + 1) / (rt.Z - h) * rt) / (1 + (h + 1) / (rt.Z - h)));

                    s = Vector3.Normalize(Vector3.Lerp(Vector3.Forward, rt, (h + 1) / (rt.Z + 1)));
                }
                if (s != Vector3.Forward)
                {

                    Matrix rr = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(s, Vector3.Forward)), -(float)Math.Acos(Vector3.Dot(Vector3.Forward, s)));
                    Rotation = rr * Rotation;
                }

                rotationTarget = Vector3.Zero;
            }
            #endregion

            base.Update(dealer);
        }
        

        /// <summary>
        /// 施加一个推力，作用时间为一桢
        /// </summary>
        /// <param name="th"></param>
        public void GetThrust(Vector3 th)
        {
            this.thrust += th;
        }
        /// <summary>
        /// 施加一个瞬时冲量
        /// </summary>
        /// <param name="impulse"></param>
        public void GetImpulse(Vector3 impulse)
        {
            
            this.impulse += impulse;
        }



        /// <summary>
        /// 在下一桢里旋转
        /// </summary>
        /// <param name="target">目标点</param>
        public void RotateFrame(Vector3 target)
        {
            if (target != Position)
            {
                rotationTarget = Vector3.Normalize(Vector3.Transform(target, Matrix.Invert(World)));
            }
        }
        

        /// <summary>
        /// 在下一帧里旋转 向量是相对物件的旋转
        /// </summary>
        /// <param name="RelatedTarget"></param>
        public void RotateFrameRelated(Vector3 relatedTarget)
        {
            if (relatedTarget != Vector3.Zero)
            {
                rotationTarget = Vector3.Normalize(relatedTarget);
            }

        }

        

        /// <summary>
        /// 计算并施予一帧内通过功率的受到的推力
        /// 只能执行一次
        /// </summary>
        protected void CalculatePower(Vector3 direction, float maxPower, float maxForce, float elapsedTime)
        {
            if (Vector3.Dot(Velocity, direction * maxForce) < maxPower)
            {
                GetThrust(direction * maxForce);
            }
            else if (Vector3.Dot(Velocity, direction) != 0)
            {
                GetThrust(maxPower / Vector3.Dot(Velocity, direction) * direction);
            }
        }
        /// <summary>
        /// 计算并施予一帧内通过功率的受到的摩擦力
        /// 只能执行一次，一般放在最后执行
        /// </summary>
        protected void CalculateFriction(float frictionForce,float elapsedTime)
        {
            #region 计算摩擦力

            if (Velocity.Length() != 0)
            {
                Vector3 frictionDirection = Vector3.Zero;
                Vector3 friction = Vector3.Zero;

                if (Velocity.Length() * Mass <= frictionForce * elapsedTime)
                {
                    frictionDirection = (Vector3.Normalize(Velocity)) * (-1);
                    friction = (Velocity.Length() * Mass)/elapsedTime * frictionDirection;

                }
                else
                {
                    frictionDirection = (Vector3.Normalize(Velocity)) * (-1);
                    friction = frictionForce * frictionDirection;

                }
                GetThrust(friction);

            }
            #endregion

        }
       
    }
}

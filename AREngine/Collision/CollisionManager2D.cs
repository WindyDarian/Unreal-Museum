using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Graphs;


namespace AREngine.Collision
{
    /// <summary>
    /// 2D碰撞检测
    /// </summary>
    public static class CollisionManager2D
    {
        
        ///// <summary>
        ///// 处理弹药和单位的碰撞，返回碰撞点
        ///// </summary>
        ///// <param name="unit"></param>
        ///// <param name="bullet"></param>
        ///// <returns></returns>
        //public static Vector3? IsCollided(VioableUnit unit, Bullet bullet)
        //{
        //    if (Vector3.Distance(unit.Position, bullet.position) <= GameConsts.BoundingDistance)
        //    {

        //        if (bullet.position != bullet.positionl)
        //        {

        //            Ray ray1;
        //            Ray ray2;
        //            if (bullet.weapon.isInstant == false)
        //            {
        //                ray1 = new Ray(bullet.positionl, Vector3.Normalize(bullet.position - bullet.positionl));
        //                ray2 = new Ray(bullet.position, Vector3.Normalize(bullet.positionl - bullet.position));

        //            }
        //            else
        //            {
        //                Vector3 v1 = bullet.position;
        //                Vector3 v2 = bullet.position + Vector3.Normalize(bullet.velocity) * bullet.Range;

        //                ray1 = new Ray(v1, Vector3.Normalize(v2 - v1));
        //                ray2 = new Ray(v2, Vector3.Normalize(v1 - v2));
        //            }


        //            if (unit.Model.TransformedMajorSphere.Intersects(ray1) != null && unit.Model.TransformedMajorSphere.Intersects(ray2) != null)
        //            {
        //                foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
        //                {
        //                    if (k.Intersects(ray1) != null && k.Intersects(ray2) != null)
        //                    {

        //                        return k.GetCollisionPoint(ray1);
        //                    }
        //                }
        //            }


        //        }
        //        return null;
        //    }
        //    return null;
        //}
    }
}

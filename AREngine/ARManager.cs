using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AREngine.Base;
using AREngine.Graphs;

namespace AREngine
{
    [Serializable]
    /// <summary>
    /// AR物件管理器,大地无敌2011.3.19 
    /// 改进于2011.10.11
    /// </summary>
    /// <typeparam name="T">该ARManager管理的物件类型</typeparam>
    public class ARManager<T>:IARUpdateable,IARDrawable,IARInputSupport,IEnumerable<T>
    {

        /// <summary>
        /// 成员列表
        /// </summary>
        public List<T> MemberList
        {
            get
            {
                return memberList;
            }
        }

        T latestAddedMember;
        /// <summary>
        /// 最后加入的成员
        /// </summary>
        public T LatestAddedMember
        {
            get { return latestAddedMember; }
            set { latestAddedMember = value; }
        }

        private List<T> removingList = new List<T>(10);//待移除的项
        private List<T> addingList = new List<T>(10);//待添加的项
        private List<T> memberList;

        public ARManager()
        {
            memberList = new List<T>();
            //CheckT();
            
        }
        public ARManager(int n)
        {
            memberList = new List<T>(n);
            //CheckT();
        }
        //void CheckT()
        //{
        //     //如果T不是派生于ARObject
        //    if (!(typeof(T).IsSubclassOf(typeof(ARObject))))
        //    {
        //        throw new ApplicationException("T不是ARObject");
        //    }
        //}

        /// <summary>
        /// 添加某一物，将在下一次更新时自动加入
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T item)
        {
            addingList.Add(item);
            latestAddedMember = item;
        }



        public void Update(ARUpdateDealer dealer)
        {
            foreach (T o in addingList)
            {
                memberList.Add(o);
            }
            addingList.Clear();

            foreach (T o in memberList)
            {

                if (IsItemRemoving(o))
                {
                    continue;
                }

                
                if (o is IARUpdateable)
                {
                    ((IARUpdateable)o).Update(dealer);
                }
            }
        }
        public void SUpdate(ARUpdateDealer dealer)
        {
            foreach (T o in memberList)
            {

                    if (!IsItemRemoving(o))
                    {
                        if (o is IARUpdateable)
                        {
                            ((IARUpdateable)o).SUpdate(dealer);
                        }
                    }
                    else
                    {
                        removingList.Add(o);
                    }
                

            }
            foreach (T o in removingList)
            {
                memberList.Remove(o);
            }
            removingList.Clear();
        }

        public virtual void Draw(ARGraphDealer dealer)
        {
            foreach (T o in memberList)
            {

                if (!IsItemRemoving(o))
                {
                    if (o is IARDrawable)
                    {

                        ((IARDrawable)o).Draw(dealer);
                    }
                }
            }
        }

        bool IsItemRemoving(T item)
        {
            if (item is IARRemovable)
            {
                return ((IARRemovable)item).Removing;
            }
            else return false;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return memberList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return memberList.GetEnumerator();
        }
    }
}

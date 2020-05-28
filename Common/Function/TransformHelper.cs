using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class TransformHelper
    {
        /// <summary>
        /// 以 tf 为核心， 更具名字向下查找满足条件的 对象。
        /// </summary>
        /// <param name="tf">核心</param>
        /// <param name="name">对象名字</param>
        /// <returns>对象</returns>
        public static Transform FindInChild(this Transform tf, string name)
        {
            Transform target = tf.Find(name);


            if (target == null)
            {
                if (tf.childCount == 0)
                {
                    return null;
                }

                for (int i = 0; i < tf.childCount; i++)
                {
                    if (target != null)
                    {
                        return target;
                    }

                    Transform tfs = tf.GetChild(i);
                    if (tfs.name == name)
                    {
                        return tfs;
                    }
                    else
                    {
                        target = FindInChild(tfs, name);
                    }
                }

            }

            return target;

        }

        /// <summary>
        /// 以parent为核心，  依次获取其每一个子对象 ，不包括孙对象
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="action">对每一个子对象的操作</param>
        public static void GetPerChild(this Transform parent, Action<Transform> action)
        {
            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                action(child);
            }
        }

        /// <summary>
        /// Get all childen from parent,which do not contain parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Transform[] GetAllChild(this Transform parent)
        {
            List<Transform> cache = new List<Transform>();
            for (int i = 0; i < parent.childCount; i++)
            {
                cache.Add(parent.GetChild(i));
            }

            return cache.ToArray();

        }

        /// <summary>
        /// 计算两个Tranform 在 三维空间中的夹角 ， 以 主物体的正前方为参照 返回 0 到 180 之间的一个角度
        /// </summary>
        /// <param name="originalTf">主物体</param>
        /// <param name="targetTf">目标物体</param>
        /// <returns></returns>
        public static float AngleCalculate(Transform originalTf, Transform targetTf)
        {
            Vector3 one = originalTf.forward.normalized;
            Vector3 tow = (targetTf.position - originalTf.position).normalized;
            float angle = Vector3.Angle(one, tow);
            return angle;

        }

        /// <summary>
        /// 遍历给定对象下的所有对象
        /// </summary>
        /// <param name="predecessor">给定的对象</param>
        /// <param name="action">对子集的行为</param>
        public static void FindinDescendant(this Transform predecessor, Action<Transform> action)
        {

            for (int i = 0; i < predecessor.childCount; i++)
            {
                Transform curr = predecessor.GetChild(i);
                action(curr);
                if (curr.childCount <= 0) continue;
                FindinDescendant(curr, action);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public static class ArrayHelper
    {
        /*
         * 查找满足条件的当个对象
            获取最大值
            对象数组升序排列
            查找满足条件的所有对象

            筛选目标

            Enemy[] => GameObject[]
            Enemy[] =>Fsm[]	;
          * 
          */

        /// <summary>
        /// 在对象数组中根据指定条件查找单个对象
        /// </summary>
        /// <typeparam name="T">对象数组类型</typeparam>
        /// <param name="array">对象数组</param>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            if (array == null || array.Length == 0) return default(T);

            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    return array[i];
                }
            }

            return default(T);

        }

        /// <summary>
        /// 按指定条件查找指定类型数组的最大值
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <typeparam name="X">方法将通过X类型来对数组元素进行比较</typeparam>
        /// <param name="array">要比较的数组</param>
        /// <param name="condition">按这个条件比较</param>
        /// <returns></returns>
        public static T GetMax<T, X>(this T[] array, Func<T, X> condition) where X : IComparable
        {
            T max = array[0];
            for (int i = 1; i < array.Length; i++) // 为了不重复和自己比 从1开始
            {
                if (condition(max).CompareTo(condition(array[i])) < 0)
                {
                    max = array[i];
                }
            }

            return max;

        }

        /// <summary>
        /// 将数组按指定条件升序排列
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <typeparam name="X">返回值类型</typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void SortUp<T, X>(this T[] array, Func<T, X> condition) where X : IComparable
        {
            for (int j = 1; j < array.Length; j++)
            {

                for (int i = 0; i < array.Length - j; i++)
                {
                    if (condition(array[i]).CompareTo(condition(array[i + 1])) < 0)
                    {
                        T angle = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = angle;
                    }
                }
            }

        }

        /// <summary>
        /// 查找数组中满足条件的所有项，返回值为满足条件的项的集合
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">条件</param>
        /// <returns>满足条件的项的集合</returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> agents = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    agents.Add(array[i]);

                }
            }

            return agents.ToArray();
        }

        /// <summary>
        /// Unity脚本类型转换，将一种类型的数组转化为另一种类型的数组
        /// </summary>
        /// <typeparam name="T">原类型</typeparam>
        /// <typeparam name="X">目标类型</typeparam>
        /// <param name="array">原类型数组</param>
        /// <param name="type">目标类型</param>
        /// <returns>转化结果</returns>
        public static X[] TypeChange<T, X>(this T[] array, Func<T, X> condition) where T : MonoBehaviour where X : MonoBehaviour
        {
            X[] agent = new X[array.Length];

            for (int i = 0; i < agent.Length; i++)
            {
                agent[i] = condition(array[i]);
            }

            return agent;



        }


        /// <summary>
        /// 打印一个二维数组
        /// </summary>
        /// <param name="doubleArray"></param>
        /// <returns></returns>
        public static string PrintDiArray(int[,] doubleArray)
        {
            string resoult = "";
            for (int i = 0; i < doubleArray.GetLength(0); i++)
            {
                for (int j = 0; j < doubleArray.GetLength(1); j++)
                {
                    resoult += doubleArray[i, j]+" ";
                }

                resoult += "/n";

            }

            return resoult;
        }

        /// <summary>
        /// 数组转列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<T> ArrayToList<T>(this T[] array)
        {
            List<T> LT = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                LT.Add(array[i]);
            }

            return LT;
        }

        /// <summary>
        /// 列表转数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] ListToArray<T>(this List<T> list)
        {
            T[] array = new T[list.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = list[i];
            }

            return array;
        }

    }
}
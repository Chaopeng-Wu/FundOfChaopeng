using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chaopeng.QiTianXuan.UIFrame
{
    public class UIManage : MonoSingleton<UIManage>
    {
        /*
         * 提供 统一的 UI管理功能
         * 查找 ，禁用 ，
         */
        UIWindow[] UIWindows;

        Dictionary<Type, UIWindow> cache;
        private void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<Type, UIWindow>();
            UIWindowStatusInit();

        }

        private void UIWindowStatusInit()
        {
            UIWindows = FindObjectsOfType<UIWindow>();
            for (int i = 0; i < UIWindows.Length; i++)
            {
                cache.Add(UIWindows[i].GetType(), UIWindows[i]);           
            }
        }

        public T GetWindow<T>() where T : UIWindow
        {
            Type type = typeof(T);
            if (!cache.ContainsKey(type))
            {
                return null;
            }
            return cache[type] as T;
        }

        public void OperateAllItem(Action<UIWindow> action)
        {
            foreach (var item in cache)
            {
                action(item.Value); // 可能会报错， 因为 foreach 中的 值是不允许操作的。
            }
        }
    }
}

using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chaopeng.QiTianXuan.UIFrame
{
    [RequireComponent(typeof(CanvasGroup))]
    /// <summary>
    /// UIWindow base
    /// </summary>
    public abstract class UIWindow : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private static Dictionary<string, UIEventListener> cache;
        //private VRTK_UICanvas vrtk_UICanvas;



        protected virtual void Awake()
        {
            cache = new Dictionary<string, UIEventListener>();
            canvasGroup = GetComponent<CanvasGroup>();
            InitUIEvent();
            InitComponent();

        }


        private void SetVisibleImmediate(bool status)
        {
            canvasGroup.alpha = status ? 1 : 0;
            canvasGroup.blocksRaycasts = status;
            canvasGroup.interactable = status;

            // vrtk_UICanvas.enabled = status;


        }
        private IEnumerator SetVisibleDelay(bool status, float time)
        {

            yield return new WaitForSeconds(time);
            if (status)
            {
                OnWindowActive();
            }
            else
            {
                OnWindowDisactive();
            }
            SetVisibleImmediate(status);


        }

        /// <summary>
        /// 设置UI显隐
        /// </summary>
        /// <param name="status">true为显示</param>
        /// <param name="time">延迟调用的时间</param>
        public void SetVisible(bool status, float time = 0)
        {
            StartCoroutine(SetVisibleDelay(status, time));
        }

        public void SetVisible(float time = 0)
        {
            bool status = gameObject.activeInHierarchy;
            StartCoroutine(SetVisibleDelay(!status, time));
        }

        /// <summary>
        /// 为窗口下的所有Button 提供便捷的事件注册 
        /// </summary>
        /// <param name="ButtonName">按钮的名字</param>
        /// <returns></returns>
        public UIEventListener AddListener(string ButtonName)
        {
            Transform tfs = transform.FindInChild(ButtonName);
            if (tfs == null)
            {
                return null;
            }

            UIEventListener uie = tfs.GetComponent<UIEventListener>();
            if (uie == null)
            {
                uie = tfs.gameObject.AddComponent<UIEventListener>();
            }
            if (!cache.ContainsKey(ButtonName))
                cache.Add(ButtonName, uie);

            return cache[ButtonName];



        }

        /// <summary>
        /// 激活指定窗口并关闭自身窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ActiveWindowWithDisactiveself<T>() where T : UIWindow
        {
            UIWindow window = UIManage.Instance.GetWindow<T>();
            SetVisible(false);
            window.SetVisible(true);
            return window as T;
        }

        protected virtual void InitUIEvent() { }
        protected virtual void InitComponent()
        {
            //    vrtk_UICanvas = GetComponent<VRTK_UICanvas>();
        }
        public virtual void OnWindowActive() { }
        public virtual void OnWindowDisactive() { }

    }
}

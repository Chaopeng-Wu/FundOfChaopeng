using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 倘若使用对象池时发现问题，请参阅注解
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        #region 注解
        /*
         * 注解：
         * 现在的对象池在重新激活对象后没有帮助初始化数据。 只重置了位置， 而 未来可能还需要
         * 重置血量啊 ， 状态啊 什么的， 建议在需要做这些重置的类继承 IResetable接口 实现Reset 方法， 将要重置的数据放入其中
         * 然后在此类中重新激活对象处调用
         * 
         */
        #endregion

      

        public GameObject CreatGameObject(string type, GameObject prefab, Vector3 pos, Quaternion rotation = default(Quaternion))
        {
            if (prefab == null) return null;
            if (!pool.ContainsKey(type))
            {
                pool.Add(type, new List<GameObject>());
            }

            GameObject obj = pool[type].Find(e => e.gameObject.activeSelf == false);
            if (obj == null)
            {
                obj = Instantiate(prefab);
            }

            #region 配置游戏对象
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rotation;
            #endregion

            pool[type].Add(obj);
            return obj;
        }

        /// <summary>
        /// 回收游戏对象
        /// </summary>
        /// <param name="object"></param>
        /// <param name="delay"></param>
        public void CollectObject(GameObject @object, float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(@object, delay));
        }


        private void CollectObjectImmediatly(GameObject @object)
        {
            @object.SetActive(false);
        }

        private IEnumerator CollectObjectDelay(GameObject @object, float delay)
        {
            yield return new WaitForSeconds(delay);
            CollectObjectImmediatly(@object);
        }
        private Dictionary<string, List<GameObject>> pool;

        protected override void Init()
        {
            base.Init();
            pool = new Dictionary<string, List<GameObject>>();
        }
    }
}

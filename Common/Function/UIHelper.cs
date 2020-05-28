using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Common
{
    public class UIHelper
    {
        public List<GameObject> showObjsPrefab; // 展品的预制体

        private static IEnumerator UI_Move_Position_constantSpeed_(Transform rect, Vector3 from, Vector3 to, float time = 1)
        {
            rect.position = from;

            float x_difference_value;
            float y_difference_value;
            float z_difference_value;

            x_difference_value = to.x - rect.position.x; // 差值 ， 用目标位置减去当前位置
            y_difference_value = to.y - rect.position.y;
            z_difference_value = to.z - rect.position.z;



            float step_x = Mathf.Abs(x_difference_value) / time * Time.deltaTime; // 这里用差值的绝对值除deltaTime 得到每秒中走完全程的速度， 在除时间得到比例比如除2相当于每步的距离为一半，则到达的时间为两倍；
            float step_y = Mathf.Abs(y_difference_value) / time * Time.deltaTime;
            float step_z = Mathf.Abs(z_difference_value) / time * Time.deltaTime;

            //这个JB 走路方法老是检测不到。！ 因为太快le ,加大下面这个值可以。。
            while (Vector3.Distance(rect.position, to) > 50)
            {
                if (x_difference_value > 0)
                {
                    rect.position = new Vector3(rect.position.x + step_x, rect.position.y, rect.position.z);


                }
                else if (x_difference_value <= 0)
                {

                    rect.position = new Vector3(rect.position.x - step_x, rect.position.y, rect.position.z);
                }
                if (y_difference_value > 0)
                {
                    rect.position = new Vector3(rect.position.x, rect.position.y + step_y, rect.position.z);
                }
                else if (y_difference_value <= 0)
                {
                    rect.position = new Vector3(rect.position.x, rect.position.y - step_y, rect.position.z);
                }
                if (z_difference_value > 0)
                {
                    rect.position = new Vector3(rect.position.x, rect.position.y, rect.position.z + step_z);
                }
                else if (z_difference_value <= 0)
                {
                    rect.position = new Vector3(rect.position.x, rect.position.y, rect.position.z - step_z);
                }
                //float c = Mathf.Sign(x_difference_value * x_difference_value + y_difference_value * y_difference_value + z_difference_value * z_difference_value); //起始点到目标点的距离         

                if (Vector3.Distance(rect.position, to) > 1050)
                    rect.position = to;
                yield return null;
            }

            rect.position = to; // 过了while 说明 接近了；

        }

        private static IEnumerator ChangeAlpha_constantSpeed_(Image image, float from, float to, float time = 1)
        {
            if (from > 1)
                from = 1;
            if (from < 0)
                from = 0;
            if (to > 1)
                to = 1;
            if (to < 0)
                to = 0;

            Color colorAgent = image.color;
            colorAgent.a = from;
            image.color = colorAgent;


            if (from > to)
            {
                while (colorAgent.a > to)
                {
                    colorAgent.a -= (from - to) * Time.deltaTime / time;
                    image.color = colorAgent;
                    yield return null;
                }
            }
            else if (from < to)
            {
                while (colorAgent.a < to)
                {
                    colorAgent.a += (to - from) * Time.deltaTime / time;
                    image.color = colorAgent;
                    yield return null;
                }
            }

            colorAgent.a = to;
            image.color = colorAgent;

        }

        /// <summary>
        /// 提供匀速的UI 运动功能
        /// </summary>
        /// <param name="caller">调用者，一般为继承Mono的自身，用它来开启协程</param>
        /// <param name="rect">UI的Trans</param>
        /// <param name="from">从</param>
        /// <param name="to">到</param>
        /// <param name="time">时长</param>
        /// <returns></returns>
        public static void UI_Move_Position_constantSpeed<T>(T caller, Transform rect, Vector3 from, Vector3 to, float time = 1) where T : MonoBehaviour
        {
            caller.StartCoroutine(UI_Move_Position_constantSpeed_(rect, from, to, time));
        }

        /// <summary>
        /// 提供Image透明度匀速变换功能
        /// </summary>
        /// <param name="caller">调用者，一般为继承Mono的自身，用它来开启协程</param>
        /// <param name="image"></param>
        /// <param name="from">0-1 之间</param>
        /// <param name="to">0-1 之间</param>
        /// <param name="time">变换总时长</param>
        /// <returns></returns>
        public static void ChangeAlpha_constantSpeed<T>(T caller, Image image, float from, float to, float time = 1) where T : MonoBehaviour
        {
            caller.StartCoroutine(ChangeAlpha_constantSpeed_(image, from, to, time));
        }

        /// <summary>
        /// 按特定的位置角度展示一个目标，
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="ShowObj">要展示的信息</param>
        /// <param name="distance">展示距离</param>
        /// <param name="offeiceAngle">每个展示物的角度差</param>
        /// <param name="showHight">展示物的高度</param>
        private static GameObject ShowInfomations(Transform target, GameObject ShowObj, float distance, float offeiceAngle, float showHight)
        {
            #region TestArea

            #endregion TestArea

            Vector3 playerPosition = target.position;// 获取玩家的真实位置
            playerPosition.y = showHight; // ShowObj 的显示高度

            Quaternion q = Quaternion.Euler(0, target.transform.localRotation.eulerAngles.y, 0); // 获得旋转角度

            GameObject obj = GameObject.Instantiate(ShowObj, playerPosition, q); //创建物体 并将位置设在目标物体点上 并让物体与目标物体旋转一致
    

            obj.transform.Rotate(Vector3.up, offeiceAngle); // 旋转偏移

            obj.transform.position += obj.transform.forward * (distance);

            return obj;


        }

        /*
         *        给定的 showGameObject 最好是预制件， 每次执行方法将创建预制件实例
         *        This method is not liable for destroying instance instantiated,When you call the method again,which will instantiate instance again
         */
        /// <summary>
        /// 在三维世界中展示多个物品给目标看，返回创建的展示物实例
        /// </summary>
        /// <typeparam name="T">被展示物品的类型</typeparam>
        /// <param name="showObjectList">被展示的物品组（预制件）</param>
        /// <param name="count">一个水平线上要展示的物品数量</param>
        /// <param name="target">目标，观看者</param>
        /// <param name="distance">展示距离</param>
        ///  <param name="Height">如果展示行数大于一，请设置行高</param>
        public static List<GameObject> ShowInfomationMultiple(List<GameObject> showObjectList, int count, Transform target, float distance, bool AngleLock = false, float MaxAngle = 360, float Height = 2) 
        {
            List<GameObject> cache = new List<GameObject>();
            float deltaAngle = 360 / count; // 获得每个被展示物之间的夹角
            if (AngleLock)
            {
                if (deltaAngle > MaxAngle)
                    deltaAngle = MaxAngle;
            }


            int parameter; // 用来给  i  归零
            int a = 0;
            int factor = 0;
            for (int i = 0; i < showObjectList.Count; i++)
            {
                parameter = i % count;
                if (parameter == 0)
                    a = 0;

                if (parameter < 1)
                    factor = parameter;
                else if (parameter >= 1)
                {
                    if (parameter % 2 == 0)
                        factor = -(parameter / 2);
                    else
                    {
                        factor = parameter - a;
                        a++;
                    }
                }

                if (i < count) // 当 i < count时 ， 显示高度为 目标高度
                    cache.Add(ShowInfomations(target, showObjectList[i] as GameObject, distance, factor * deltaAngle, target.position.y));
                else if (i >= count && i < 2 * count) // 当 i < 两倍的count 时， 高度为 两倍的目标高度
                {
                    cache.Add(ShowInfomations(target, showObjectList[i] as GameObject, distance, factor * deltaAngle, target.position.y + Height));
                }
                else if (i >= 2 * count && i < 3 * count) // 当i < 三倍的 count 时， 高度为三倍的目标高度。
                {
                    cache.Add(ShowInfomations(target, showObjectList[i] as GameObject, distance, factor * deltaAngle, target.position.y + 2 * Height));
                }
                else if (i >= 3 * count && i < 4 * count)
                {
                    cache.Add(ShowInfomations(target, showObjectList[i] as GameObject, distance, factor * deltaAngle, target.position.y + -1 * Height));
                }
                else if (i >= 4 * count && i < 5 * count)
                {
                    cache.Add(ShowInfomations(target, showObjectList[i] as GameObject, distance, factor * deltaAngle, target.position.y + 3 * Height));
                }
            }

            return cache;
        }

        public static List<GameObject> ShowInfomationMultiple(GameObject[] showObjectArray, int count, Transform target, float distance, bool AngleLock = false, float MaxAngle = 360, float Height = 2)
        {
            List<GameObject> showObjects = new List<GameObject>();
            showObjects = showObjectArray.ArrayToList();

            List<GameObject> cache = new List<GameObject>();
            float deltaAngle = 360 / count; // 获得每个被展示物之间的夹角
            if (AngleLock)
            {
                if (deltaAngle > MaxAngle)
                    deltaAngle = MaxAngle;
            }


            int parameter; // 用来给  i  归零
            int a = 0;
            int factor = 0;
            for (int i = 0; i < showObjects.Count; i++)
            {
                parameter = i % count;
                if (parameter == 0)
                    a = 0;

                if (parameter < 1)
                    factor = parameter;
                else if (parameter >= 1)
                {
                    if (parameter % 2 == 0)
                        factor = -(parameter / 2);
                    else
                    {
                        factor = parameter - a;
                        a++;
                    }
                }

                if (i < count) // 当 i < count时 ， 显示高度为 目标高度
                    cache.Add(ShowInfomations(target, showObjects[i] as GameObject, distance, factor * deltaAngle, target.position.y));
                else if (i >= count && i < 2 * count) // 当 i < 两倍的count 时， 高度为 两倍的目标高度
                {
                    cache.Add(ShowInfomations(target, showObjects[i] as GameObject, distance, factor * deltaAngle, target.position.y + Height));
                }
                else if (i >= 2 * count && i < 3 * count) // 当i < 三倍的 count 时， 高度为三倍的目标高度。
                {
                    cache.Add(ShowInfomations(target, showObjects[i] as GameObject, distance, factor * deltaAngle, target.position.y + 2 * Height));
                }
                else if (i >= 3 * count && i < 4 * count)
                {
                    cache.Add(ShowInfomations(target, showObjects[i] as GameObject, distance, factor * deltaAngle, target.position.y + -1 * Height));
                }
                else if (i >= 4 * count && i < 5 * count)
                {
                    cache.Add(ShowInfomations(target, showObjects[i] as GameObject, distance, factor * deltaAngle, target.position.y + 3 * Height));
                }
            }

            return cache;
        }




    }
}
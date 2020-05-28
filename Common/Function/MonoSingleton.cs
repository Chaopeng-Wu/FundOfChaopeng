using UnityEngine;

namespace Common
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /*
         *有一个类需要何时何地都能调用，不需要每次都找，
         */
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject g  = new GameObject();
                        instance =  g.AddComponent<T>();
                        g.name = instance.GetType().Name;
                    }
                    instance.Init();
                }

                return instance;
            }

            private set { }

        }

     

        protected virtual void Init()
        {

        }


        


    }
}

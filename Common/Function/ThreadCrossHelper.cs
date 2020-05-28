using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{

    public delegate void TreadAction();
    public delegate void TreadAction<in T>(T @object);

    public class ThreadCrossHelper : MonoBehaviour
    {
        private static TreadAction treadAction;

        public static bool ExecuteInMainThread(TreadAction treadAction)
        {
            if (treadAction == null)
            {
                return false;
            }

            ThreadCrossHelper.treadAction += treadAction;
            return true;
        }

        private void Update()
        {
            if (treadAction != null)
            {
                treadAction.Invoke();
                treadAction = null;
            }
        }
    }
}
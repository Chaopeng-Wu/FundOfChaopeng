using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// 动画事件行为类
	/// </summary>
	public class AnimationEventBehaviour : MonoBehaviour 
	{
        /*
         策划：在Unity编译器中，添加事件（取消动画、攻击）。
         程序：播放动画，注册攻击事件。
        */

        private Animator anim;
        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        //1. 取消动画
        //当动画播放完毕或，由Unity引擎调用
        private void OnCancelAnim(string animParam)
        {
            anim.SetBool(animParam, false);
        }

        //2. 提供动画事件
        public event Action attackHandler;

        //当动画播放到某一时刻，由Unity引擎调用
        private void OnAttack()
        {
            if (attackHandler != null)
                attackHandler();
        }

    }
}
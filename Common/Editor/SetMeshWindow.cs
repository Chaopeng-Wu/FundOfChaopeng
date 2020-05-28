using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Common;

/// <summary>
/// 本脚本创建一个窗口，然后将拖上来的物体的所有子物体设置为无阴影。
/// </summary>
public class SetMeshWindow : EditorWindow
{
   [MenuItem("自定义窗口/设置无阴影")]
    public static void SetMesh()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 500, 700);
        SetMeshWindow window = (SetMeshWindow)GetWindowWithRect(typeof(SetMeshWindow), rect,true,"！！！");
        window.Show();

    }

    GameObject sele;
    private void OnGUI()
    {
        //--------------------设置字体样式---------------------------
        GUIStyle MainTitle = new GUIStyle(EditorStyles.helpBox);
        MainTitle.fontSize = 22;
        MainTitle.fontStyle = FontStyle.Normal;
        MainTitle.alignment = TextAnchor.MiddleCenter;
        //--------------------设置字体样式---------------------------


        //最后一个参数在GUILayoutOption 中 可以设置当前组件的相关属性，比如占用高度
        GUILayout.Box("阴影接受开关", MainTitle, new GUILayoutOption[] { GUILayout.Height(30), GUILayout.Width(200) });

        //创建一个可以拖拽物体的
        Rect objRect = GUILayoutUtility.GetRect(0, 30);
        sele = EditorGUI.ObjectField(objRect, sele, typeof(GameObject),true) as GameObject;


        GUIStyle ReightButton = new GUIStyle(EditorStyles.miniButtonRight);
        ReightButton.fontSize = 22;
        ReightButton.fontStyle = FontStyle.Bold;
        bool action  = GUILayout.Button("确定", ReightButton, GUILayout.Height(30));
        if (action)
        {
            List<MeshRenderer> meshs = new List<MeshRenderer>();
            sele.transform.FindinDescendant(t => { if (t.GetComponent<MeshRenderer>() != null) meshs.Add(t.GetComponent<MeshRenderer>()); });
            if (meshs.Count > 0)
            {
                for (int i = 0; i < meshs.Count; i++)
                {
                    meshs[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    meshs[i].receiveShadows = false;
                }
                meshs.Clear();
            }

        }



    }
}

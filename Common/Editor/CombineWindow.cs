using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombineWindow : EditorWindow
{
    static float width = 500;
    static float heigh = 700;

    [MenuItem("自定义窗口/网格合并")]
    public static void Combine_Model()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, width, heigh);
        CombineWindow window = (CombineWindow)GetWindowWithRect(typeof(CombineWindow), rect);
        window.titleContent = new GUIContent("一个平凡的窗口");
        window.Show();
    }
    GameObject collection;
    UnityEngine.Object target;
    bool isMeshCombine;
    bool isMetarialCombine;
    bool perform;
    bool cancel;
    string mas;
    private void OnGUI()
    {
        GUILayoutOption[] standardOption = new GUILayoutOption[] { GUILayout.Width(width / 2), GUILayout.Height(30) };

        GUIStyle title = new GUIStyle(EditorStyles.label);
        title.fontSize = 18;
        title.alignment = TextAnchor.MiddleCenter;
        title.fontStyle = FontStyle.Bold;
        GUILayout.Box("网格合并", title);
        Get_Space(20);

        target = EditorGUILayout.ObjectField("要合并的网格", target, typeof(GameObject), true);

        EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.Width(width / 2) });
        isMeshCombine = EditorGUILayout.Toggle("合并网格", isMeshCombine, new GUILayoutOption[] { GUILayout.Width(width / 2) });
        isMetarialCombine = EditorGUILayout.Toggle("材质合并", isMetarialCombine, new GUILayoutOption[] { GUILayout.Width(width / 2) });
        EditorGUILayout.EndHorizontal();
        string info = "这是一个提示，提示你一些重要的信息，但其实并没有什么卵用";
        EditorGUILayout.HelpBox(info, MessageType.Info, true);
        EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.Width(width / 2) });
        if (GUILayout.Button("确定", standardOption))
            Combine(target);
        if (GUILayout.Button("取消", standardOption))
            Cancel(collection);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox(mas, MessageType.Info);







    }

    private void Cancel(UnityEngine.Object target)
    {
        var Orig = (this.target as GameObject).GetComponentsInChildren(typeof(Transform), true);
        foreach (var item in Orig)
        {
            item.gameObject.SetActive(true);
        }
        if (target == null)
        {
            Debug.Log("It is Inexistent that you went to destroy");
            return;
        }
        DestroyImmediate(target);


    }

    /// <summary>
    /// 网格+材质合并的完美案例
    /// </summary>
    /// <param name="target"></param>
    private void Combine(UnityEngine.Object target)
    {

        GameObject obj = target as GameObject;
        MeshFilter[] filter = obj.GetComponentsInChildren<MeshFilter>(); // 获取所有要合并的网格
        CombineInstance[] combine = new CombineInstance[filter.Length];// 为合并网格创建  合并器。
        Material[] mates = new Material[combine.Length]; //获得所有要合并的材质
        //------------------------这个循环自己看-------------------------
        //在编辑模式下 对网格的操作只能用 .sharedMesh, 如果是运行时可以用 .Mesh
        //如果在编辑模式下用 .Mesh 会报内存溢出
        for (int i = 0; i < combine.Length; i++)
        {
            mates[i] = filter[i].GetComponent<MeshRenderer>().sharedMaterial;
            combine[i].mesh = filter[i].sharedMesh;
            combine[i].transform = filter[i].transform.localToWorldMatrix;
            filter[i].gameObject.SetActive(false);
        }
        //-----------------------------------------------------------------

        collection = new GameObject("Combine", new Type[] { typeof(MeshFilter), typeof(MeshRenderer) }); //创建一个空物体用来承载合并后的网格
        var meshf = collection.GetComponent<MeshFilter>();
        meshf.sharedMesh = new Mesh();
        meshf.sharedMesh.CombineMeshes(combine, false); // 为空物体附加网格，第二个参数很重要
        var render = collection.GetComponent<MeshRenderer>();
        render.sharedMaterials = mates; // 为空物体附加材质

        Debug.Log("完成");



    }

    private void Get_Space(float space)
    {
        Rect rect = GUILayoutUtility.GetRect(0, space);
    }
}

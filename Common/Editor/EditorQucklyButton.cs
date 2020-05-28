using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorQucklyButton 
{
    [MenuItem("Button/SetActive #Q")]
    public static void AAA()
    { 
        GameObject obj = Selection.activeGameObject;
        obj.SetActive(!obj.activeSelf);
    }
}

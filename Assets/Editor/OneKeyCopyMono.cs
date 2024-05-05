using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OneKeyCopyMono 
{
    static Component[] copiedComponents;
    [MenuItem("GameObject/���Ƶ�ǰ�������")]
    static void Copy()
    {
        copiedComponents = Selection.activeGameObject.GetComponents<Component>();
    }
    [MenuItem("GameObject/������Ƶ��������")]
    static void Paste()
    {
        foreach (var targetGameObject in Selection.gameObjects)
        {
            if (!targetGameObject || copiedComponents == null) continue;
            foreach (var copiedComponent in copiedComponents)
            {
                if (!copiedComponent) continue;
                UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponent);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
            }
        }
    }

}

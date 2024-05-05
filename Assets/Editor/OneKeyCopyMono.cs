using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OneKeyCopyMono 
{
    static Component[] copiedComponents;
    [MenuItem("GameObject/复制当前所有组件")]
    static void Copy()
    {
        copiedComponents = Selection.activeGameObject.GetComponents<Component>();
    }
    [MenuItem("GameObject/黏贴复制的所有组件")]
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

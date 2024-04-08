using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class MirrorScriptableObject : ScriptableObject
{
    public MirrorType mirrorType;
    public Sprite icon,sprite;
    public GameObject mirrorPrefab;
    public GameObject mirrorIconPrefab;


}
public enum MirrorType
{
    BaseMirror,
    ReflectMirror,
    LensMirror,
    PrismMirror,
    Clapboard
}

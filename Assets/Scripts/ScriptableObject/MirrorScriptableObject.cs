using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class MirrorScriptableObject : ScriptableObject
{
    public MirrorType mirrorType;

    public GameObject mirrorPrefab;


}
public enum MirrorType
{
    BaseMirror,
    ReflectMirror,
    LensMirror,
    PrismMirror,
    Clapboard
}

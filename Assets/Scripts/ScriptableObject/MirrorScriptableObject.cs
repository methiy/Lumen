using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class MirrorScriptableObject : ScriptableObject
{
    public MirrorType mirrorType;
    public Sprite icon,sprite;
    public GameObject mirrorPrefab;
    public GameObject mirrorIconPrefab;
    public bool isHaveIcon;

}
public enum MirrorType
{
    BaseMirror,
    ReflectMirror,
    LensMirror,
    PrismMirror,
    Clapboard,
    TransmissionInputMirror,
    TransmissionOutputMirror,
    DispersingMirror,
    LensAndReflectMirror,
    ReflectAndClapBoardMirror,
    ClapboardAndLensMirror,
    ReflectAndDispersingMirror
}

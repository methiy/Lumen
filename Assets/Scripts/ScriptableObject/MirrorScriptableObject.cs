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
    DispersingMirror,
    RedLensMirror,
    BlueLensMirror,
    WhiteLensMirror,
    Clapboard,
    TransmissionInputMirror,
    TransmissionOutputMirror,
    RedLensAndReflectMirror,
    BlueLensAndReflectMirror,
    WhiteLensAndReflectMirror,
    ReflectAndClapBoardMirror,
    ClapboardAndRedLensMirror,
    ClapboardAndBlueLensMirror,
    ClapboardAndWhiteLensMirror,
    ReflectAndDispersingMirror,
    LensAndDispersingMirror
}

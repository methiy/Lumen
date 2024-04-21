using System;
using UnityEngine;

public class BaseMirror : MonoBehaviour
{
    protected MainLaser mainLaser;
    [Header("��Ҫ�����������")]
    [SerializeField] protected LayerMask layerMasks;
    private void Awake()
    {
        mainLaser=GetMainLaser();
        mainLaser.UpdateMainLaser();
    }

    private MainLaser GetMainLaser(){
        MainLaser[] allMirrorManager = UnityEngine.Object.FindObjectsOfType<MainLaser>();  
        return allMirrorManager[0];
    }

    public virtual void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color){

    }

    

}
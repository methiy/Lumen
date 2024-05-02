using System;
using UnityEngine;

public class BaseMirror : MonoBehaviour
{
    protected MainLaser mainLaser;
    [Header("需要检测的物体类别")]
    [SerializeField] protected LayerMask layerMasks;
    private void Awake()
    {
        mainLaser=GetMainLaser();
    }
    private MainLaser GetMainLaser(){
        MainLaser[] allMirrorManager = UnityEngine.Object.FindObjectsOfType<MainLaser>();  
        return allMirrorManager[0];
    }

    public virtual void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color){

    }

    

}
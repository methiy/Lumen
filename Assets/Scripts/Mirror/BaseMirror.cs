using System;
using UnityEngine;

public class BaseMirror : MonoBehaviour
{
    protected MainLaser mainLaser;
    [Header("需要检测的物体类别")]
    [SerializeField] protected LayerMask layerMasks;
    [SerializeField] protected LayerMask mirrorLayerMask;
    [SerializeField] protected LayerMask lensLayerMask;
    [SerializeField] protected LayerMask ClapboardLayerMask;

    [SerializeField] protected LayerMask PrismLayerMask;

    

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
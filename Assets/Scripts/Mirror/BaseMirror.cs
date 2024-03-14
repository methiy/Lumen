using UnityEngine;

public class BaseMirror : MonoBehaviour
{

    
    [Header("需要检测的物体类别")]
    [SerializeField] protected LayerMask layerMasks;
    [SerializeField] protected LayerMask mirrorLayerMask;
    [SerializeField] protected LayerMask lensLayerMask;
    [SerializeField] protected LayerMask ClapboardLayerMask;

    [SerializeField] protected LayerMask PrismLayerMask;
    public virtual void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color){

    }

    

}
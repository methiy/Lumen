using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLaser : MonoBehaviour
{
    [Header("需要检测的物体类别")]
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private LayerMask mirrorLayerMask;
    [SerializeField] private LayerMask lensLayerMask;

    //射线
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();

    [SerializeField] private const float MAX_LENGTH = 100.0f;

    private void Start()
    {
        Ray(transform.position,new Vector2(transform.position.x+1,transform.position.y),1,Color.white);
    }

    private void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        Vector2 direction = (endPosition - startPosition).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, MAX_LENGTH, layerMasks);
        
        if (hit.collider!=null && hit.collider.GetComponent<Laser>())
        {

            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, startPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知被击中的物体去发射射线
            hit.collider.GetComponent<Laser>()?.Ray(
                startPosition,
                hit.collider.transform.position,
                index,
                color);
        }
        else
        {
            //没有击中
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1, direction*MAX_LENGTH);
        }
    }

}

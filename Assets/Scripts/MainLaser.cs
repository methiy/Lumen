using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLaser : MonoBehaviour
{
    [Header("需要检测的物体类别")]
    [SerializeField]private LayerMask layerMasks;
    public Action OnChangeMirror;
    //射线
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();

    [SerializeField] private const float MAX_LENGTH = 100.0f;
    private void Start()
    {
        ClearLine();
        Ray(transform.position, new Vector2(transform.position.x + 1, transform.position.y), 1, Color.white);
    }

    public void UpdateMainLaser(){
        OnChangeMirror?.Invoke();
        ClearLine();
        Ray(transform.position, new Vector2(transform.position.x + 1, transform.position.y), 1, Color.white);
        
        OnChangeMirror?.Invoke();
        ClearLine();
        Ray(transform.position, new Vector2(transform.position.x + 1, transform.position.y), 1, Color.white);
    }
    private void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        Vector2 direction = (endPosition - startPosition).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, MAX_LENGTH, layerMasks);

        if (hit.collider!=null && hit.collider.GetComponent<BaseMirror>())
        {
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, startPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知被击中的物体去发射射线
            
            hit.collider.GetComponent<BaseMirror>()?.Ray(
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
            lasersList[index].SetPosition(1, new Vector3(transform.position.x+direction.x*MAX_LENGTH,transform.position.y+direction.y*MAX_LENGTH,0));
        }
        // StartCoroutine(ClearLinePoints());
    }

    private float clearInterval =0.1f;
    private IEnumerator ClearLinePoints()  
    {  
        while (true) // 无限循环，直到停止Coroutine  
        {  
            yield return new WaitForSeconds(clearInterval); // 等待指定的时间间隔  
            ClearLine(); // 清除LineRenderer中的点  
        }  
    }  
  
    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; // 将LineRenderer中的点数量设置为0，从而清除所有点  
            lineRenderer.material.color=Color.white;
        }
    }  
}

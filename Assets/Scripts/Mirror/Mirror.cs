using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mirror : BaseMirror

{
    //射线
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();

    //const
    [Header("常量")]
    // [SerializeField]private const float OFFSET=0.01f;
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    // [SerializeField]private const int MAX_COUNT=10;

    /// <summary>
    /// 起点终点 index（设置为哪个line render出射0123 分别为上右左下） color为光线颜色
    /// </summary>
    /// <param name="originposition"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {

        //他的1 是我的3 他的 0 是我的 2
        index^=2;
        //求出射时 哪个line render发射射线
        index ^= 3;
        //偏移值
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        //出射方向
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        //! 放置出现自己打自己的情况 就是走一个半径
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        // Debug.Log(endPosition+direction*1.1f);
        // if(hit.collider==null)
        //     Debug.Log("NULL");
        // else
        //     Debug.Log(hit.rigidbody.name);
        
        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知被击中的物体去发射射线
            if(hit.collider.gameObject.layer!=ClapboardLayerMask){
                hit.collider.GetComponent<BaseMirror>()?.Ray(
                    endPosition,
                    hit.collider.transform.position,
                    index,
                    color);
            }
        }
        else
        {
            Debug.Log("Don't hit");
            //没有击中
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
        }
        StartCoroutine(ClearLinePoints());
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



    // private void Ray(Vector2 startPosition,Vector2 endPosition){
    //     //发射方向
    //     Vector2 direction = (endPosition - startPosition).normalized;

    //     RaycastHit2D hit=Physics2D.Raycast(startPosition, direction,MAX_LENGTH,layerMasks);
    //     List<Pair> hitList=new List<Pair>();
    //     if(hit.collider!=null)  hitList.Add(new Pair(hit,startPosition));
    //     int count=0;
    //     while(hitList.Count!=0&&++count<MAX_COUNT)
    //     {
    //         List<Pair> newhitList=new List<Pair>();
    //         foreach(Pair hit2D in hitList) {

    //             RaycastHit2D raycastHit2D=hit2D.GetRaycastHit2D();//! 当前击中的点
    //             Vector2 originPosition=hit2D.GetOriginPosition();

    //             hit=ReflectMirror(raycastHit2D,originPosition);
    //             if(hit.collider!=null)  newhitList.Add(new Pair(hit,raycastHit2D.point));
    //             hit=LensMirror(raycastHit2D,originPosition);
    //             if(hit.collider!=null)  newhitList.Add(new Pair(hit,raycastHit2D.point));
    //         }
    //         hitList=newhitList;
    //     }
    // }


    // private RaycastHit2D ReflectMirror(RaycastHit2D raycastHit2D,Vector2 originPosition) {

    //     Vector2 direction = Vector2.Reflect(originPosition,raycastHit2D.normal);

    //     RaycastHit2D hit=Physics2D.Raycast(originPosition, direction,MAX_LENGTH,mirrorLayerMask);

    //     return hit; 
    // }
    // private RaycastHit2D LensMirror(RaycastHit2D raycastHit2D,Vector2 originPosition){
    //     //发射方向
    //     Vector2 direction = (raycastHit2D.point-originPosition).normalized;

    //     RaycastHit2D hit=Physics2D.Raycast(originPosition, direction,MAX_LENGTH,lensLayerMask);

    //     return hit;
    // }
}

// /// <summary>
// /// 记录射线击中的目标和射入起始点
// /// </summary>
// public class Pair
// {
//     RaycastHit2D raycastHit2D;
//     Vector2 originPosition;

//     public Pair(RaycastHit2D raycastHit2D, Vector2 originPosition)
//     {
//         this.raycastHit2D = raycastHit2D;
//         this.originPosition = originPosition;
//     }
//     public RaycastHit2D GetRaycastHit2D()
//     {
//         return raycastHit2D;
//     }
//     public Vector2 GetOriginPosition()
//     {
//         return originPosition;
//     }
// }
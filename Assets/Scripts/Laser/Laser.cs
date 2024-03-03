using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("需要检测的物体类别")]
    [SerializeField] private LayerMask layerMasks;
    [SerializeField] private LayerMask mirrorLayerMask;
    [SerializeField] private LayerMask lensLayerMask;

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
    public void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {

        //求出射时 哪个line render发射射线
        index ^= 3;
        //偏移值
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };
        //出射方向
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        RaycastHit2D hit = Physics2D.Raycast(endPosition, direction, MAX_LENGTH, layerMasks);

        

        if (hit.collider != null && hit.collider.GetComponent<Laser>())
        {
            Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知被击中的物体去发射射线
            hit.collider.GetComponent<Laser>()?.Ray(
                endPosition,
                hit.collider.transform.position,
                index,
                color);
        }
        else
        {
            Debug.Log(this.transform.name + "Dont hit");
            //没有击中
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,direction*MAX_LENGTH);
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
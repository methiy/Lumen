using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LensMirror : BaseMirror
{
    //射线
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();

    //const
    [Header("常量")]
    // [SerializeField]private const float OFFSET=0.01f;
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    // [SerializeField]private const int MAX_COUNT=10;

    /// <summary>
    /// 起点终点 index（�?�置为哪个line render出射0123 分别为上右左下） color为光线�?�色
    /// </summary>
    /// <param name="originposition"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// 

    private void OnEnable()
    {
        mainLaser.OnChangeMirror+=ClearLine;
        mainLaser.UpdateMainLaser();
    }
    private void OnDisable()
    {
        mainLaser.UpdateMainLaser();
        mainLaser.OnChangeMirror-=ClearLine;
    }

    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {

       if((index^curRotation)!=1) return ;

        //偏移�?
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        //出射方向
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        //! 放置出现�?己打�?己的情况 就是走一�?半径
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        
        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知�?击中的物体去发射射线
            hit.collider.GetComponent<BaseMirror>()?.Ray(
                endPosition,
                hit.collider.transform.position,
                index,
                color);
        }
        else
        {
            //没有击中
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
        }
    }


    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; // 将LineRenderer�?的点数量设置�?0，从而清除所有点  
            lineRenderer.material.color=Color.white;
        }
    }    

    /// <summary>
    /// Rotate
    /// </summary>
    private int curRotation=0;

    public bool isRotate;

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && isRotate){
            TryRotateMirror();
        }
    }
    private void TryRotateMirror(){
        
            Vector3 mousePos = Input.mousePosition; // 获取鼠标的屏幕坐�?  
            mousePos.z = Camera.main.nearClipPlane; // 设置z坐标为相机的近�?�剪�?，确保转换到正确�?2D平面  
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos); // 将鼠标的屏幕坐标�?�?为世界坐�?  
              
            // 使用Physics2D.OverlapCircle来�?�测圆形区域内的所有�?�撞�?  
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, 0.2f);  
            foreach (Collider2D collider in colliders)  
            {  
                // 检查相交的物体�?否是当前物体  
                if (collider.gameObject == this.gameObject)  
                {  
                    RotateMirror();
                }  
            }
    }
    private void RotateMirror(){
        
        transform.Rotate(0,0,90);
        curRotation+=1;
        curRotation%=4;
        mainLaser.UpdateMainLaser();
    }
}
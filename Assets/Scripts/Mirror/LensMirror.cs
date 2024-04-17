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
    /// 起点终点 index（设置为哪个line render出射0123 分别为上右左下） color为光线颜色
    /// </summary>
    /// <param name="originposition"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// 

    private void OnEnable()
    {
        mainLaser.OnChangeMirror+=ClearLine;
    }
    private void OnDisable()
    {
        mainLaser.OnChangeMirror-=ClearLine;
    }

    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {

        //他的1 是我的3 他的 0 是我的 2
        // index^=2;

        //偏移值
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        //出射方向
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        //! 放置出现自己打自己的情况 就是走一个半径
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        
        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知被击中的物体去发射射线
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

        // StartCoroutine(ClearLinePoints());
    }
    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; // 将LineRenderer中的点数量设置为0，从而清除所有点  
            lineRenderer.material.color=Color.white;
        }
    }    
    private int curRotation=0;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            TryRotateMirror();
        }
    }
    private void TryRotateMirror(){
        
            Vector3 mousePos = Input.mousePosition; // 获取鼠标的屏幕坐标  
            mousePos.z = Camera.main.nearClipPlane; // 设置z坐标为相机的近裁剪面，确保转换到正确的2D平面  
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos); // 将鼠标的屏幕坐标转换为世界坐标  
              
            // 使用Physics2D.OverlapCircle来检测圆形区域内的所有碰撞器  
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, 0.2f);  
            foreach (Collider2D collider in colliders)  
            {  
                // 检查相交的物体是否是当前物体  
                if (collider.gameObject == this.gameObject)  
                {  
                    RotateMirror();
                }  
            }
    }
    private void RotateMirror(){
        
        transform.transform.Rotate(0,0,90);
        curRotation+=1;
        curRotation%=4;
    }
}
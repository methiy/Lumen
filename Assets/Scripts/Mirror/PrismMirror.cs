using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismMirror : BaseMirror
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
        index^=2;
        //求出射时 哪个line render发射射线
        int index1=index^3,index2=index^1;

        Color color1=Color.blue,color2=Color.red;
        RRay(startPosition,endPosition,index1,color1);
        RRay(startPosition,endPosition,index2,color2);
            
    }

    private void RRay(Vector2 startPosition, Vector2 endPosition, int index, Color color){
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
            //没有击中
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
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

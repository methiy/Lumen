using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersingMirror : BaseMirror
{
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();
    [SerializeField] private const float MAX_LENGTH = 10.0f;


    private void OnEnable()
    {
        mainLaser.OnChangeMirror+=ClearLine;
        mainLaser.ClearAllMirror();
        mainLaser.RestartLaser();
    }
    private void OnDisable()
    {
        mainLaser.ClearAllMirror();
        mainLaser.RestartLaser();
        mainLaser.OnChangeMirror-=ClearLine;
    }
    
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        index^=2;

        if((index^curRotation)!=3)    return ;

        int index1=index^3,index2=index^1;
        if(index==3||index==1){
            index1=index^3;
            index2=index^1;
        }else if(index==2||index==0){
            index1=index^1;
            index2=index^3;
        }

        RRay(startPosition,endPosition,index1,color);
        RRay(startPosition,endPosition,index2,color);
            
    }

    private void RRay(Vector2 startPosition, Vector2 endPosition, int index, Color color){
        
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);

        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            
            hit.collider.GetComponent<BaseMirror>()?.Ray(
                endPosition,
                hit.collider.transform.position,
                index,
                color);
        }
        else
        {
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
        }
    }
    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; 
            lineRenderer.material.color=Color.white;
        }
    }  

    /// <summary>
    /// Rotate
    /// </summary>
    private int curRotation=0;

    public bool isRotate;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && isRotate){
            TryRotateMirror();
        }
    }
    private void TryRotateMirror(){
            Vector3 mousePos = Input.mousePosition; 
            mousePos.z = Camera.main.nearClipPlane; 
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
              
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, 0.2f);  
            foreach (Collider2D collider in colliders)  
            {  
                if (collider.gameObject == this.gameObject)  
                {  
                    StartRotation();
                }  
            }
    }
    [SerializeField]private float rotationDuration = 0.1f; // 旋转持续时间
    private bool isRotating = false; // 是否正在旋转

    // 开始旋转的方法
    public void StartRotation()
    {
        // 如果当前没有旋转过程，则启动新的旋转
        if (!isRotating)
        {
            mainLaser.ClearAllMirror();
            float rotationIncrement = 90f;
            StartCoroutine(RotateObject(rotationIncrement));
        }
    }

    // 协程函数，用于平滑地旋转物体
    IEnumerator RotateObject(float rotationIncrement)
    {
        isRotating = true; // 设置旋转状态为true
        // 计算目标旋转角度
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, rotationIncrement);

        Quaternion startRotation = transform.rotation; // 开始时的旋转角度
        Quaternion endRotation = targetRotation; // 目标旋转角度

        float elapsedTime = 0f; // 已经过时间

        // 在旋转持续时间内进行插值旋转
        while (elapsedTime < rotationDuration)
        {
            // 计算当前时间占持续时间的比例
            float t = elapsedTime / rotationDuration;
            // 使用插值函数逐渐改变当前角度到目标角度
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            // 增加已经过时间
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 旋转完成后，确保物体最终处于目标旋转角度
        transform.rotation = endRotation;

        isRotating = false; // 设置旋转状态为false

        curRotation+=1;
        curRotation%=4;
        mainLaser.RestartLaser();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LensMirror : BaseMirror
{
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    [SerializeField] private Color mirrorColor;

    //! delete
    // private VictoryManager victoryManager;
    // private void Start()
    // {
        
    // }
    // private VictoryManager GetVictoryManager(){
    //     VictoryManager[] allVictoryManager = UnityEngine.Object.FindObjectsOfType<VictoryManager>();  
    //     return allVictoryManager[0];
    // }
    

    private void OnEnable()
    {
        //! delete
        // victoryManager=GetVictoryManager();

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

        // ! delete
        // victoryManager.UpdateLensMirrorAmount(transform.position);

        //偏移??
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        
        lasersList[index].material.color=mirrorColor;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //如果击中就通知??击中的物体去发射射线
            hit.collider.GetComponent<BaseMirror>()?.Ray(
                endPosition,
                hit.collider.transform.position,
                index,
                mirrorColor);
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
            lineRenderer.positionCount = 0; // 将LineRenderer??的点数量设置??0，从而清除所有点  
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
    private float rotationDuration = 0.25f; // 旋转持续时间
    private bool isRotating = false; // 是否正在旋转

    // 开始旋转的方法
    public void StartRotation()
    {
        // 如果当前没有旋转过程，则启动新的旋转
        if (!isRotating)
        {
            float rotationIncrement = 90f;
            StartCoroutine(RotateObject(rotationIncrement));
            curRotation+=1;
            curRotation%=4;
            mainLaser.UpdateMainLaser();
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
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionInputMirror : BaseMirror
{

    private TransmissionOutputMirror outputMirror;
    public void setOutputMirror(TransmissionOutputMirror outputMirror){
        this.outputMirror = outputMirror;
    }
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        if(index==curRotation)
            outputMirror?.RRay(color);
    }


    /// <summary>
    /// Rotate
    /// </summary>
    public int curRotation=0;
    public bool isRotate;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1)&& isRotate){
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
        
        transform.Rotate(0,0,90);
        curRotation+=1;
        curRotation%=4;
        mainLaser.UpdateMainLaser();
    }
}

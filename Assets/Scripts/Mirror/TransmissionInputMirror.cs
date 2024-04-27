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
        
            Vector3 mousePos = Input.mousePosition; // ��ȡ������Ļ����  
            mousePos.z = Camera.main.nearClipPlane; // ����z����Ϊ����Ľ��ü��棬ȷ��ת������ȷ��2Dƽ��  
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos); // ��������Ļ����ת��Ϊ��������  
              
            // ʹ��Physics2D.OverlapCircle�����Բ�������ڵ�������ײ��  
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, 0.2f);  
            foreach (Collider2D collider in colliders)  
            {  
                // ����ཻ�������Ƿ��ǵ�ǰ����  
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

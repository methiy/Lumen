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
        index^=2;
        if(index==(curRotation^2))
            outputMirror?.RRay(color);
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
    private float rotationDuration = 0.25f; // ��ת����ʱ��
    private bool isRotating = false; // �Ƿ�������ת

    // ��ʼ��ת�ķ���
    public void StartRotation()
    {
        // �����ǰû����ת���̣��������µ���ת
        if (!isRotating)
        {
            float rotationIncrement = 90f;
            StartCoroutine(RotateObject(rotationIncrement));
            curRotation+=1;
            curRotation%=4;
            mainLaser.UpdateMainLaser();
        }
    }

    // Э�̺���������ƽ������ת����
    IEnumerator RotateObject(float rotationIncrement)
    {
        isRotating = true; // ������ת״̬Ϊtrue
        // ����Ŀ����ת�Ƕ�
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, rotationIncrement);

        Quaternion startRotation = transform.rotation; // ��ʼʱ����ת�Ƕ�
        Quaternion endRotation = targetRotation; // Ŀ����ת�Ƕ�

        float elapsedTime = 0f; // �Ѿ���ʱ��

        // ����ת����ʱ���ڽ��в�ֵ��ת
        while (elapsedTime < rotationDuration)
        {
            // ���㵱ǰʱ��ռ����ʱ��ı���
            float t = elapsedTime / rotationDuration;
            // ʹ�ò�ֵ�����𽥸ı䵱ǰ�Ƕȵ�Ŀ��Ƕ�
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            // �����Ѿ���ʱ��
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��ת��ɺ�ȷ���������մ���Ŀ����ת�Ƕ�
        transform.rotation = endRotation;

        isRotating = false; // ������ת״̬Ϊfalse
    }
}

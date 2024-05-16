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
    [SerializeField]private float rotationDuration = 0.1f; // ��ת����ʱ��
    private bool isRotating = false; // �Ƿ�������ת

    // ��ʼ��ת�ķ���
    public void StartRotation()
    {
        // �����ǰû����ת���̣��������µ���ת
        if (!isRotating)
        {
            mainLaser.ClearAllMirror();
            float rotationIncrement = 90f;
            StartCoroutine(RotateObject(rotationIncrement));
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

        curRotation+=1;
        curRotation%=4;
        mainLaser.RestartLaser();
    }
}

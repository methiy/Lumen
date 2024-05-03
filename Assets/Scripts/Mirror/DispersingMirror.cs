using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersingMirror : BaseMirror
{
    //����
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();

    //const
    [Header("����")]
    // [SerializeField]private const float OFFSET=0.01f;
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    // [SerializeField]private const int MAX_COUNT=10;

    /// <summary>
    /// ����յ�? index������Ϊ�ĸ�line render����0123 �ֱ�Ϊ�������£� colorΪ������ɫ
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

        //����1 ���ҵ�3 ���� 0 ���ҵ� 2
        index^=2;

        if((index^curRotation)!=3)    return ;

        //������? �ĸ�line render��������
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
        //ƫ��ֵ
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        //���䷽��
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        //! ���ó����Լ����Լ������? ������һ���뾶
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);

        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //������о�ͨ�?�����е�����ȥ��������
            hit.collider.GetComponent<BaseMirror>()?.Ray(
                endPosition,
                hit.collider.transform.position,
                index,
                color);
        }
        else
        {
            //û�л���
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
        }
        // StartCoroutine(ClearLinePoints());
    }
    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; // ��LineRenderer�еĵ���������Ϊ0���Ӷ�������е�?  
            lineRenderer.material.color=Color.white;
        }
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
            mousePos.z = Camera.main.nearClipPlane; // ����z����Ϊ����Ľ��ü���?ȷ��ת������ȷ��2Dƽ��  
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos); // ��������Ļ����ת��Ϊ��������  
              
            // ʹ��Physics2D.OverlapCircle�����Բ�������ڵ�������ײ��?  
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, 0.2f);  
            foreach (Collider2D collider in colliders)  
            {  
                // ����ཻ�������Ƿ��ǵ�ǰ����?  
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

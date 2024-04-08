using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReflectMirror : BaseMirror

{
    //����
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();
    //const
    [Header("����")]
    // [SerializeField]private const float OFFSET=0.01f;
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    // [SerializeField]private const int MAX_COUNT=10;

    /// <summary>
    /// ����յ� index������Ϊ�ĸ�line render����0123 �ֱ�Ϊ�������£� colorΪ������ɫ
    /// </summary>
    /// <param name="originposition"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {

        //����1 ���ҵ�3 ���� 0 ���ҵ� 2
        index^=2;
        //�����ʱ �ĸ�line render��������
        index ^= 3;
        //ƫ��ֵ
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        //���䷽��
        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        //! ���ó����Լ����Լ������ ������һ���뾶
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        // Debug.Log(endPosition+direction*1.1f);
        // if(hit.collider==null)
        //     Debug.Log("NULL");
        // else
        //     Debug.Log(hit.rigidbody.name);
        
        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, endPosition);
            lasersList[index].SetPosition(1, hit.collider.transform.position);
            //������о�֪ͨ�����е�����ȥ��������
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
            //û�л���
            lasersList[index].positionCount = 2;
            lasersList[index].SetPosition(0, transform.position);
            lasersList[index].SetPosition(1,transform.position+(Vector3)direction*MAX_LENGTH);
        }
        StartCoroutine(ClearLinePoints());
    }

    private float clearInterval =0.1f;
    private IEnumerator ClearLinePoints()  
    {  
        while (true) // ����ѭ����ֱ��ֹͣCoroutine  
        {  
            yield return new WaitForSeconds(clearInterval); // �ȴ�ָ����ʱ����  
            ClearLine(); // ���LineRenderer�еĵ�  
        }  
    }  
  
    private void ClearLine()  
    {  
        foreach(var lineRenderer in lasersList){
            lineRenderer.positionCount = 0; // ��LineRenderer�еĵ���������Ϊ0���Ӷ�������е�  
            lineRenderer.material.color=Color.white;
        }
    }    



    // private void Ray(Vector2 startPosition,Vector2 endPosition){
    //     //���䷽��
    //     Vector2 direction = (endPosition - startPosition).normalized;

    //     RaycastHit2D hit=Physics2D.Raycast(startPosition, direction,MAX_LENGTH,layerMasks);
    //     List<Pair> hitList=new List<Pair>();
    //     if(hit.collider!=null)  hitList.Add(new Pair(hit,startPosition));
    //     int count=0;
    //     while(hitList.Count!=0&&++count<MAX_COUNT)
    //     {
    //         List<Pair> newhitList=new List<Pair>();
    //         foreach(Pair hit2D in hitList) {

    //             RaycastHit2D raycastHit2D=hit2D.GetRaycastHit2D();//! ��ǰ���еĵ�
    //             Vector2 originPosition=hit2D.GetOriginPosition();

    //             hit=ReflectMirror(raycastHit2D,originPosition);
    //             if(hit.collider!=null)  newhitList.Add(new Pair(hit,raycastHit2D.point));
    //             hit=LensMirror(raycastHit2D,originPosition);
    //             if(hit.collider!=null)  newhitList.Add(new Pair(hit,raycastHit2D.point));
    //         }
    //         hitList=newhitList;
    //     }
    // }


    // private RaycastHit2D ReflectMirror(RaycastHit2D raycastHit2D,Vector2 originPosition) {

    //     Vector2 direction = Vector2.Reflect(originPosition,raycastHit2D.normal);

    //     RaycastHit2D hit=Physics2D.Raycast(originPosition, direction,MAX_LENGTH,mirrorLayerMask);

    //     return hit; 
    // }
    // private RaycastHit2D LensMirror(RaycastHit2D raycastHit2D,Vector2 originPosition){
    //     //���䷽��
    //     Vector2 direction = (raycastHit2D.point-originPosition).normalized;

    //     RaycastHit2D hit=Physics2D.Raycast(originPosition, direction,MAX_LENGTH,lensLayerMask);

    //     return hit;
    // }
}

// /// <summary>
// /// ��¼���߻��е�Ŀ���������ʼ��
// /// </summary>
// public class Pair
// {
//     RaycastHit2D raycastHit2D;
//     Vector2 originPosition;

//     public Pair(RaycastHit2D raycastHit2D, Vector2 originPosition)
//     {
//         this.raycastHit2D = raycastHit2D;
//         this.originPosition = originPosition;
//     }
//     public RaycastHit2D GetRaycastHit2D()
//     {
//         return raycastHit2D;
//     }
//     public Vector2 GetOriginPosition()
//     {
//         return originPosition;
//     }
// }
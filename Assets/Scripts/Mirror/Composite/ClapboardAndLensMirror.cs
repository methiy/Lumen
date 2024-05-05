using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapboardAndLensMirror : BaseMirror
{
    [SerializeField] private List<LineRenderer> lasersList = new List<LineRenderer>();
    [SerializeField] private const float MAX_LENGTH = 10.0f;
    

    /// <summary>
    /// 
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

       index^=2;

       if(index%2!=curRotation%2)   return ;

       int index1=index^2;
       
       RRay(startPosition, endPosition, index1, color);

    }

    public void RRay(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        Vector2 direction=new Vector2(dx[index],dy[index]).normalized;
        
        float lineOffset=1.1f;
        RaycastHit2D hit = Physics2D.Raycast(endPosition+lineOffset*direction, direction, MAX_LENGTH, layerMasks);
        
        lasersList[index].material.color=color;
        if (hit.collider != null && hit.collider.GetComponent<BaseMirror>()&&(endPosition!=(Vector2)hit.collider.transform.position))
        {
            // Debug.Log(this.transform.name + "hit "+index);
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
        if(Input.GetMouseButtonDown(1)){
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
                RotateMirror();
            }  
        }
    }
    private void RotateMirror(){

        if (isRotate)
        {
            transform.Rotate(0,0,90);
            curRotation+=1;
            curRotation%=4;
        }

        mainLaser.UpdateMainLaser();
    }
}

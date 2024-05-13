using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField]public MirrorScriptableObject mirrorSO;

    private Detection detection;
    private Composite composite;

    [SerializeField]private bool isCanDrag;
    
    private void OnEnable()
    {
        composite = GetComponent<Composite>();
        prePosition=transform.position; 
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];
    }
    private void OnDisable()
    {
    }
    private Vector3 prePosition;
    // [SerializeField]private Texture2D cursor;

    public Vector3 GetMousePosition()
    { 
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z=0;

        return mousePosition;
    }
    public void OnMouseDown()
    {

    }

    public void OnMouseDrag()
    {
        if(!isCanDrag) return;
        transform.position=GetMousePosition();
    }
    public void OnMouseUp()
    {
        if(!isCanDrag) return;


        bool isPlay=false,isIcon=false;
        MirrorScriptableObject targetMirror=null;

        Vector3 targetPosition=GetMousePosition();
        Vector3 location;
        if (detection.PlayPositionPlaceable(targetPosition,out location)){
            if(composite!=null && composite.HasMirror(location)){
                targetMirror = composite.CompositeResult();
                Debug.Log(targetMirror);
                if(targetMirror!=null){
                    isPlay=true;
                    transform.position=location;
                    Instantiate(targetMirror.mirrorPrefab,location,targetMirror.mirrorPrefab.transform.rotation);
                }else{
                    transform.position=prePosition;    
                }
            }else{
                isPlay=true;
                transform.position=location;
                tryCreatMirror(location);
            }
        }else if(mirrorSO.isHaveIcon && detection.IconPositionPlaceable(targetPosition,out location)){
            if(composite!=null && composite.HasMirrorIcon(location, mirrorSO.mirrorType)){
                transform.position=prePosition;    
            }else{
                isIcon=true;
                transform.position=location;
                tryRebackMirror(location);
            }

        }else{
            transform.position=prePosition;
        }    

        prePosition=transform.position;
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        if(isPlay||isIcon){
            Destroy(gameObject);
        }
    }


    private void tryCreatMirror(Vector3 position){
        GameObject newGameobject = Instantiate(mirrorSO.mirrorPrefab, position, mirrorSO.mirrorPrefab.transform.rotation);
        if(newGameobject.GetComponent<Composite>() != null)
            newGameobject.GetComponent<Composite>().compositable = composite.compositable;
    }

    private void tryRebackMirror(Vector3 position){
        GameObject newGameobject = Instantiate(mirrorSO.mirrorIconPrefab,position,Quaternion.identity);
        if(newGameobject.GetComponent<Composite>() != null)
            newGameobject.GetComponent<Composite>().compositable = composite.compositable;
    }

}

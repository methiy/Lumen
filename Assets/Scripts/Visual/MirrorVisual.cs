using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

using UnityEngine;
using UnityEngine.EventSystems;

public class MirrorVisual:MonoBehaviour{
    [SerializeField]public MirrorScriptableObject mirrorSO;

    private Detection detection;
    private Composite composite;
    private Vector3 prePosition;
    private void OnEnable()
    {
        composite = GetComponent<Composite>(); 
        prePosition = transform.position;
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];
    }

    private void OnDisable()
    {
    }

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
        transform.position=GetMousePosition();
    }

    public void OnMouseUp()
    {
        bool isPlay=false,isIcon=false;
        MirrorScriptableObject targetMirror=null;

        Vector3 targetPosition=GetMousePosition();
        Vector3 location;
        if (detection.PlayPositionPlaceable(targetPosition,out location)){
            //是否有镜子 是hasMirror的返回值
            //composite.CompositeResult() 能不能合成
            if(composite!=null && composite.HasMirror(location)){
                targetMirror = composite.CompositeResult();
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
            if(composite!=null && composite.HasMirror(location)){
                targetMirror = composite.CompositeResult();
                if(targetMirror!=null){
                    isPlay=true;
                    transform.position=location;
                    Instantiate(targetMirror.mirrorPrefab,location,targetMirror.mirrorPrefab.transform.rotation);
                }else{
                    transform.position=prePosition;    
                }
            }else if(composite!=null && composite.HasMirrorIcon(location, mirrorSO.mirrorType)){
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
        newGameobject.GetComponent<Composite>().compositable = composite.compositable;
    }

    private void tryRebackMirror(Vector3 position){
        GameObject newGameobject = Instantiate(mirrorSO.mirrorIconPrefab,position,Quaternion.identity);
        newGameobject.GetComponent<Composite>().compositable = composite.compositable;
    }
}
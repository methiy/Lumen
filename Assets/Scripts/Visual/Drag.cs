using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField]public MirrorScriptableObject mirrorSO;

    private Detection detection;
    private Composite composite;

    
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
                    Instantiate(targetMirror.mirrorPrefab,location,Quaternion.identity);
                }else{
                    transform.position=prePosition;    
                }
            }else{
                isPlay=true;
                transform.position=location;
                tryCreatMirror(location);
            }
        }else if(mirrorSO.isHaveIcon && detection.IconPositionPlaceable(targetPosition,out location)){
            isIcon=true;
            transform.position=location;
            //! TODO  放回icon
            tryRebackMirror(location);

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
        //! todo 判断
        Instantiate(mirrorSO.mirrorPrefab,position,Quaternion.identity);
    }

    private void tryRebackMirror(Vector3 position){
        //! todo 判断
        Instantiate(mirrorSO.mirrorIconPrefab,position,Quaternion.identity);
        //! 事件让他将sprite,数量+(1)

    }

}

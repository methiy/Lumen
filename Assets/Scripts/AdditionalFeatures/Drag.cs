using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField]public MirrorScriptableObject mirrorSO;

    [SerializeField]private Detection detection;

    private Composite composite;

    
    private void OnEnable()
    {
        composite = GetComponent<Composite>();
        prePosition=transform.position; 
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];

        if(detection.UsedIconPosition.ContainsKey(detection.PositionToString(prePosition))){
            detection.UsedIconPosition[detection.PositionToString(prePosition)]="true";
        }
        if(detection.UsedPlayPosition.ContainsKey(detection.PositionToString(prePosition))){
            detection.UsedPlayPosition[detection.PositionToString(prePosition)]="true";
        }
    }
    private void OnDisable()
    {
        if(detection.UsedIconPosition.ContainsKey(detection.PositionToString(prePosition)))
            detection.UsedIconPosition[detection.PositionToString(prePosition)]="false";
        if(detection.UsedPlayPosition.ContainsKey(detection.PositionToString(prePosition)))
            detection.UsedPlayPosition[detection.PositionToString(prePosition)]="false";
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
        bool isComposable=false;
        MirrorScriptableObject targetMirror=null;
        if(composite!=null && composite.Composable()){
            targetMirror = composite.CompositeResult();
            isComposable=true;
        }

        Vector3 targetPosition=GetMousePosition();
        Vector3 location;
        if (detection.PlayPositionPlaceable(targetPosition,out location)){
            //! TODO 放置镜子生成实体
            if(!isComposable){
                isPlay=true;
                isComposable=false;
                transform.position=location;
                tryCreatMirror(location);
            }else if(targetMirror!=null){
                isPlay=true;
                transform.position=location;
                Instantiate(targetMirror.mirrorPrefab,location,Quaternion.identity);
            }else{
                isComposable=false;
                transform.position=prePosition;    
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
        if(isPlay||isIcon||isComposable){
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

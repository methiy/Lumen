using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField]private MirrorScriptableObject mirrorSO;

    [SerializeField]private Detection detection;
    
    private void OnEnable()
    {
        prePosition=transform.position; 
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];
        if(detection.UsedIconPosition.ContainsKey(prePosition))
            detection.UsedIconPosition[prePosition]=true;
        if(detection.UsedPlayPosition.ContainsKey(prePosition))
            detection.UsedPlayPosition[prePosition]=true;
    }
    private void OnDisable()
    {
        if(detection.UsedIconPosition.ContainsKey(prePosition))
            detection.UsedIconPosition[prePosition]=false;
        if(detection.UsedPlayPosition.ContainsKey(prePosition))
            detection.UsedPlayPosition[prePosition]=false;
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
        Vector3 targetPosition=GetMousePosition();
        bool isPlay=false,isIcon=false;
        Vector3 location;
        if (detection.PlayPositionPlaceable(targetPosition,out location)){
            Debug.Log("Play");
            isPlay=true;
            transform.position=location;
            //! TODO 放置镜子生成实体
            tryCreatMirror(location);

        }else if(detection.IconPositionPlaceable(targetPosition,out location)){
            Debug.Log("Icon");
            isIcon=true;
            transform.position=location;
            Debug.Log("Icon location: " + location);
            //! TODO  放回icon
            tryRebackMirror(location);

        }else{
            transform.position=prePosition;
            Debug.Log("Icon location: ");
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

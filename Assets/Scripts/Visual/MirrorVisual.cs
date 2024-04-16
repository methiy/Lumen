using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MirrorVisual:MonoBehaviour{
    private MainLaser mainLaser;
    [SerializeField]private MirrorScriptableObject mirrorSO;
    [SerializeField]private int mirrorAmount;
    [SerializeField]private Transform icon;

    [SerializeField]private Vector3 originLocalScale;
    [SerializeField]private float scaleFactort=1.2f;

    [SerializeField]private Detection detection;
    private Vector3 prePosition;

    // [SerializeField]private MirrorManager mirrorManager;

    // private Vector3 bornPosition;

    // private void OnEnable()
    // {
    //     bornPosition=transform.position;
    // }
    // private void OnDestroy()
    // {
    //     mirrorManager.SetVisualAmount(bornPosition,mirrorSO.mirrorType);
    // }

    // private MirrorManager GetMirrorManager(){
    //     MirrorManager[] allMirrorManager = UnityEngine.Object.FindObjectsOfType<MirrorManager>();  
    //     return allMirrorManager[0];
    // }
    // private void OnEnable()
    // {
    //     mirrorManager=GetMirrorManager();
    //     Debug.Log(transform.position);
    //     mirrorManager.SetVisualAmount(transform.position,mirrorSO.mirrorType);
    // }
    private void OnEnable()
    {
        prePosition = transform.position;
        originLocalScale =icon.localScale;
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
    
    // [SerializeField]private Texture2D cursor;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z=0;

        return mousePosition;
    }

    public void OnMouseDown()
    {
        //cursor 
        // cursor=mirrorSO.sprite.texture;
        // Cursor.SetCursor(cursor,Vector2.zero,CursorMode.Auto);

    }

    public void OnMouseDrag()
    {
        transform.position=GetMousePosition();
        Debug.Log("Icon location: " + prePosition);
    }

    public void OnMouseUp()
    {
        Debug.Log("Mouse Up ");
        Vector3 targetPosition=GetMousePosition();
       bool isPlay=false,isIcon=false;
       Vector3 location; 
        if(detection.PlayPositionPlaceable(targetPosition,out location)){
            Debug.Log("Play");
            isPlay=true;
            transform.position=location;
            //! TODO 放置镜子生成实体
            tryCreatMirror(location);

        }else if(detection.IconPositionPlaceable(targetPosition,out location)){
            Debug.Log("Icon");
            isIcon=true;
            transform.position=location;
            //! TODO  放回icon
            tryRebackMirror(location);

        }else{
            Debug.Log("no");
            Debug.Log("Icon location: " + prePosition);
            transform.position=prePosition;
        }

        prePosition=transform.position; 
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        if(isPlay||isIcon){
            Destroy(transform.parent.gameObject);
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
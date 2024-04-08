using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{

    [SerializeField]private MirrorScriptableObject mirrorSO;

    [SerializeField]private Detection detection;

    private void Start()
    {
        prePosition=transform.position; 
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];
    }

    private Vector3 prePosition;
    [SerializeField]private Texture2D cursor;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z=0;

        return mousePosition;
    }
    public void OnMouseDown()
    {
        //cursor 
        cursor=mirrorSO.sprite.texture;
        Cursor.SetCursor(cursor,Vector2.zero,CursorMode.Auto);

    }

    public void OnMouseDrag()
    {
        transform.position=GetMousePosition();
    }

    public void OnMouseUp()
    {
       Vector3 targetPosition=GetMousePosition();
       bool flag=false;

        if(detection.PlayPositionPlaceable(targetPosition)){
            Debug.Log("Play");
            flag=true;
            transform.position=targetPosition;
            //! TODO 放置镜子生成实体
            tryCreatMirror();

        }else if(detection.IconPositionPlaceable(targetPosition)){
            Debug.Log("Icon");
            flag=true;
            transform.position=targetPosition;
            //! TODO  放回icon
            tryRebackMirror();

        }else{
            transform.position=prePosition;
        }

        prePosition=transform.position; 
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        if(flag){
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }


    private void tryCreatMirror(){
        //! todo 判断
        Instantiate(mirrorSO.mirrorPrefab);
    }

    private void tryRebackMirror(){
        //! todo 判断

        //! 事件让他将sprite设置为mirrorSO.icon，并且数量+(1)



    }





    // private Vector3 prePosition;
    // [SerializeField]private Detection detection;

    // private void Start()
    // {
    //     prePosition=transform.position; 
    // }

    // public Vector3 GetMousePosition()
    // {
    //     Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     mousePosition.z=0;

    //     return mousePosition;
    // }
    // private void OnMouseDown()
    // {
        
    // }
    // private void OnMouseDrag()
    // {
    //     transform.position=GetMousePosition();
    // }
    // private void OnMouseUp()
    // {
    //     Vector3 targetPosition=GetMousePosition();

    //     bool canLocation=detection.PlayPositionPlaceable(targetPosition);

    //     if(detection.PlayPositionPlaceable(targetPosition)){
            
    //         transform.position=targetPosition;
    //         //! TODO 放置镜子生成实体
    //         // tryCreatMirror();

    //     }else if(detection.IconPositionPlaceable(targetPosition)){
    //         transform.position=targetPosition;
    //         //! TODO  放回icon
    //         // tryRebackMirror();

    //     }else{
    //         transform.position=prePosition;
    //     }

    //     prePosition=transform.position; 
    // }

}

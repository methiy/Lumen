using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MirrorVisual : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]private MirrorScriptableObject mirrorSO;
    [SerializeField]private int mirrorAmount;
    [SerializeField]private Transform icon;

    [SerializeField]private Vector3 originLocalScale;
    [SerializeField]private float scaleFactort=1.2f;

    [SerializeField]private Detection detection;

    private void Start()
    {
        originLocalScale=icon.localScale;
        prePosition=transform.position; 
        Detection[] detections=FindObjectsOfType<Detection>();
        detection=detections[0];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.localScale=originLocalScale*scaleFactort;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.localScale=originLocalScale;
    }

    private Vector3 prePosition;
    [SerializeField]private Texture2D cursor;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z=0;

        return mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //cursor 
        cursor=mirrorSO.sprite.texture;
        Cursor.SetCursor(cursor,Vector2.zero,CursorMode.Auto);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=GetMousePosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 targetPosition=GetMousePosition();

        if(detection.PlayPositionPlaceable(targetPosition)){
            
            transform.position=targetPosition;
            //! TODO 放置镜子生成实体
            tryCreatMirror();

        }else if(detection.IconPositionPlaceable(targetPosition)){
            transform.position=targetPosition;
            //! TODO  放回icon
            tryRebackMirror();

        }else{
            transform.position=prePosition;
        }

        prePosition=transform.position; 
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }


    private void tryCreatMirror(){
        //! todo 判断
        Instantiate(mirrorSO.mirrorPrefab);
    }

    private void tryRebackMirror(){
        //! todo 判断

        //! 事件让他将sprite设置为mirrorSO.icon，并且数量+(1)



    }
}

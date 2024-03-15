using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    private Vector3 prePosition;
    [SerializeField]private GameObject gameObject;
    [SerializeField]private List<Vector3> canLoadPositionList=new List<Vector3>();

    private const float OFFSET=2.0f;

    private void Start()
    {
        prePosition=transform.position; 
        foreach(Transform child in gameObject.GetComponentInChildren<Transform>(true)){
            if(child!=gameObject)
                canLoadPositionList.Add(child.position);
        }
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z=0;

        return mousePosition;
    }

    private void OnMouseDrag()
    {
        transform.position=GetMousePosition();
    }
    private void OnMouseUp()
    {
        Vector3 targetPosition=GetMousePosition();

        bool canLocation=false;
        foreach(Vector3 position in canLoadPositionList){
            if(Mathf.Abs(position.x-targetPosition.x)<=OFFSET&&Mathf.Abs(position.y-targetPosition.y)<=OFFSET){
                targetPosition=position;
                canLocation=true;
                break;
            }
        }
        if(!canLocation)   targetPosition=prePosition;

        transform.position=targetPosition;
        prePosition =targetPosition;
    }
    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log("1");
    //     transform.position=GetMousePosition();
    // }
    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     Debug.Log("2");
    //     Vector3 targetPosition=GetMousePosition();

    //     bool canLocation=false;
    //     foreach(Vector3 position in canLoadPositionList){
    //         if(Mathf.Abs(position.x-targetPosition.x)<=OFFSET&&Mathf.Abs(position.y-targetPosition.y)<=OFFSET){
    //             targetPosition=position;
    //             canLocation=true;
    //             break;
    //         }
    //     }
    //     if(!canLocation)   targetPosition=prePosition;

    //     transform.position=targetPosition;
    //     prePosition =targetPosition;
    // }
}

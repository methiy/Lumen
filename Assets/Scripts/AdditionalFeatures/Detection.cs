using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    [SerializeField]private GameObject playArea,iconArea;

    //! play Area
    [SerializeField]private List<Vector3> playPositionList=new List<Vector3>();
    //! icon Area
    [SerializeField]private List<Vector3> iconPositionList=new List<Vector3>();

    public Dictionary<Vector3,bool> UsedPlayPosition=new Dictionary<Vector3, bool>();
    public Dictionary<Vector3,bool> UsedIconPosition=new Dictionary<Vector3, bool>();

    
    private const float OFFSET=2.0f;

    private void Awake()
    {
        foreach(Transform child in playArea.GetComponentInChildren<Transform>(true)){
            if(child!=playArea){
                playPositionList.Add(child.position);
                UsedPlayPosition.Add(child.position,false);
            }
        }
        foreach(Transform child in iconArea.GetComponentInChildren<Transform>(true)){
            if(child!=iconArea){
                iconPositionList.Add(child.position);
                UsedIconPosition.Add(child.position,false);
            }
        }

    }
    public bool PlayPositionPlaceable(Vector3 targetPosition,out Vector3 location){
    
        bool canLocation=false;
        location=Vector3.zero;

        foreach(Vector3 position in playPositionList){
            if(Mathf.Abs(position.x-targetPosition.x)<=OFFSET&&Mathf.Abs(position.y-targetPosition.y)<=OFFSET){
                location=position;
                canLocation=true;
                break;
            }
        }

        return canLocation;
    }

    public bool IconPositionPlaceable(Vector3 targetPosition,out Vector3 location){

        bool canLocation=false;
        location=Vector3.zero;

        foreach(Vector3 position in iconPositionList){
            if(Mathf.Abs(position.x-targetPosition.x)<=OFFSET&&Mathf.Abs(position.y-targetPosition.y)<=OFFSET){
                location=position;
                canLocation=true;
                break;
            }
        }

        return canLocation;
    }



}

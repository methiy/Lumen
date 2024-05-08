using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField]private int targetAmount;
    [SerializeField]private int currentAmount = 0;

    [SerializeField]private int targetLensMirrorAmount;
    [SerializeField]private int currentLensMirrorAmount = 0;

    [SerializeField]private GameObject gameObject;
    [SerializeField]private GameObject gameObjec;

    private Dictionary<string,bool> hasUpdate = new Dictionary<string, bool>();
    private Dictionary<string,bool> hasUpdate1 = new Dictionary<string, bool>();
    [SerializeField]private bool addition;

    public void UpdateAmount(Vector3 vector3){
        if(hasUpdate.ContainsKey(vector3.ToString())){
            if(currentAmount >= targetAmount && addition){
            Victory();
        }
            return ;
        }
        hasUpdate[vector3.ToString()]=true;
        currentAmount++;
        if(currentAmount >= targetAmount && addition){
            Victory();
        }
    }

    public void UpdateLensMirrorAmount(Vector3 vector3){
        if(hasUpdate1.ContainsKey(vector3.ToString())){
            return ;
        }
        hasUpdate1[vector3.ToString()]=true;
        currentLensMirrorAmount++;
        
        if(currentLensMirrorAmount >= targetLensMirrorAmount){
            addition=true;
        }
    }

    private void Victory(){
        gameObjec.SetActive(false);
        gameObject.SetActive(true); 
    }
}

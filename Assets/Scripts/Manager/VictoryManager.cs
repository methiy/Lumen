using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [SerializeField]private int targetAmount;
    [SerializeField]private int targetLensMirrorAmount;

    [SerializeField]private GameObject gameObject;
    [SerializeField]private GameObject gameObjec;
    private bool hasVictory;

    // private Dictionary<string,bool> hasUpdate = new Dictionary<string, bool>();
    // private Dictionary<string,bool> hasUpdate1 = new Dictionary<string, bool>();
    // [SerializeField]private bool addition;

    private void Start()
    {
        hasVictory = false;
    }


    private void Update()
    {
        if(!hasVictory && UpdateAmount() && UpdateLensMirrorAmount()){
            CompleteTheLevel();
        }
    }

    public bool UpdateAmount(){
        int currentAmount = 0;
        Dictionary<string,bool> dict = new Dictionary<string, bool>();

        LineRenderer[] LineRenderers = UnityEngine.Object.FindObjectsOfType<LineRenderer>();
        foreach(LineRenderer lineRenderer in LineRenderers){
            int pointCount = lineRenderer.positionCount;
            // 遍历所有点，并输出它们的位置
            for (int i = 0; i < pointCount; i++)
            {
                Vector3 point = lineRenderer.GetPosition(i);
                dict[point.ToString()] = true;
            }
        }

        TargetMirror[] targetMirrors = UnityEngine.Object.FindObjectsOfType<TargetMirror>();     
        foreach (var TargetMirror in targetMirrors)
        {
            if(TargetMirror.ColorisRight() && dict.ContainsKey(TargetMirror.transform.position.ToString())){
                currentAmount++;
            }
        }
        if(currentAmount>=targetAmount){
            return true;
        }
        return false;
    }

    public bool UpdateLensMirrorAmount(){
    
        int currentLensMirrorAmount = 0;
        Dictionary<string,bool> dict = new Dictionary<string, bool>();

        LineRenderer[] LineRenderers = UnityEngine.Object.FindObjectsOfType<LineRenderer>();
        foreach(LineRenderer lineRenderer in LineRenderers){
            int pointCount = lineRenderer.positionCount;
            // 遍历所有点，并输出它们的位置
            for (int i = 0; i < pointCount; i++)
            {
                Vector3 point = lineRenderer.GetPosition(i);
                dict[point.ToString()] = true;
            }
        }

        LensMirror[] LensMirrors = UnityEngine.Object.FindObjectsOfType<LensMirror>();     
        foreach (var LensMirror in LensMirrors)
        {
            if(dict.ContainsKey(LensMirror.transform.position.ToString())){
                currentLensMirrorAmount++;
            }
        }

        if(currentLensMirrorAmount >= targetLensMirrorAmount){
            return true;
        }
        return false;
    }

    private void CompleteTheLevel(){
        hasVictory = true;
        gameObjec.SetActive(false);
        gameObject.SetActive(true);
        if(LevelSelection.Instance!=null)
            LevelSelection.Instance.setLevelStatus(SceneManager.GetActiveScene().name);
        else
            Debug.LogError("Instance is null");
    }
}

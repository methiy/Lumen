using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    [SerializeField]private int clapboardAmount;
    [SerializeField]private int lensMirrorAmount;
    [SerializeField]private int prismMirrorAmount;
    [SerializeField]private int reflectAmount;


    [SerializeField]private List<Transform> transforms= new List<Transform>();
    [SerializeField]private Dictionary<Vector3,int> boards=new Dictionary<Vector3, int>();

    private void OnEnable()
    {
        init();
    }
    private void Start()
    {
        
    }
    void init(){
        for(int i=0;i<transforms.Count;i++){
            boards.Add(transforms[i].position,i);
            Debug.Log(transforms[i].position);
        }
    }
    
    
    private int GetMirrorAmount(MirrorType mirrorType){
        int result=0;
        switch (mirrorType)
        {
            case MirrorType.ReflectMirror:
            result=reflectAmount-GetUsedMirrorAmount(mirrorType);
            break;
            case MirrorType.PrismMirror:
            result=prismMirrorAmount-GetUsedMirrorAmount(mirrorType);
            break;
            case MirrorType.LensMirror:
            result=lensMirrorAmount-GetUsedMirrorAmount(mirrorType);
            break;
            case MirrorType.Clapboard:
            result=clapboardAmount-GetUsedMirrorAmount(mirrorType);
            break;
        }
        return result;
    }
    private int GetUsedMirrorAmount(MirrorType mirrorType){
        int result=0;
        switch (mirrorType)
        {
            case MirrorType.ReflectMirror:
                result=CountObjectsWithScript<ReflectMirror>();
                break;
            case MirrorType.PrismMirror:
                result=CountObjectsWithScript<PrismMirror>();
                break;
            case MirrorType.LensMirror:
                result=CountObjectsWithScript<LensMirror>();
                break;
            case MirrorType.Clapboard:
                result=CountObjectsWithScript<Clapboard>();
                break;
        }
        Debug.Log("Used "+result);
        return result;
    }

    private int CountObjectsWithScript<T>() where T : MonoBehaviour  
    {  
        return FindObjectsOfType<GameObject>()  
            .Where(go => go.GetComponent<T>() != null)  
            .Count();  
    }

    public void SetVisualAmount(Vector3 position,MirrorType mirrorType){
        Debug.Log("Ò»½×¶Î");
        if(boards.ContainsKey(position)){

            int amount=GetMirrorAmount(mirrorType);
            Debug.Log("¶þ½×¶Î");
            transforms[boards[position]].GetComponent<MirrorAmountVisual>()?.ChangeAmount(amount);
        }
    }

}
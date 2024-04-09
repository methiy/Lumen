using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    [SerializeField]private int clapboardAmount=0;
    [SerializeField]private int lensMirrorAmount=0;
    [SerializeField]private int prismMirrorAmount=0;
    [SerializeField]private int reflectAmount=0;
    
    public int GetMirrorAmount(MirrorType mirrorType){
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
    public int GetUsedMirrorAmount(MirrorType mirrorType){
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
        return result;
    }

    private int CountObjectsWithScript<T>() where T : MonoBehaviour  
    {  
        return FindObjectsOfType<GameObject>()  
            .Where(go => go.GetComponent<T>() != null)  
            .Count();  
    }

}

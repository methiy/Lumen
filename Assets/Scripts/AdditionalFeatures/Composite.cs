using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : MonoBehaviour
{
    [SerializeField]public bool compositable;
    private MirrorScriptableObject mirrorResult;
    [SerializeField]private MirrorScriptableObject ClapboardAndRedLensMirror;
    [SerializeField]private MirrorScriptableObject ClapboardAndBlueLensMirror;
    [SerializeField]private MirrorScriptableObject ClapboardAndWhiteLensMirror;
    [SerializeField]private MirrorScriptableObject RedLensAndReflectMirror;
    [SerializeField]private MirrorScriptableObject BlueLensAndReflectMirror;
    [SerializeField]private MirrorScriptableObject WhiteLensAndReflectMirror;
    [SerializeField]private MirrorScriptableObject ReflectAndClapBoardMirror;
    [SerializeField]private MirrorScriptableObject ReflectAndDispersingMirror;
    [SerializeField]private LayerMask layerMask;

    MirrorScriptableObject GetMirrorScriptableObject(MirrorType mirrorType){
        switch(mirrorType){
            case MirrorType.ClapboardAndRedLensMirror:
            return ClapboardAndRedLensMirror;
            break;
            case MirrorType.ClapboardAndBlueLensMirror:
            return ClapboardAndBlueLensMirror;
            break;
            case MirrorType.ClapboardAndWhiteLensMirror:
            return ClapboardAndWhiteLensMirror;
            break;
            case MirrorType.RedLensAndReflectMirror:
            return RedLensAndReflectMirror;
            break;
            case MirrorType.BlueLensAndReflectMirror:
            return BlueLensAndReflectMirror;
            break;
            case MirrorType.WhiteLensAndReflectMirror:
            return WhiteLensAndReflectMirror;
            break;
            case MirrorType.ReflectAndClapBoardMirror:
            return ReflectAndClapBoardMirror;
            break;
            case MirrorType.ReflectAndDispersingMirror:
            return ReflectAndDispersingMirror;
            break;
            default :
            return null;
        }
    }

    MirrorScriptableObject FindTargetMirror(MirrorType firstMirror, MirrorType secondMirror){
        Debug.Log(firstMirror+" "+secondMirror);
        MirrorScriptableObject mirrorTarget = null;

        if(!compositable)   return mirrorTarget;

        if((firstMirror == MirrorType.Clapboard && secondMirror == MirrorType.RedLensMirror) || 
           (firstMirror == MirrorType.RedLensMirror && secondMirror == MirrorType.Clapboard)
        ){
            return GetMirrorScriptableObject(MirrorType.ClapboardAndRedLensMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.RedLensMirror) || 
                 (firstMirror == MirrorType.RedLensMirror && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.RedLensAndReflectMirror);
        }else if((firstMirror == MirrorType.Clapboard && secondMirror == MirrorType.BlueLensMirror) || 
           (firstMirror == MirrorType.BlueLensMirror && secondMirror == MirrorType.Clapboard)
        ){
            return GetMirrorScriptableObject(MirrorType.ClapboardAndBlueLensMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.BlueLensMirror) || 
                 (firstMirror == MirrorType.BlueLensMirror && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.BlueLensAndReflectMirror);
        }else if((firstMirror == MirrorType.Clapboard && secondMirror == MirrorType.WhiteLensMirror) || 
           (firstMirror == MirrorType.WhiteLensMirror && secondMirror == MirrorType.Clapboard)
        ){
            return GetMirrorScriptableObject(MirrorType.ClapboardAndWhiteLensMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.WhiteLensMirror) || 
                 (firstMirror == MirrorType.WhiteLensMirror && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.WhiteLensAndReflectMirror);
        }
        else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.Clapboard) || 
                 (firstMirror == MirrorType.Clapboard && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.ReflectAndClapBoardMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.DispersingMirror) || 
                 (firstMirror == MirrorType.DispersingMirror && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.ReflectAndDispersingMirror);
        }
        return mirrorTarget;
    }
    private RaycastHit2D CheckHits(RaycastHit2D[] hits)
    {
        if (hits == null || hits.Length == 0) // 检查 hits 是否为 null 或者空数组
        {
            return default(RaycastHit2D); // 返回默认值
        }

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject) // 排除自身
            {
                return hit;
            }
        }
        return hits[0];
    }
    /// <summary>
    /// 上面或者下面有没有东西
    /// </summary>
    /// <returns></returns>
    public bool HasMirror(Vector3 position){
        bool result = false;
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.back,layerMask);
        RaycastHit2D hit=CheckHits(hits);
        if (hit.collider != null&&hit.collider.gameObject != gameObject)
        {
            Drag firstDrag = hit.collider.gameObject.GetComponent<Drag>();
            Drag secondDrag = GetComponent<Drag>();
            MirrorType firstMirror=MirrorType.BaseMirror,secondMirror=MirrorType.BaseMirror;
            if (firstDrag != null && firstDrag.mirrorSO != null) {
                firstMirror = firstDrag.mirrorSO.mirrorType;
            }
            if (secondDrag != null && secondDrag.mirrorSO != null) {
                secondMirror = secondDrag.mirrorSO.mirrorType;
            }
            mirrorResult = FindTargetMirror(firstMirror, secondMirror);
            result=true;
            
        }else{
            hits = Physics2D.RaycastAll(position,Vector3.forward,layerMask);
            hit = CheckHits(hits);
            if (hit.collider != null &&  hit.collider.gameObject != gameObject)
            {
                Drag firstDrag = hit.collider.gameObject.GetComponent<Drag>();
                Drag secondDrag = GetComponent<Drag>();
                MirrorType firstMirror=MirrorType.BaseMirror,secondMirror=MirrorType.BaseMirror;
                if (firstDrag != null && firstDrag.mirrorSO != null) {
                    firstMirror = firstDrag.mirrorSO.mirrorType;
                }
                if (secondDrag != null && secondDrag.mirrorSO != null) {
                    secondMirror = secondDrag.mirrorSO.mirrorType;
                }
                mirrorResult = FindTargetMirror(firstMirror, secondMirror);
                result=true;
            }else{
                result = false;
            }
        }
        if(result&&mirrorResult!=null)  Destroy(hit.collider.gameObject);
        return result;
    }

    public MirrorScriptableObject  CompositeResult(){
        return mirrorResult;
    }
}


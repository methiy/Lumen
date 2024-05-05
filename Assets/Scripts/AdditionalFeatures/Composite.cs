using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : MonoBehaviour
{
    private MirrorScriptableObject mirrorResult;
    [SerializeField]private MirrorScriptableObject ClapboardAndLensMirror;
    [SerializeField]private MirrorScriptableObject LensAndReflectMirror;
    [SerializeField]private MirrorScriptableObject ReflectAndClapBoardMirror;
    [SerializeField]private MirrorScriptableObject ReflectAndDispersingMirror;
    [SerializeField]private LayerMask layerMask;

    MirrorScriptableObject GetMirrorScriptableObject(MirrorType mirrorType){
        switch(mirrorType){
            case MirrorType.ClapboardAndLensMirror:
            return ClapboardAndLensMirror;
            break;
            case MirrorType.LensAndReflectMirror:
            return LensAndReflectMirror;
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
        if((firstMirror == MirrorType.Clapboard && secondMirror == MirrorType.LensMirror) || 
           (firstMirror == MirrorType.LensMirror && secondMirror == MirrorType.Clapboard)
        ){
            return GetMirrorScriptableObject(MirrorType.ClapboardAndLensMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.LensMirror) || 
                 (firstMirror == MirrorType.LensMirror && secondMirror == MirrorType.ReflectMirror)
        ){
            return GetMirrorScriptableObject(MirrorType.LensAndReflectMirror);
        }else if((firstMirror == MirrorType.ReflectMirror && secondMirror == MirrorType.Clapboard) || 
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


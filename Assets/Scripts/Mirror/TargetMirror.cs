using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetMirror : BaseMirror
{
    public GameObject gameObject;
    public GameObject gameObjec;
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        //! todo �ж���ɫ
        Debug.Log("ʤ��");
        gameObjec.SetActive(false);
        gameObject.SetActive(true);
    }
}

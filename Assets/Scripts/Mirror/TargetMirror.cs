using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetMirror : BaseMirror
{
    public GameObject gameObject;
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        //! todo ÅÐ¶ÏÑÕÉ«
        Debug.Log("Ê¤Àû");
        gameObject.SetActive(true);
    }
}

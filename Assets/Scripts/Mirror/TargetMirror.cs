using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetMirror : BaseMirror
{
    
    public GameObject gameObject;
    public GameObject gameObjec;

    public Color color;
    public override void Ray(Vector2 startPosition, Vector2 endPosition, int index, Color color)
    {
        Debug.Log(color + "000");
        Debug.Log(this.color);
        //!todo color is right?
        if (color == this.color)
        {
            gameObjec.SetActive(false);
            gameObject.SetActive(true);  
        }

    }
}

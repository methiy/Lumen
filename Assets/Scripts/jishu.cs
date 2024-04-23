using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class jishu : MonoBehaviour
{
    private int collisionCount = 0;
    public Text collisionText; 

    private void Start()
    {
        UpdateCollisionText(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionCount++;
        UpdateCollisionText(); 
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        collisionCount--;
        UpdateCollisionText(); 
    }

    private void UpdateCollisionText()
    {
        if (collisionText != null)
        {
            collisionText.text = "" + collisionCount;
        }
    }
}


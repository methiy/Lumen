using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class QuitGame : MonoBehaviour
{
     void OnClick()
    {
        Debug.Log("exit");
        Application.Quit(); 
    }
}
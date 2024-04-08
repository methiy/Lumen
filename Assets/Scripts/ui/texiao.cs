using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class texiao : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnClick()
    {
        animator.SetTrigger("start");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

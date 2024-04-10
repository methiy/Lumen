using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask : MonoBehaviour
{
    public GameObject maska;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 pos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
       pos.z=0;
        if(Input.GetMouseButton(0))
        {
            Instantiate(maska,pos,Quaternion.identity,transform);
        }
    }
}

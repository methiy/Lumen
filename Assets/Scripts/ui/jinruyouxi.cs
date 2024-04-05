using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choose : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
       
    }

        // 调用延迟加载方法

    

    // 延迟加载场景的方法
    IEnumerator LoadSceneAfterDelay()
    {
        // 等待两秒
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("11");
    }

    void OnClick()
    {
        StartCoroutine(LoadSceneAfterDelay());
        
    }

    // Update is called once per frame
    void Update () {

    }
}


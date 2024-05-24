using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class continuee : MonoBehaviour
{
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
       
    }
    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene(LocalConfig.LoadUserData("user").currentLevel);
    }

    void OnClick()
    {
        StartCoroutine(LoadSceneAfterDelay());
        
    }
}

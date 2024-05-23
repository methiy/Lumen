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
    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);
        // SceneManager.LoadScene("shipin");
        SceneManager.LoadScene(LocalConfig.LoadUserData("user").currentLevel);
    }

    void OnClick()
    {
        StartCoroutine(LoadSceneAfterDelay());
        
    }

    // Update is called once per frame
    void Update () {

    }
}


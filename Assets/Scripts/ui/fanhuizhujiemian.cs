using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fanhuizhujiemian : MonoBehaviour
{
	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Update is called once per frame
    void Update () {

    }
}

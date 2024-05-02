using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level2 : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene("level2");
    }
}

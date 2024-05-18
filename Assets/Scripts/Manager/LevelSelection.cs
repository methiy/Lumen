using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private int LevelAmount;
    [SerializeField] private GameObject[] images;

    private static LevelSelection instance;
    public static LevelSelection Instance => instance;

    private UserData data;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            data = LocalConfig.LoadUserData("user");
            if (data == null){
                data = new UserData();
            } 
            // GetLevelStatus();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetLevelStatus()
    {
        for (int i = 1; i <= LevelAmount; i++)
        {
            string _LevelName = "Level " + i;
            bool status = data.LevelStatus[_LevelName];
            images[i].SetActive(status);
        }
    }

    public void setLevelStatus(string _LevelName)
    {
        data.currentLevel = _LevelName;
        if (data.LevelStatus.ContainsKey(_LevelName))
        {
            data.LevelStatus[_LevelName] = true;
        }
        else
        {
            data.LevelStatus.Add(_LevelName, true);
        }
        Save();
    }

    public void TryPressSelection(string _LevelName)
    {
        if (data.LevelStatus[_LevelName])
        {
            SceneManager.LoadScene(_LevelName);
        }
    }

    private void Save()
    {
        LocalConfig.SaveUserData(data);
    }
}

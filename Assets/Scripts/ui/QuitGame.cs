using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("退出游戏");
        Application.Quit(); // 在游戏中退出
    }
}
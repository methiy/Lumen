using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class shipin : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer���δ���䣡");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // ����Ƶ�������ʱ���õķ���
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("xinshoujiaocheng");
    }
}

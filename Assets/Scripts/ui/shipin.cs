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
            Debug.LogError("VideoPlayer组件未分配！");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // 当视频播放完毕时调用的方法
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("xinshoujiaocheng");
    }
}

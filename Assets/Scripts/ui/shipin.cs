using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class shipin : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // 确保VideoPlayer组件已分配
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer组件未分配！");
            return;
        }

        // 注册视频播放完毕的回调函数
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // 当视频播放完毕时调用的方法
    void OnVideoFinished(VideoPlayer vp)
    {
        // 停止视频播放
        vp.Stop();
        // 关闭视频
        gameObject.SetActive(false);
    }
}

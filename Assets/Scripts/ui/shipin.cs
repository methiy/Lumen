using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class shipin : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // ȷ��VideoPlayer����ѷ���
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer���δ���䣡");
            return;
        }

        // ע����Ƶ������ϵĻص�����
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // ����Ƶ�������ʱ���õķ���
    void OnVideoFinished(VideoPlayer vp)
    {
        // ֹͣ��Ƶ����
        vp.Stop();
        // �ر���Ƶ
        gameObject.SetActive(false);
    }
}

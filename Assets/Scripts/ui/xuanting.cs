using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ȷ����ʵ������Ҫ�Ľӿ�
public class xuanting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject aa; // ��Ҫ��ʾ�����ص�GameObject
    public bool isshow = false; // ������ʾ״̬�Ĳ�������

    // �������ͣ����ʱ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        isshow = true; // ����Ϊtrue��ʾGameObject
        aa.SetActive(isshow); // ����isshow��ֵ��ʾ������aa
    }

    // �������ͣ�˳�ʱ����
    public void OnPointerExit(PointerEventData eventData)
    {
        isshow = false; // ����Ϊfalse����GameObject
        aa.SetActive(isshow); // ����isshow��ֵ��ʾ������aa
    }
}

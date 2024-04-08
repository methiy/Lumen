using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 确保类实现了需要的接口
public class xuanting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject aa; // 需要显示或隐藏的GameObject
    public bool isshow = false; // 控制显示状态的布尔变量

    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        isshow = true; // 设置为true显示GameObject
        aa.SetActive(isshow); // 根据isshow的值显示或隐藏aa
    }

    // 当鼠标悬停退出时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        isshow = false; // 设置为false隐藏GameObject
        aa.SetActive(isshow); // 根据isshow的值显示或隐藏aa
    }
}

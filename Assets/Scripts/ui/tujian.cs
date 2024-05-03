using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMovement : MonoBehaviour
{
    public float moveDistance = 153f; // 移动距离
    public float moveSpeed = 5f; // 移动速度
    public float buttonCooldown = 0.7f; // 按钮冷却时间

    private RectTransform rectTransform;
    private bool canInteract = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // 获取左右按钮并绑定点击事件
        Button leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        leftButton.onClick.AddListener(MoveLeft);

        Button rightButton = GameObject.Find("RightButton").GetComponent<Button>();
        rightButton.onClick.AddListener(MoveRight);
    }

    // 向左移动游戏对象
    void MoveLeft()
    {
        if (canInteract)
        {
            MoveObject(-moveDistance);
            StartCoroutine(ButtonCooldown());
        }
    }

    // 向右移动游戏对象
    void MoveRight()
    {
        if (canInteract)
        {
            MoveObject(moveDistance);
            StartCoroutine(ButtonCooldown());
        }
    }

    // 移动游戏对象
    void MoveObject(float distance)
    {
        Vector3 targetPosition = rectTransform.position + new Vector3(distance, 0f, 0f);
        StartCoroutine(MoveCoroutine(targetPosition));
    }

    // 协程实现平滑移动
    IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(rectTransform.position, targetPosition) > 0.01f)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // 按钮冷却协程
    IEnumerator ButtonCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(buttonCooldown);
        canInteract = true;
    }
}

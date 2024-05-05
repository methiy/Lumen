using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMovement : MonoBehaviour
{
    public float moveDistance = 153f; // �ƶ�����
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public float buttonCooldown = 0.7f; // ��ť��ȴʱ��

    private RectTransform rectTransform;
    private bool canInteract = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // ��ȡ���Ұ�ť���󶨵���¼�
        Button leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        leftButton.onClick.AddListener(MoveLeft);

        Button rightButton = GameObject.Find("RightButton").GetComponent<Button>();
        rightButton.onClick.AddListener(MoveRight);
    }


    void MoveLeft()
    {

        float currentPosition = rectTransform.position.x;
        Debug.Log(currentPosition);
        if (currentPosition >-2200 && canInteract)
        {
            MoveObject(-moveDistance);
            StartCoroutine(ButtonCooldown());
        }
    }

    void MoveRight()
    {
        float currentPosition = rectTransform.position.x;
        Debug.Log(currentPosition);
        if (currentPosition <= 2.75606 && canInteract)
        {
            MoveObject(moveDistance);
            StartCoroutine(ButtonCooldown());
        }
    }

    // �ƶ���Ϸ����
    void MoveObject(float distance)
    {
        Vector3 targetPosition = rectTransform.position + new Vector3(distance, 0f, 0f);
        StartCoroutine(MoveCoroutine(targetPosition));
    }

    IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(rectTransform.position, targetPosition) > 0.01f)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ButtonCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(buttonCooldown);
        canInteract = true;
    }
}

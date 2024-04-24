using UnityEngine;

public class reflact : MonoBehaviour
{
    public GameObject objectToShow; 
    private bool isHovering = false; 
    private float hoverTimer = 0f;

    private void Start()
    {
        objectToShow.SetActive(false);
    }

    private void Update()
    {
        if (isHovering)
        {
            hoverTimer += Time.deltaTime; 

            if (hoverTimer >= 1.5f) // 如果悬停时间超过1.5秒
            {
                objectToShow.SetActive(true); 
                
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; 

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                objectToShow.transform.position = worldPosition;
            }
        } 
        if (Input.GetMouseButtonDown(0)){
            objectToShow.SetActive(false);
        }
        
    }

    private void OnMouseEnter()
    {
        isHovering = true; 
    }

    private void OnMouseExit()
    {
        isHovering = false; 
        hoverTimer = 0f;
        objectToShow.SetActive(false); 
    }
}

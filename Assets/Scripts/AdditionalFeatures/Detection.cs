using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    [SerializeField] private GameObject playArea, iconArea;

    //! play Area
    [SerializeField] private List<Vector3> playPositionList = new List<Vector3>();
    //! icon Area
    [SerializeField] private List<Vector3> iconPositionList = new List<Vector3>();
    private const float OFFSET = 1.3f;
    public String PositionToString(Vector3 position){
        return position.ToString();
    }
    private void Awake()
    {
        foreach (Transform child in playArea.GetComponentInChildren<Transform>(true))
        {
            if (child != playArea)
            {
                playPositionList.Add(child.position);
            }
        }
        foreach (Transform child in iconArea.GetComponentInChildren<Transform>(true))
        {
            if (child != iconArea)
            {
                iconPositionList.Add(child.position);
            }
        }

    }

    public bool PlayPositionPlaceable(Vector3 mousePosition, out Vector3 location)
    {

        bool canLocation = false;
        location = Vector3.zero;

        foreach (Vector3 position in playPositionList)
        {
            if (Vector3.Distance(position, mousePosition) <= OFFSET)
            {
                location = position;
                canLocation = true;
                break;
            }
        }

        return canLocation;
    }

    public bool IconPositionPlaceable(Vector3 mousePosition, out Vector3 location)
    {

        bool canLocation = false;
        location = Vector3.zero;

        foreach (Vector3 position in iconPositionList)
        {
                if (Vector3.Distance(position, mousePosition) <= OFFSET)
                {
                    location = position;
                    canLocation = true;
                    break;
                }
        }

        return canLocation;
    }
}


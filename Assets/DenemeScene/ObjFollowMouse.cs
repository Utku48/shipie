using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjFollowMouse : MonoBehaviour
{

    private PlaceObjectOnGrid placeObjectOnGrid;
    public bool isOnGrid;
    
    void Start()
    {
        placeObjectOnGrid = FindAnyObjectByType<PlaceObjectOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGrid)
        {
            transform.position = placeObjectOnGrid.smoothMousePosition + new Vector3(0, 0.5f, 0);
        }
      
    }
}



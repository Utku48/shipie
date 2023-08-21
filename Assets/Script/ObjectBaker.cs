using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectBaker : MonoBehaviour
{

    public static ObjectBaker Instance { get; private set; }

    public List<NavMeshSurface> surfaces;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }
    private void Start()
    {

    }



    public void BakeRunTime()
    {

        for (int n = 0; n < surfaces.Count; n++)
        {
            surfaces[n].RemoveData();
            surfaces[n].BuildNavMesh();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterWalk : MonoBehaviour
{
    public static CharacterWalk Instance { get; private set; }

    private Vector3 target;
    public LayerMask clickOn;

    public NavMeshAgent agent;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        target = transform.position;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, clickOn))
            {
                Debug.Log("hit");
                agent.SetDestination(hitInfo.transform.position);
            }
        }
    }
}

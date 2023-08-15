using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterWalk : MonoBehaviour
{
    private Vector3 target;
    public LayerMask clickOn;

    private NavMeshAgent agent;

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
                agent.SetDestination(hitInfo.transform.position);
            }
        }
    }
}

using DG.Tweening;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class ClickToMove : MonoBehaviour
{
    public static ClickToMove Instance { get; private set; }

    public NavMeshAgent agent;

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

    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Dokunulan yere bir ışın gönder
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
                CharacterStart.Instance._anim.SetBool("isWalk", true);

            }
        }
        if (transform.position == agent.destination)
        {
            CharacterStart.Instance._anim.SetBool("isWalk", false);

        }
    }

}

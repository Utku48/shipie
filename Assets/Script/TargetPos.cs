using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPos : MonoBehaviour
{

    [SerializeField] private Transform[] ChangeTargetPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("circle"))
        {

        }
    }

}

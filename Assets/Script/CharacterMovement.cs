using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetPos;
    [SerializeField] private float speed;


    void Start()
    {


    }


    void FixedUpdate()
    {
        if (CharacterStart.Instance.swim)
        {
            Vector3 a = transform.position;
            Vector3 b = _targetPos.position;

            transform.position = Vector3.MoveTowards(a, b, speed);
        }

    }
}

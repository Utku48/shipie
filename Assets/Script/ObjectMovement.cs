using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    private Transform _targetPos;
    [SerializeField] private float speed = 1;


    private void Start()
    {

    }
    void Update()
    {

        Vector3 a = transform.position;
        Vector3 b = _targetPos.position;
        b.y = transform.position.y;
        transform.position = Vector3.MoveTowards(a, b, speed);
        transform.DOLookAt(b, 0.1f)
         .SetEase(Ease.Linear);


    }
}

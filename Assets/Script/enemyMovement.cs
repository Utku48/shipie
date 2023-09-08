using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = .75f;
    void Start()
    {
        target = GameObject.Find("PoolObjectsTarget").transform;
    }


    void Update()
    {
        float step = speed * Time.deltaTime;
        target.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

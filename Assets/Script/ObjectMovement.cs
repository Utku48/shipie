using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetPos;
    [SerializeField] private float speed;

    private Transform[] returnPos;

    private void Start()
    {
        returnPos = ObjectsinPool.Instance.PrefabSpawnPos;
        _targetPos = GameObject.Find("PoolEnd").transform;
    }
    void FixedUpdate()
    {
        if (CharacterStart.Instance.swim)
        {
            Vector3 a = transform.position;
            Vector3 b = _targetPos.position;
            b.y = transform.position.y;

            transform.position = Vector3.MoveTowards(a, b, speed);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("poolEnd"))
        {
            Debug.Log("Değdi");
            int randomIndex = Random.Range(0, returnPos.Length);
            Vector3 ReturnSpawnPosition = new Vector3(returnPos[randomIndex].position.x, 0.35f, returnPos[randomIndex].position.z);
            this.gameObject.transform.position = ReturnSpawnPosition;
        }

    }
}

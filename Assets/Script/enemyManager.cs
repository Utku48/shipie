using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemyInstPos;

    private void Start()
    {

        Instantiate(enemy, new Vector3(enemyInstPos.position.x, 0.25f, enemyInstPos.position.z), Quaternion.Euler(0, 0, 90));
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsinPool : MonoBehaviour
{
    public static ObjectsinPool Instance { get; private set; }

    [SerializeField] private float spawnInterval = 1; //Ne kadar sıklıkla spawn edecegiz

    [SerializeField] private ObjectPooling objectPool = null;

    public Transform[] PrefabSpawnPos;



    int counter = 0;
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

        StartCoroutine(SpawnObjectsWithDelay());

    }

    IEnumerator SpawnObjectsWithDelay()
    {
        for (int i = 0; i < objectPool.prefabsInPool.Count; i++)
        {
            GameObject obj = objectPool.GetPooledObject(counter++ % 2); // ObjectPool scriptinden GetPooledObject'i çağır

            int randomIndex = Random.Range(0, PrefabSpawnPos.Length);
            Vector3 spawnPosition = new Vector3(PrefabSpawnPos[randomIndex].position.x, 0.35f, PrefabSpawnPos[randomIndex].position.z);

            // Eğer PrefabSpawnPos'da obj varsa 1 saniye bekle
            if (PrefabSpawnPos[randomIndex].gameObject == obj)
            {

            }

            obj.transform.position = spawnPosition;
            yield return new WaitForSeconds(1.5f);
        }
    }

}

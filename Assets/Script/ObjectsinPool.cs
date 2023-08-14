using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsinPool : MonoBehaviour
{
    public static ObjectsinPool Instance { get; private set; }


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
    void LateUpdate()
    {


    }
    IEnumerator SpawnObjectsWithDelay()
    {
        int temp = objectPool.prefabsInPool.Count;
        for (int i = 0; i < temp; i++)
        {

            GameObject obj = objectPool.GetPooledObject(counter++ % 2); // ObjectPool scriptinden GetPooledObject'i çağır
            Debug.Log(obj.name);

            int randomIndex = Random.Range(0, PrefabSpawnPos.Length);
            Vector3 spawnPosition = new Vector3(PrefabSpawnPos[randomIndex].position.x, -0.15f, PrefabSpawnPos[randomIndex].position.z);


            obj.transform.position = spawnPosition;
            ObjectPooling.Instance.prefabsInPool.Remove(obj);

            yield return new WaitForSeconds(1.5f);
        }
    }

}
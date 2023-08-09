using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance { get; private set; }

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> PooledObject;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] public Pool[] pools = null;

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


        for (int i = 0; i < pools.Length; i++) //Havuz çin
        {
            pools[i].PooledObject = new Queue<GameObject>();

            for (int j = 0; j < pools[i].poolSize; j++) //Havuz içindeki objeler için
            {
                GameObject obj = Instantiate(pools[i].objectPrefab);
                obj.SetActive(false);
                pools[i].PooledObject.Enqueue(obj);
            }
        }
    }

    //Aşağıdaki kodlarda kullanmış oldugumuz ObjectType'larımız aslında hangi havuzdan obje çekeceğimizi söyler

    public GameObject GetPoolObject(int ObjectType)
    {
        if (ObjectType >= pools.Length)
            return null;

        if (pools[ObjectType].PooledObject.Count == 0)
            AddSizePool(5f, ObjectType);

        GameObject obj = pools[ObjectType].PooledObject.Dequeue();
        obj.SetActive(true);
        return obj;

    }
    public void SetPoolObject(GameObject pooledObject, int ObjectType)
    {
        if (ObjectType >= pools.Length)
            return;
        pools[ObjectType].PooledObject.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }

    //Belirlenen sayıdan fazla objeye ihtiyaç duyulursa
    public void AddSizePool(float amount, int ObjectType)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(pools[ObjectType].objectPrefab);
            obj.SetActive(false);

            pools[ObjectType].PooledObject.Enqueue(obj);
        }
    }

}

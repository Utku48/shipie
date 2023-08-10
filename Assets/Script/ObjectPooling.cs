using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<GameObject> prefabsInPool = new List<GameObject>();

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;  //Objelerin sırası
        public GameObject objectPrefab;
        public int poolCount; //Havuzda saklanacak obje sayisi
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {

        //pools[j] j kaç ise  o pool için sıradan çeker veya sıraya ekler??
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>(); //Sırayı olusturduk

            for (int i = 0; i < pools[j].poolCount; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);//obj'yi Enqueue ile sıraya soktuk
                prefabsInPool.Add(obj);
            }
        }



    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }


        GameObject obj = pools[objectType].pooledObjects.Dequeue(); //Sıranın basındaki elemani al ve çagır

        obj.SetActive(true);

        pools[objectType].pooledObjects.Enqueue(obj);//Sıranın basındaki objeyi aldıktan sonra tekrar sıraya soktuk
  


        return obj;
    }

}

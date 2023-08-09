using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsinPool : MonoBehaviour
{
    public static ObjectsinPool Instance { get; private set; }

    public Queue<GameObject> objs1 = new Queue<GameObject>();
    [SerializeField] Transform startPos;


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
    }
    private void Update()
    {
        for (int i = 0; i < objs1.Count; i++)
        {
            GameObject obj1 = ObjectPooling.Instance.GetPoolObject(0);//ilk havuzdan obje çekiyoruz
            obj1.transform.position = startPos.position;
            objs1.Enqueue(obj1);//Kuyruga geri ekledik
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("poolEnd"))
        {
            GameObject obj1 = objs1.Dequeue();//Dequeue() metodu, Queue'dan en üstteki (ilk eklenen) elemanı çıkarır.
            ObjectPooling.Instance.SetPoolObject(obj1, 0);
        }
    }
}

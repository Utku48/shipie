using UnityEngine;
using System.Collections;
using UnityEditorInternal;
using Unity.VisualScripting;
using Tayx.Graphy.Utils;

public class ObjectMovement : MonoBehaviour
{

    [SerializeField] private Transform _targetPos;
    [SerializeField] private float speed;

    private Transform[] returnPos;
    public bool collectable = false;

    private void Start()
    {

        returnPos = ObjectsinPool.Instance.PrefabSpawnPos;
        _targetPos = GameObject.Find("PoolEnd").transform;

    }

    void FixedUpdate()
    {
        if (CharacterStart.Instance.swim)
        {
            Vector3 targetPosition = new Vector3(_targetPos.position.x, transform.position.y, _targetPos.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (ObjectPooling.Instance.prefabsInPool.Count == 0)
        {
            if (other.gameObject.CompareTag("poolEnd"))
            {
                Debug.Log("Değdi");

                int randomIndex = Random.Range(0, returnPos.Length);
                Vector3 ReturnSpawnPosition = new Vector3(returnPos[randomIndex].position.x, -0.15f, returnPos[randomIndex].position.z);

                StartCoroutine(Delay(ReturnSpawnPosition));
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("circle"))
        {
            collectable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("circle"))
        {
            collectable = false;
        }
    }


    IEnumerator Delay(Vector3 returnPos)
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.transform.position = returnPos;
    }
}

using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BombMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0f;
    public Animator _anim;
    private void Start()
    {
        target = GameObject.Find("PoolObjectsTarget").transform;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        target.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("palet"))
        {

            _anim.SetBool("isbomb", true);
            Destroy(gameObject, 1f);

            if (other.gameObject.CompareTag("palet"))
            {
                Debug.Log("palet");
                Vector3 targetPosition = new Vector3(transform.position.x, (transform.position.y - 1f), transform.position.z);
                other.gameObject.transform.DOMove(targetPosition, 1.5f);
                other.gameObject.transform.DOScale(Vector3.zero, 2.5f);

            }
        }
        IEnumerator PaletDestroy()
        {
            yield return new WaitForSeconds(2f);
        
        }
    }
}


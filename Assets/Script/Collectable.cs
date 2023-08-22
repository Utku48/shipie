using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public LayerMask collectableLayer;

    [SerializeField] private Transform[] restartPos;

    [SerializeField] private ParticleSystem _coinParticule;

    private void Start()
    {
        restartPos = ObjectsinPool.Instance.PrefabSpawnPos;
    }


    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // İlk dokunma olayını al

            if (touch.phase == TouchPhase.Began) // Dokunma başladığında
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position); // Dokunulan yere bir ışın gönder

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, collectableLayer))
                {
                    if (hit.collider != null && hit.collider.CompareTag("trash"))
                    {
                        ObjectMovement objectMovement = hit.collider.GetComponent<ObjectMovement>();

                        if (objectMovement != null && objectMovement.collectable == true)
                        {
                            _coinParticule.transform.position = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                            _coinParticule.Play();

                            int randomIndex = Random.Range(0, restartPos.Length);
                            Vector3 spawnPosition = new Vector3(restartPos[randomIndex].position.x, -0.15f, restartPos[randomIndex].position.z);
                            ScoreManager.money += 10;

                            hit.collider.gameObject.SetActive(false);
                            objectMovement.collectable = false;


                            StartCoroutine(CollectDelay(hit.collider.gameObject, spawnPosition));

                        }
                        else
                            Debug.Log("collectable false");
                    }
                }
            }
        }
        IEnumerator CollectDelay(GameObject obj, Vector3 spwn)
        {
            yield return new WaitForSeconds(1.75f);

            obj.gameObject.transform.position = spwn;
            obj.SetActive(true);


        }

    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class ClickToMove : MonoBehaviour
{
    public static ClickToMove Instance { get; private set; }

    public NavMeshAgent agent;

    [SerializeField] public LayerMask Palett;
    [SerializeField] public Transform character;

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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Dokunulan yere bir ışın gönder
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Palett))
            {
                agent.SetDestination(hit.point);
                CharacterStart.Instance._anim.SetBool("isWalk", true);
                float distance = Vector3.Distance(transform.position, hit.point);

                if (distance <= 1.5f)
                {
                    CharacterStart.Instance._anim.SetBool("isWalk", false);
                }

            }
        }

    }

}

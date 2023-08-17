using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem.HID;

public class ClickToMove : MonoBehaviour
{

    [SerializeField] private LayerMask Palet;
    private void Update()
    {


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // İlk dokunma olayını al

            if (touch.phase == TouchPhase.Began) // Dokunma başladığında
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position); // Dokunulan yere bir ışın gönder

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Palet))
                {
                    if (hit.collider != null && hit.collider.CompareTag("palet"))
                    {

                        StartCoroutine(GoClickPos(hit));
                    }
                    else Debug.Log("palete dokun");
                }
            }
        }

    }

    IEnumerator GoClickPos(RaycastHit hitTransform)
    {
        gameObject.transform.position = new Vector3(hitTransform.transform.position.x, transform.position.y, hitTransform.transform.position.z);

        yield return new WaitForSeconds(5f);
    }


}

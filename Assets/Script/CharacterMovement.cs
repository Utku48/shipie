using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterMovement : MonoBehaviour
{
    public Transform fallPos;
    public Transform swimPos;
    public Animator anim;

    void Start()
    {
        this.gameObject.transform.DOMove(fallPos.position, 2f).OnComplete(() =>
        {
            // Belirli bir süre sonra ikinci hedefe hareket
            DOVirtual.DelayedCall(1f, () =>
            {
                this.gameObject.transform.DOMove(swimPos.position, 2f);
                anim.SetBool("isSwim", true);
            });
        });
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sea"))
        {
            
        }
    }
}

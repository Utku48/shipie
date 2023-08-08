using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CharacterStart : MonoBehaviour
{
    public static CharacterStart Instance { get; private set; }

    public Transform fallPos;
    public Transform swimPos;
    public Animator anim;

    [SerializeField] private GameObject _circle;

    public bool swim = false;
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

        _circle.SetActive(false);
        this.gameObject.transform.DOMove(fallPos.position, 2f).OnComplete(() =>
        {
            // Belirli bir süre sonra ikinci hedefe hareket
            DOVirtual.DelayedCall(1f, () =>
            {
                this.gameObject.transform.DOMove(swimPos.position, 2f);
                anim.SetBool("isSwim", true);
                _circle.SetActive(true);
                swim = true;

            });
        });

    }


}




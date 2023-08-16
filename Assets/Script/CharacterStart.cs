using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CharacterStart : MonoBehaviour
{
    public static CharacterStart Instance { get; private set; }

    [SerializeField] private Transform _fallPos;
    [SerializeField] private Transform _swimPos;
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject _circle;
    [SerializeField] private ParticleSystem _splash;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _source2;


    public bool swim = false;
    private void Awake()
    {
        _source2.Play();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _circle.SetActive(false);
        this.gameObject.transform.DOMove(_fallPos.position, 2f).OnComplete(() =>
        {
            // Belirli bir süre sonra ikinci hedefe hareket
            DOVirtual.DelayedCall(1f, () =>
            {
                this.gameObject.transform.DOMove(_swimPos.position, 2f);
                _anim.SetBool("isSwim", true);
                _circle.SetActive(true);
                swim = true;

            });
        });

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sea"))
        {
            StartCoroutine(StartParticule());
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("palet"))
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, 0.12f, transform.position.z);
            _anim.SetBool("isPalet", true);

            swim = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("palet"))
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _anim.SetBool("isPalet", false);
            swim = true;
        }
    }

    IEnumerator StartParticule()
    {
        yield return new WaitForSeconds(0.5f);
        _source2.Stop();
        _splash.Play();
        _source.Play();

    }

}




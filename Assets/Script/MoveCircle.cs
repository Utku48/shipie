using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class MoveCircle : MonoBehaviour
{
    [SerializeField] private GameObject character;

    private void Update()
    {
        transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
    }

}

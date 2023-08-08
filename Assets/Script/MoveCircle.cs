using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class MoveCircle : MonoBehaviour
{


    void Start()
    {

        transform.DORotate(new Vector3(-90.0f, 360.0f, 0.0f), 2f).SetLoops(-1, LoopType.Restart);
    }



}

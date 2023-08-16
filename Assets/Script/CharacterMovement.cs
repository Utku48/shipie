using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : MonoBehaviour
{
    

    [SerializeField] private Transform _targetPos;
    [SerializeField] private float speed;


    void FixedUpdate()
    {
        
            if (CharacterStart.Instance.swim)
            {
                Vector3 a = transform.position;
                Vector3 b = _targetPos.position;
                b.y = transform.position.y;

                transform.position = Vector3.MoveTowards(a, b, speed);
            transform.DOLookAt(b, 1f)
             .SetEase(Ease.Linear);
        }

        }
    
}

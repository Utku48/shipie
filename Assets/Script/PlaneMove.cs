using DG.Tweening;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public Transform _targetPos;
    public float movementSpeed;
    public Transform character;

    private void FixedUpdate()
    {
        if (CharacterStart.Instance.swim)
        {
            transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        }
        if (!CharacterStart.Instance.swim)
        {
            Vector3 a = transform.position;
            Vector3 b = _targetPos.position;
            b.y = transform.position.y;

            transform.position = Vector3.MoveTowards(a, b, movementSpeed);
            transform.DOLookAt(b, 1f)
             .SetEase(Ease.Linear);
        }

    }
}

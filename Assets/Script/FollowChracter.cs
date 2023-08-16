using UnityEngine;

public class FollowChracter : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, transform.position, smoothSpeed);
        transform.position = smoothedPosition;

    }
}

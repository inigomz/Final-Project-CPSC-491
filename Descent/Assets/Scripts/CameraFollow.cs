using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(smooth.x, smooth.y, transform.position.z);
    }
}
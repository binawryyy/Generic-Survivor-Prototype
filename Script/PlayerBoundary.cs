using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public float xLimit = 420f;
    public float zLimit = 420f;

    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, -xLimit, xLimit);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -zLimit, zLimit);

        transform.position = currentPosition;
    }
}
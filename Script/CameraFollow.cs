using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        if(PlayerController.Instance == null) return;
        if (offset == Vector3.zero)
        {
            offset = transform.position - PlayerController.Instance.transform.position;
        }
        transform.position = PlayerController.Instance.transform.position + offset;
    }
}

using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    [Header("Bullet Stats")]
    private float boundDestroy = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.Instance == null) return;
        BulletLaunch();
        
        // DESTROY OUT OF BOUNDS
        Vector3 playerPos = PlayerController.Instance.transform.position;
        if (transform.position.z >= playerPos.z + boundDestroy || transform.position.z <= playerPos.z - boundDestroy)
        {
            Destroy(gameObject);
        }
        if (transform.position.x >= playerPos.x + boundDestroy || transform.position.x <= playerPos.x - boundDestroy)
        {
            Destroy(gameObject);
        }
    }
    void BulletLaunch()
    {
        transform.Translate(Vector3.forward * PlayerController.Instance.bulletSpeed * Time.deltaTime);
    }
    
    
}

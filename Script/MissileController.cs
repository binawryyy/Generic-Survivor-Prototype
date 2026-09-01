using UnityEngine;

public class MissileController : MonoBehaviour
{
    private float boundDestroy = 300f;
    private float turretBulletSpeed = 75f;
    private Vector3 startPosition;

    public void SetBulletSpeed(float speed)
    {
        turretBulletSpeed = speed;
    }
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TurretBulletLaunch();
        // DESTROY OUT OF BOUNDS
        if(Vector3.Distance(transform.position, startPosition) >= boundDestroy)
        {
            Destroy(gameObject);
        }
    }

    void TurretBulletLaunch()
    {
        transform.Translate(Vector3.forward * turretBulletSpeed * Time.deltaTime);
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.UpdateHpDamaged(10);
                Destroy(gameObject);
            }
        }
    }
    */
}

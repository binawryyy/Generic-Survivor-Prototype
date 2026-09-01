using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float enemyHP;
    public float currentHP;
    public float damageInterval = 1f;
    public float nextDamage;
    public GameObject interior;
    private float interiorSpeed = 50f;

    [Header("Enemy Navigation")]
    private NavMeshAgent navMeshAgent;

    [Header("Enemy Sound Effect")]
    public AudioClip damagedSound;
    public AudioClip destroySound;
    public AudioClip playerDamagedSound;
    public AudioClip playerDestroySound;


    [Range(0f, 1f)] 
    public float soundVolume = 0.8f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHP = enemyHP;
    }

   
    void Update()
    {
        if (PlayerController.Instance != null)
        {
            navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
            interior.transform.Rotate(Vector3.up * Time.deltaTime * interiorSpeed);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurretBullet"))
        {
            if (TurretController.Instance != null)
            {
                //AudioSource.PlayClipAtPoint(damagedSound, Camera.main.transform.position, soundVolume);
                currentHP -= TurretController.Instance.turretBulletDamage;
            }
        }
        else if (other.CompareTag("Bullet"))
        {
            if (PlayerController.Instance != null)
            {   
                AudioSource.PlayClipAtPoint(damagedSound, Camera.main.transform.position, soundVolume);
                currentHP -= PlayerController.Instance.bulletDamage;
                Destroy(other.gameObject); 
            }
        }
        else if (other.CompareTag("Ultimate"))
        {
            if (PlayerController.Instance != null)
            {   
                currentHP -= PlayerController.Instance.ultDamage;
                Destroy(other.gameObject); 
            } 
        }
        if (currentHP <= 0)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, soundVolume);
            Destroy(gameObject);
            if(GameManager.Instance != null)
            {
                GameManager.Instance.UpdateScore(1000);
            }
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.AddExp(1);
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        
            if (other.CompareTag("Player"))
            {
                if(Time.time >= nextDamage)
                {
                    if(PlayerController.Instance != null)
                    {
                        
                        PlayerController.Instance.UpdateHpDamaged(10);
                        AudioSource.PlayClipAtPoint(playerDamagedSound, Camera.main.transform.position, soundVolume);
                    }
                    nextDamage = Time.time + damageInterval;
                }
            }
    }
}

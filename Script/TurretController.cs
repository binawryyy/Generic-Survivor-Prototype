using UnityEngine;

public class TurretController : MonoBehaviour
{
    
    public static TurretController Instance;
    [Header("Turret Bullet")]
    public GameObject turretBullet;
    private GameObject currentBullet;
    public Transform turretBulletPos;
    public float turrentBulletSpeed = 50f;
    public float turretBulletDamage = 5f;
    public float turretBulletInterval = 1f;
    private float turretNextBullet;

    [Header("Turret Sound")]
    public AudioClip turretBulletSound;
    private AudioSource playerAudio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= turretNextBullet)
        {
            TurretBulletLaunch();
            turretNextBullet = Time.time + turretBulletInterval;
            playerAudio.clip = turretBulletSound;
            playerAudio.time = 4.0f;
            playerAudio.Play();
        }
    }
    public void TurretBulletLaunch()
    {
        if (turretBullet != null)
        {
            currentBullet = Instantiate(turretBullet, turretBulletPos.position, turretBulletPos.transform.rotation);
            currentBullet.GetComponent<TurretBulletController>().SetBulletSpeed(turrentBulletSpeed);
        }
        
        
    }
}

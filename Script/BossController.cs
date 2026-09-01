using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public static BossController Instance;
    [Header("Boss Weapons")]
    //ATTACK 1
    public GameObject missiles;
    public Transform missileLoc1;
    public Transform missileLoc2;
    //ATTACK 2
    public GameObject missileGiant;
    public Transform missileLoc3;

    //ATTACK 3
    public Transform missileLoc4;
    public Transform missileLoc5;
    public Transform missileLoc6;
    public Transform missileLoc7;

    [Header("Boss Stats")]
    public float enemyHP;
    public float currentHP;
    public float damageInterval = 0.5f;
    public float nextDamage;
    public float attacksInterval = 1.5f;
    public float nextAttack;

    [Header("Boss Navigation")]
    private NavMeshAgent navMeshAgent;

    [Header("Boss Sound Effect")]
    public AudioClip damagedSound;
    public AudioClip destroySound;
    public AudioClip playerDamagedSound;
    public AudioClip playerDestroySound;



    [Range(0f, 1f)] 
    public float soundVolume = 0.8f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHP = enemyHP;
    }

   
    void Update()
    {
        if (PlayerController.Instance != null)
        {
            navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
        }
        if (Time.time >= nextAttack)
        {
            BossAttacks();
            nextAttack = Time.time + attacksInterval;
        }
        
    }
    // DAMAGE FROM PLAYER
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurretBullet"))
        {
            if (TurretController.Instance != null)
            {
                AudioSource.PlayClipAtPoint(damagedSound, Camera.main.transform.position, soundVolume);
                currentHP -= TurretController.Instance.turretBulletDamage;
                BossUpdateHealth();
            }
        }
        else if (other.CompareTag("Bullet"))
        {
            if (PlayerController.Instance != null)
            {   
                AudioSource.PlayClipAtPoint(damagedSound, Camera.main.transform.position, soundVolume);
                currentHP -= PlayerController.Instance.bulletDamage;
                Destroy(other.gameObject); 
                BossUpdateHealth();
            }
        }
        else if (other.CompareTag("Ultimate"))
        {
            if (PlayerController.Instance != null)
            {   
                currentHP -= PlayerController.Instance.ultDamage + 315f;
                Destroy(other.gameObject);
                BossUpdateHealth();
            }
            
        }
        if (currentHP <= 0)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, soundVolume);
            Destroy(gameObject);
            if(GameManager.Instance != null)
            {
                GameManager.Instance.UpdateScore(50000);
                GameManager.Instance.statusText.text = "You Win!";
                GameManager.Instance.finalScoreText.text = GameManager.Instance.score + "";
                GameManager.Instance.playerAudio.Stop();
            }
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.AddExp(1);
            }
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.GameOver();
                MenuManager.Instance.YouWinMusic();
            }
            if (EnemySpawner.Instance != null)
            {
                EnemySpawner.Instance.playerAudio.Stop();
            }
        }
    }
    // DAMAGE TO PLAYER
    void OnTriggerStay(Collider other)
    {
        if(Time.time >= nextDamage)
        {
            if (other.CompareTag("Player"))
            {
                if(PlayerController.Instance != null)
                {
                    AudioSource.PlayClipAtPoint(playerDamagedSound, Camera.main.transform.position, soundVolume);
                    PlayerController.Instance.UpdateHpDamaged(20);
                }
            }
            nextDamage = Time.time + damageInterval;
        }
    }
    
    public void BossAttacks()
    {
        
        int kind = Random.Range(0, 3);
        switch (kind)
        {
            case 0:
                Instantiate(missiles, missileLoc1.position, missileLoc1.rotation);
                Instantiate(missiles, missileLoc2.position, missileLoc2.rotation);
            break;
            case 1:
                Instantiate(missileGiant, missileLoc3.position, missileLoc3.rotation);
            break;
            case 2:
                Instantiate(missiles, missileLoc4.position, missileLoc4.rotation);
                Instantiate(missiles, missileLoc5.position, missileLoc5.rotation);
                Instantiate(missiles, missileLoc6.position, missileLoc6.rotation);
                Instantiate(missiles, missileLoc7.position, missileLoc7.rotation);
            break;
        }
    }
    public void BossUpdateHealth()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateBossHealth(currentHP);
        }
    }
    // FORE RESETTING BOSS HP
    /*public void BossResetHp()
    {
        enemyHP = 15000f;
        currentHP = enemyHP;
    }
    */
    /*public void MissilesLaunch()
    {
        
        if (Time.time >= nextCurrentMissiles)
        {
            currentMissiles = Instantiate(missiles, missileLoc1.position, missileLoc1.rotation);
            currentMissiles = Instantiate(missiles, missileLoc2.position, missileLoc2.rotation);    
            nextCurrentMissiles = Time.time + currentMissilesInterval;
        }
        StartCoroutine(MissilesLaunchHangtime());
        
    }
    IEnumerator MissilesLaunchHangtime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(currentMissiles);
    }
    */

    // UNUSED IENUMERATOR
    /*public IEnumerator MissilesLaunch()
    {
    float endTime = Time.time + 5f;

    while (Time.time < endTime)
    {
        Instantiate(missiles, missileLoc1.position, missileLoc1.rotation);
        Instantiate(missiles, missileLoc2.position, missileLoc2.rotation);

        yield return new WaitForSeconds(0.5f);
    }
    }
    */


}

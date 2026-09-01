using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefabs;
    public GameObject currentBoss;
    public GameObject currentEnemy;
    public GameObject denominator;
    public float enemyInterval = 0.8f;
    public float enemyIntevalLimit = 0.1f;

    public AudioClip bossMusic;
    public AudioSource playerAudio;
    //public bool timeToBoss = false;
    //public bool bossSpawned = false;

    private float nextEnemy = 2f;
    private float spawnDistance = 80f;

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
        playerAudio = GetComponent<AudioSource>();
        
        Invoke("Denominator", 599f);
        Invoke("BossSpawner", 600f);
        InvokeRepeating("EnemyIntervalLimiter", 60f, 60f);
        
    }

    void Update()
    {
        if (Time.time >= nextEnemy)
        {
            EnemyPrefabSpawner();
            nextEnemy = Time.time + enemyInterval;
        }
    }
    public void EnemyIntervalLimiter()
    {
        if (enemyInterval - 0.2 <= enemyIntevalLimit)
        {
            enemyInterval = enemyIntevalLimit;
        }
        else
        {
            enemyInterval -= 0.2f;
        }
    }
    void EnemyPrefabSpawner()
    {
        if(PlayerController.Instance == null) return;
        
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 spawnPos = Vector3.zero;
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0:
                spawnPos.x = playerPos.x + Random.Range(-spawnDistance, spawnDistance);
                spawnPos.z = playerPos.z + spawnDistance;
            break;

            case 1:
                spawnPos.x = playerPos.x + Random.Range(-spawnDistance, spawnDistance);
                spawnPos.z = playerPos.z - spawnDistance;
            break;
            
            case 2:
                spawnPos.z = playerPos.z + Random.Range(-spawnDistance, spawnDistance);
                spawnPos.x = playerPos.x + spawnDistance;
            break;

            case 3:
                spawnPos.z = playerPos.z + Random.Range(-spawnDistance, spawnDistance);
                spawnPos.x = playerPos.x - spawnDistance;
            break;
        }
        if (GameManager.Instance != null)
        {
            
            if(GameManager.Instance.timerValue < 120)
            {
                currentEnemy = Instantiate(enemyPrefabs[0], spawnPos, enemyPrefabs[0].transform.rotation);
            }
            else if(GameManager.Instance.timerValue >= 120 && GameManager.Instance.timerValue < 240)
            {
                int enemyIndex = Random.Range(0, 2);
                currentEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }
            else if (GameManager.Instance.timerValue >= 240 && GameManager.Instance.timerValue < 360) 
            {
                int enemyIndex = Random.Range(1, 3);
                currentEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }
            else if (GameManager.Instance.timerValue >= 360 && GameManager.Instance.timerValue < 598)
            {
                int enemyIndex = Random.Range(2, 4);
                currentEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
    }
    

    void BossSpawner()
    {
        if(PlayerController.Instance == null) return;
        Vector3 enemyPos = PlayerController.Instance.transform.position;
        Vector3 enemySpawnPos = Vector3.zero;
        enemySpawnPos.z = enemyPos.z + spawnDistance;
        currentBoss = Instantiate(bossPrefabs, enemySpawnPos, bossPrefabs.transform.rotation);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.bossHPText.gameObject.SetActive(true);
            GameManager.Instance.playerAudio.Stop();
        }
        playerAudio.clip = bossMusic;
        playerAudio.Play();
    }
    public void Denominator()
    {
        Vector3 denominatorPos = Vector3.zero;
        Instantiate(denominator, denominatorPos, denominator.transform.rotation);
    }
}

using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.Multiplayer.PlayMode;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    #region PLAYER STATS
    [Header("Player Stats")]
    public float playerSpeed = 10f;
    public float playerSpeedLimit = 25f;
    public int playerExp;
    public int playerLvl;
    public int playerMaxHP;
    public int playerCurrentHP;
    #endregion

    #region PLAYER BULLET
    [Header("Player Bullet")]
    //LEVEL 1 BULLET
    public GameObject bullet;
    public Transform bulletPosition;

    //LEVEL 2 BULLET
    public GameObject bullet2;
    public Transform bulletPosition2;
    public Transform bulletPosition3;

    //LEVEL 2 BULLET
    public GameObject bullet3;
    public Transform bulletPosition4;
    public Transform bulletPosition5;
    public Transform bulletPosition6;
    
    
    public float bulletSpeed = 50f;
    public float bulletSpeedLimit = 200f;
    public float bulletDamage = 5f;
    public float bulletDamageLimit = 25f;
    public float bulletInterval = 2.3f;
    private float bulletIntervalLimit = 0.3f;
    private float nextBullet;
    #endregion
    
    #region PLAYER ARGUMENT
    [Header("Player Argument")]
    public int toLevel;
    public bool isLevel2 = false;
    public bool isLevel4 = false;
    public bool isLevel6 = false;
    #endregion

    #region PLAYER SKILLS
    [Header("Player Skills")]
    public GameObject torret;
    private GameObject currentTorret;
    public GameObject ultimate;
    public float torrentCD = 15f;
    public float healCD = 45f;
    public float ultCD = 60f;
    public bool canUseTurret = false;
    public bool canUseHeal = false;
    public bool canUseUlt = false;
    #endregion

    #region PLAYER ULTIMATE STATS
    [Header("Player Ultimate Stats")]
    
    public float ultDamage = 35f;
    #endregion

    #region PLAYER SOUND
    [Header("Player Sound")]
    public AudioClip bulletSound;
    public AudioClip levelUpSound;
    public AudioClip healSound;
    public AudioClip missileDamageSound;
    private AudioSource playerAudio;
    #endregion

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // HP STATS
        playerMaxHP = 100; // player hp value not included to instance argument
        playerExp = 0; // player hp value 
        playerLvl = 1; // player current level
        playerCurrentHP = playerMaxHP; // player current hp

        
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        // PLAYER SOUND EFFECTS
        playerAudio = GetComponent<AudioSource>();
        Debug.Log(bulletInterval);
        
    }
    void Update()
    {
        //PLAYERMOVEMENT
        PlayerMovement();
        //PLAYERBULLET
        if (Time.time >= nextBullet)
        {
            BulletShot();
            nextBullet = Time.time + bulletInterval;
        }
        // PLAYER LEVEL SKILLS
        if (playerLvl >= 2)
        {
            PlayerLevel2();
        }
        if (playerLvl >= 4)
        {
            PlayerLevel4();
        }
        if (playerLvl >= 6)
        {
            PlayerLevel6();
        }
        
    }
    
    // FOR LEVELING UP PLAYER METHOD
    public void AddExp(int expToAdd)
    {
        playerExp += expToAdd;
        toLevel = playerLvl * 8;
        float expPercentage = ((float)playerExp / toLevel) * 100f;
        if (playerExp >= toLevel)
        {
            
            LevelUp();

            
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdatePlayerExp(expPercentage);
        }
        
    }
    // PLAYER LEVEL UP METHOD
    public void LevelUp()
    {
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdatePlayerExp(0);
        }
        playerExp = 0;
        playerLvl++;
        //toLevel *= 2;
        
        
        UpdateMaxHP();
        BulletUpgrades();
        UpdatePlayerSpeed();
        if (playerCurrentHP + 10 >= playerMaxHP)
            {
                playerCurrentHP = playerMaxHP;
            }
            else
            {
                playerCurrentHP += 10;
            }
        if (playerLvl == 2)
        {   
            isLevel2 = true;
            canUseTurret = true;
            if(MenuManager.Instance != null)
            {
                MenuManager.Instance.Level2Player();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.jSkillText.text = "Ready";
                }
            }
        }
        if (playerLvl == 4)
        {   
            isLevel4 = true;
            canUseHeal = true;
            if(MenuManager.Instance != null)
            {
                MenuManager.Instance.Level4Player();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.kaySkillText.text = "Ready";
                }
            }
        }
        if (playerLvl == 6)
        {
            isLevel6 = true;
            canUseUlt = true;
            if(MenuManager.Instance != null)
            {
                MenuManager.Instance.Level6Player();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.lSkillText.text = "Ready";
                }
            }
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdatePlayerLevel(playerLvl);
            GameManager.Instance.UpdateCurrentHealth(playerCurrentHP);
            GameManager.Instance.ShowLevelUp();
            GameManager.Instance.playerAudio.Pause();
        }
        //PLAYS MUSIC AT SET TIME START
        StartCoroutine(AudioLogic());
        
    }
    // AUDIO LOGIC FOR PAUSE AND UNPAUSE AN AUDIO
    IEnumerator AudioLogic()
    {
        playerAudio.clip = levelUpSound;
        playerAudio.time = 1.0f;
        playerAudio.Play();
        yield return new WaitWhile(() => playerAudio.isPlaying);
        GameManager.Instance.playerAudio.UnPause();
    }
    // PLAYER ADDING MAX HP PER LEVEL METHOD
    public void UpdateMaxHP()
    {
        playerMaxHP += 5;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateMaxHealth(playerMaxHP);
        }
    }
    // PLAYER DAMAGE INFLICTED BY ENEMY
    public void UpdateHpDamaged(int damageToInflict)
    {
        playerCurrentHP-=damageToInflict;
        //Debug.Log(damageToInflict);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCurrentHealth(playerCurrentHP);
        }
        PlayerOnDeath();
    }
    // PLAYERSPEED
    public void UpdatePlayerSpeed()
    {
        if (playerSpeed + 0.5f >= playerSpeedLimit)
        {
            playerSpeed = playerSpeedLimit;
        }
        else
        {
            playerSpeed += 0.5f;
        }
    }
    // PLAYER DEATH
    public void PlayerOnDeath()
    {
        if (playerCurrentHP <= 0)
            {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.GameOver();
                MenuManager.Instance.GameOverMusic();
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.statusText.text = "GAMEOVER";
                GameManager.Instance.finalScoreText.text = GameManager.Instance.score + "";
                GameManager.Instance.playerAudio.Stop();
            }
            if (EnemySpawner.Instance != null)
            {
                EnemySpawner.Instance.playerAudio.Stop();
            }
            Destroy(gameObject);
            }
    }
    // PLAYER MOVEMENT METHOD
    void PlayerMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        if (input.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(input);
            transform.Translate(input * Time.deltaTime * playerSpeed, Space.World);
        }
    }
    // PLAYER BULLET UPGRADES
    public void BulletUpgrades()
    {

        if(bulletDamage + 1f >= bulletDamageLimit)
        {
            bulletDamage = bulletDamageLimit;
        }
        else
        {
            bulletDamage += 1f;
        }
        if (bulletSpeed + 5f >= bulletSpeedLimit)
        {
            bulletSpeed = bulletSpeedLimit;
        }
        else
        {bulletSpeed += 5f;
            
        }
        
        if(bulletInterval - 0.1f <= bulletIntervalLimit)
        {
            bulletInterval = bulletIntervalLimit;
        }
        else
        {
            bulletInterval -= 0.1f;
        }
        
    }
    // BULLET LAUNCHING METHOD
    void BulletShot()
    {
        if (playerLvl < 10)
        {
            Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
            playerAudio.PlayOneShot(bulletSound, 1.0f);
        }
        else if (playerLvl >= 10 && playerLvl < 20)
        {
            Instantiate(bullet2, bulletPosition2.position, bulletPosition2.rotation);
            Instantiate(bullet2, bulletPosition3.position, bulletPosition3.rotation);
            playerAudio.PlayOneShot(bulletSound, 1.0f);
        }
        else if (playerLvl >= 20)
        {
            Instantiate(bullet3, bulletPosition4.position, bulletPosition4.rotation);
            Instantiate(bullet3, bulletPosition5.position, bulletPosition5.rotation);
            Instantiate(bullet3, bulletPosition6.position, bulletPosition6.rotation);
            playerAudio.PlayOneShot(bulletSound, 1.0f);
        }
        
    }
    // PLAYER HEALING METHOD // Missile Damages
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile") && Instance != null)
        {
            UpdateHpDamaged(10);
            playerAudio.PlayOneShot(missileDamageSound, 1.0f);
            Destroy(other.gameObject);
            
        }
        else if (other.CompareTag("MissileGiant") && Instance != null)
        {
            UpdateHpDamaged(20);
            playerAudio.PlayOneShot(missileDamageSound, 1.0f);
            Destroy(other.gameObject);
        }
        
        /*if (other.CompareTag("SmallHeal"))
        {
            if(GameManager.Instance != null){
            if (playerCurrentHP + 20 > playerMaxHP)
            {
                playerCurrentHP = playerMaxHP;
                Destroy(other.gameObject);
                GameManager.Instance.UpdateCurrentHealth(playerCurrentHP);
            }
            else
            {
                playerCurrentHP+=20;
                Destroy(other.gameObject);
                GameManager.Instance.UpdateCurrentHealth(playerCurrentHP);
            }
            }
        }
        */
    }
    // PLAYER RESET
    /*public void PlayerReset()
    {
        playerSpeed = 10f;
        playerExp = 0;
        playerLvl = 1;
        playerMaxHP = 100;
        bulletSpeed = 50f;
        bulletDamage = 5f;
        bulletInterval = 2.3f;
    }
    */
    // PLAYER LEVEL 2
    public void PlayerLevel2()
    { 
            if (isLevel2 && canUseTurret && Input.GetKeyDown(KeyCode.J))
            {
                currentTorret = Instantiate(torret, bulletPosition.position, bulletPosition.transform.rotation);
                StartCoroutine(HideTorret());
                canUseTurret = false;
                if (GameManager.Instance != null)
                {
                 GameManager.Instance.jSkillText.text = "Activated";
                }
                
            }  
    }
    IEnumerator HideTorret()
    {
        yield return new WaitForSeconds(5f);
        Destroy(currentTorret);
        StartCoroutine(TurretCooldown());
        if (GameManager.Instance != null)
            {
                GameManager.Instance.jSkillText.text = "Not Ready";
            }
        
    }
    IEnumerator TurretCooldown()
    {
        yield return new WaitForSeconds(torrentCD);
        canUseTurret = true;
        if (GameManager.Instance != null)
                {
                    GameManager.Instance.jSkillText.text = "Ready";
                }
    }   
    // PLAYER LEVEL 4
    public void PlayerLevel4()
    {
        if (isLevel4 && canUseHeal && Input.GetKeyDown(KeyCode.K))
        {
            if (playerCurrentHP + 40 >= playerMaxHP)
            {
                playerCurrentHP = playerMaxHP;
            }
            else
            {
                playerCurrentHP += 40;
            }
            playerAudio.clip = healSound;
            playerAudio.time = 1.0f;
            playerAudio.Play();
            StartCoroutine(HealCooldown());
            canUseHeal = false;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.kaySkillText.text = "Not Ready";
                GameManager.Instance.UpdateCurrentHealth(playerCurrentHP);
            }
        }
    }
    IEnumerator HealCooldown()
    {
        yield return new WaitForSeconds(healCD);
        canUseHeal = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.kaySkillText.text = "Ready";
        }
    }
    // PLAYER LEVEL 6
    public void PlayerLevel6()
    {
        if (isLevel6 && canUseUlt && Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(ultimate, transform.position, transform.rotation);
            StartCoroutine(UltCooldown());
            canUseUlt = false;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.lSkillText.text = "Not Ready";
            }
        }
    }
    
    IEnumerator UltCooldown()
    {
        yield return new WaitForSeconds(ultCD);
        canUseUlt = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.lSkillText.text = "Ready";
        }
    }
}

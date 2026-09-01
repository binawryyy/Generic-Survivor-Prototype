using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI maxhealthText;
    public TextMeshProUGUI playerexpText;
    public TextMeshProUGUI playerlevelText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelUp;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI jSkillText;
    public TextMeshProUGUI kaySkillText;
    public TextMeshProUGUI lSkillText;
    public TextMeshProUGUI bossHPText;

    public AudioClip gameMusic;
    public AudioSource playerAudio;

    public int score;
    public int health;
    public int maxHealth;
    public int playerExp;
    public int playerLevel;
    public float timerValue;
    //public float jSkillTimerValue;
    //public float kSkillTimerValue;
    //public float lSkillTimerValue;

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
        jSkillText.text = "Not Available";
        kaySkillText.text = "Not Available";
        lSkillText.text = "Not Available"; 
        UpdateScore(0);
            if (PlayerController.Instance != null)
            {
                
                UpdateCurrentHealth(100);
                UpdateMaxHealth(100);
                UpdatePlayerExp(0);
                UpdatePlayerLevel(1);
            }
            if (BossController.Instance != null)
            {
                UpdateBossHealth(10000);
            }

            playerAudio.clip = gameMusic;
            playerAudio.Play();

    }
    void Update()
    {
        TimerValue();
    }
    // UPDATES SCORE BASED ON ENEMY CONTROLLER DEATH
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    // UPDATES PLAYER CURRENT HEALTH BASED ON ENEMY DAMAGE
    public void UpdateCurrentHealth(int currentHP)
    {
        healthText.text = "Health: " + currentHP;
    }
    // UPDATE BOSS CURRENT HEALTH
    public void UpdateBossHealth(float bossCurrentHp)
    {
        bossHPText.text = "BOSS: " + bossCurrentHp;
    }
    // UPDATES PLAYER MAX HEALTH BASED ON PLAYER LEVEL
    public void UpdateMaxHealth(int healthToAdd)
    {
        maxhealthText.text = "/ " + healthToAdd;
    }
    // UPDATES PLAYER EXP BASED ON ENEMY CONTROLLER DEATH EXP
    public void UpdatePlayerExp(float expToAdd)
    {
        playerexpText.text = "Exp: " + expToAdd.ToString("F2");
    }
    // UPDATES PLAYER LEVEL BASED ON EXP
    public void UpdatePlayerLevel(int levelToAdd)
    { 
        playerlevelText.text = "Level: " + levelToAdd;
    }
    // SHOWS THE GAME IF PLAYER LEVELS UP
    public void ShowLevelUp()
    {
        levelUp.gameObject.SetActive(true);
        StartCoroutine(HideLevelUp());
    }
    // SHOWS TIMER VALUE
    public void TimerValue()
    {
        timerValue += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timerValue / 60);
        int seconds = Mathf.FloorToInt(timerValue % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    /*public void JSkillTimerValue()
    {
        if(PlayerController.Instance != null)
        {
            jSkillText.text = "CD " + Mathf.Ceil(PlayerController.Instance.torrentCD);
        }
    }
    */
    // SHOWS LEVEL UP PANEL WHEN PLAYER LEVELS UP
    IEnumerator HideLevelUp()
    {
        yield return new WaitForSeconds(3f);
        levelUp.gameObject.SetActive(false);
    }
}

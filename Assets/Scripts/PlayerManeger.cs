using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int currentHealth;

    [Header("Player")]
    public Animator anim;
    public float Score = 0;
    public Text ScoreText;
    public int maxHealth = 300;
    public Vector2 checkpointPosition;
    public GameObject AttackDownEff;
    public GameObject AttackDown;
    public GameObject GroundCheck;




    [Header("BarManager")]
    //public HealthBar healthBar;
    //public manaBar ManaBar;
    public GameObject _GameOver;
    public bool IsPaused;
    public HealthBar healthBar;
    private float safeTime;
    public float safeTimeDuration = 0f;

    //[Header("Audio")]
    //public AudioSource src;
    //public AudioClip  skill;

    [Header("Data")]
    public PlayerData Data;



    private void Start()
    {
        LoadPlayerData();

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);

        // Tải dữ liệu checkpoint nếu có
        checkpointPosition = transform.position;
    }

    public void SaveCheckpoint(Vector2 position)
    {
        checkpointPosition = position;
        PlayerPrefs.SetFloat("CheckpointX", position.x);
        PlayerPrefs.SetFloat("CheckpointY", position.y);
        PlayerPrefs.Save();
    }

    public void LoadCheckpoint()
    {
        transform.position = checkpointPosition;
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(GameOverAfterDelay(0f));
            }

            //If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }

    
    private void Update()
    {
        
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
      
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Damaged"))
        {
            anim.SetTrigger("Hurt");
            TakeDam(1);
            if (currentHealth >= 1)
            {
                LoadCheckpoint();
            }
        }
        if (collision.gameObject.tag.Equals("NextLevel"))
        {
            LoadNextScene();
        }

        if (collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
            Data.scoreValue++;
            ScoreText.text = " " + Data.scoreValue;
        }
        if (collision.gameObject.tag.Equals("HP"))
        {
            Destroy(collision.gameObject);
            if (currentHealth<maxHealth)
            {
                TakeDam(-1);
            }
            
        }
        if (collision.gameObject.tag.Equals("Lava"))
        {
            currentHealth = 0;
            Destroy(gameObject);
            StartCoroutine(GameOverAfterDelay(2f));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
            Data.scoreValue++;
            ScoreText.text = " " + Data.scoreValue;
        }
        if (collision.gameObject.tag.Equals("JumpPad"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 30f);
        }
        if (collision.gameObject.tag.Equals("Damaged"))
        {
            anim.SetTrigger("Hurt");
            TakeDam(1);
            if (currentHealth>=1)
            {
                LoadCheckpoint();
            }
            
        }
        else if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            SaveCheckpoint(collision.transform.position);
        }
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform; // Gán đối tượng cha
        }
        if (collision.gameObject.tag.Equals("PointJumpAttackEnemies"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 15f);
            GameObject eff = Instantiate(AttackDownEff, GroundCheck.transform.position, Quaternion.identity);
            GameObject attackdown = Instantiate(AttackDown, gameObject.transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform; // Đảm bảo gán đối tượng cha
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null; // Bỏ gán đối tượng cha
        }
    }
    public void LoadNextScene()
    {
        Data.levelValue++;
        Data.finalTime = Data.currentTime + Data.finalTime;
        // lưu thông tin playerLevel vào PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore",Data.scoreValue);
        PlayerPrefs.SetInt("PlayerLevel", Data.levelValue); 
        PlayerPrefs.SetFloat("Playertime", Data.finalTime); 
        
        //Debug.Log("Score game" + playerData.playerScore.ToString());
        //Debug.Log("playerData.playerScore" + playerData.playerScore.ToString());

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadThisScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
   

    public void gameOver()
    {
        StartCoroutine(GameOverAfterDelay(1f));
        _GameOver.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    IEnumerator GameOverAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameOver();
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }
    void LoadPlayerData()
    {
        // Đọc dữ liệu người chơi từ file lưu trữ
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            Data.levelValue = PlayerPrefs.GetInt("PlayerLevel");
            Data.scoreValue = PlayerPrefs.GetInt("PlayerScore");
            ScoreText.text = " " + Data.scoreValue;

        }
        else
        {
            Data.levelValue = 0;
            Data.scoreValue = 0;
        }

    }
}


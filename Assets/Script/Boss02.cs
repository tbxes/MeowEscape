using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Boss02 : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 10;
    private int currentHealth;
    public Slider bossHealthSlider;
    public TMP_Text healthText;
    public float healthBarSmoothSpeed = 5f;

    private float targetSliderValue = 1f;

    [Header("Boss Attack Settings")]
    public GameObject bossBulletPrefab;
    public Transform bossMuzzle;
    public Transform player;

    public float minInterval = 1.5f;
    public float maxInterval = 3.0f;
    private float attackTimer;

    void Start()
    {
        currentHealth = maxHealth;
        targetSliderValue = 1f;
        if (bossHealthSlider != null) bossHealthSlider.value = 1f;

        UpdateHealthUI();
        SetRandomInterval();
    }

    void Update()
    {
        if (bossHealthSlider != null)
        {
            if (targetSliderValue <= 0f)
            {
                bossHealthSlider.value = 0f;
            }
            else
            {
                bossHealthSlider.value = Mathf.Lerp(bossHealthSlider.value, targetSliderValue, Time.deltaTime * healthBarSmoothSpeed);
            }
        }

        if (Time.timeScale > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                SetRandomInterval();
            }
        }
    }

    void AttackPlayer()
    {
        if (bossBulletPrefab && bossMuzzle && player)
        {
            Vector3 targetDir = player.position - bossMuzzle.position;
            bossMuzzle.rotation = Quaternion.LookRotation(targetDir);

            Instantiate(bossBulletPrefab, bossMuzzle.position, bossMuzzle.rotation);
        }
    }

    void SetRandomInterval()
    {
        attackTimer = Random.Range(minInterval, maxInterval);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        targetSliderValue = (float)currentHealth / maxHealth;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            targetSliderValue = 0f;
            if (bossHealthSlider != null) bossHealthSlider.value = 0f;

            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth + " / " + maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("💀 บอสพ่ายแพ้แล้ว!");

        // 🔊 หยุดเพลง BGM และเล่นเสียง Victory ก่อนเปลี่ยนเข้าฉาก Ending
        if (Scene02Audio.Instance != null)
        {
            Scene02Audio.Instance.StopBGM();
            Scene02Audio.Instance.PlayVictory();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Ending");
    }
}
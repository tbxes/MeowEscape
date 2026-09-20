using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // นำเข้า SceneManager สำหรับเปลี่ยน Scene
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
        // อนิเมชันหลอดเลือดสไลด์ลดลง
        if (bossHealthSlider != null)
        {
            // ถ้าเป้าหมายคือ 0 ให้เซ็ตเป็น 0 ทันที ไม่ค้างติ่ง Lerp
            if (targetSliderValue <= 0f)
            {
                bossHealthSlider.value = 0f;
            }
            else
            {
                bossHealthSlider.value = Mathf.Lerp(bossHealthSlider.value, targetSliderValue, Time.deltaTime * healthBarSmoothSpeed);
            }
        }

        // นับเวลาสุ่มยิงกระสุน (ทำงานเฉพาะตอนเกมยังไม่หยุด)
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

        // อัปเดตข้อความตัวเลขเมื่อโดนยิง
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            targetSliderValue = 0f;
            if (bossHealthSlider != null) bossHealthSlider.value = 0f; // เซ็ตหลอดเลือดเป็น 0 ทันที

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

        // คืนค่าเวลาในเกมเป็นปกติก่อนเปลี่ยนฉาก
        Time.timeScale = 1f;

        // โหลดเข้า Scene ฉากจบที่ชื่อ Ending ทันที
        SceneManager.LoadScene("Ending");
    }
}
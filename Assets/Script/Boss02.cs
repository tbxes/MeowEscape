using UnityEngine;
using UnityEngine.UI;

public class Boss02 : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 10;
    private int currentHealth;
    public Slider bossHealthSlider;
    public float healthBarSmoothSpeed = 5f; // ความเร็วในการสไลด์ลดของหลอดเลือด

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

        SetRandomInterval();
    }

    void Update()
    {
        // 1. ระบบอนิเมชันหลอดเลือดค่อยๆ สไลด์ลดลงอย่างนุ่มนวล
        if (bossHealthSlider != null)
        {
            bossHealthSlider.value = Mathf.Lerp(bossHealthSlider.value, targetSliderValue, Time.deltaTime * healthBarSmoothSpeed);
        }

        // 2. นับเวลาสุ่มยิงกระสุน
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            AttackPlayer();
            SetRandomInterval();
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

        // คำนวณเป้าหมายหลอดเลือดใหม่ เพื่อให้ Update ค่อยๆ เลื่อนหลอดเลือดไปหาจุดนี้
        targetSliderValue = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 บอสพ่ายแพ้แล้ว!");
        gameObject.SetActive(false);
    }
}
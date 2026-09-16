using UnityEngine;

public class BossController02 : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Boss Attack Settings")]
    public GameObject bossBulletPrefab; // ลาก Prefab กระสุนบอสมาใส่
    public Transform bossMuzzle;        // จุดยิงของบอส
    public Transform player;            // ลาก Player02 มาใส่

    public float minInterval = 1.5f;    // สุ่มเว้นระยะเร็วสุด
    public float maxInterval = 3.0f;    // สุ่มเว้นระยะช้าสุด
    private float attackTimer;

    void Start()
    {
        currentHealth = maxHealth;
        SetRandomInterval();
        Debug.Log("Boss HP: " + currentHealth);
    }

    void Update()
    {
        // นับเวลาสุ่มยิง
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
            // หันหน้าเล็งไปที่ผู้เล่น
            Vector3 targetDir = player.position - bossMuzzle.position;
            bossMuzzle.rotation = Quaternion.LookRotation(targetDir);

            // สร้างกระสุนบอส
            Instantiate(bossBulletPrefab, bossMuzzle.position, bossMuzzle.rotation);
        }
    }

    void SetRandomInterval()
    {
        attackTimer = Random.Range(minInterval, maxInterval);
    }

    // ฟังก์ชันรับดาเมจจากผู้เล่น
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("🔥 Boss02 โดนยิง! เลือดปัจจุบันเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 บอสพ่ายแพ้แล้ว!");
        gameObject.SetActive(false); // ซ่อนบอส
    }
}
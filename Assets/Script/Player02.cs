using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // นำเข้าไลบรารีสำหรับควบคุม UI

public class PlayerMovement02 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform gunMuzzle;
    public float fireCooldown = 0.4f;
    private float nextFireTime = 0f;

    [Header("Player Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;
    public Slider playerHealthSlider; // <--- ลาก Slider หลอดเลือดผู้เล่นมาใส่ที่นี่

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        // 1. เดินซ้าย-ขวา
        float moveInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveInput = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveInput = 1f;
        }
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);

        // 2. ยิงปืน
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab && gunMuzzle)
        {
            Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);
        }
    }

    // 3. รับดาเมจเมื่อโดนกระสุนบอส
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossBullet"))
        {
            currentHealth -= 1;
            UpdateHealthUI();

            Destroy(other.gameObject);

            if (currentHealth <= 0)
            {
                Debug.Log("💀 Game Over!");
                gameObject.SetActive(false);
            }
        }
    }

    // ฟังก์ชันอัปเดตหลอดเลือด UI
    void UpdateHealthUI()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = (float)currentHealth / maxHealth;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // นำเข้า SceneManager สำหรับ Restart ฉาก
using TMPro;

public class PlayerMovement02 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform gunMuzzle;
    public float fireCooldown = 0.6f;
    private float nextFireTime = 0f;

    [Header("Player Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;
    public Slider playerHealthSlider;
    public TMP_Text playerHealthText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel; // ลาก GameOverPanel มาใส่ตรงนี้
    public Button restartButton;     // ลาก RestartButton มาใส่ตรงนี้

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // ซ่อนหน้า GameOverPanel ไว้ตอนเริ่มเกม
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // ผูกคำสั่งปุ่ม Restart
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossBullet"))
        {
            currentHealth -= 1;
            if (currentHealth < 0) currentHealth = 0;

            UpdateHealthUI();
            Destroy(other.gameObject);

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateHealthUI()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = (float)currentHealth / maxHealth;
        }

        if (playerHealthText != null)
        {
            playerHealthText.text = currentHealth + " / " + maxHealth;
        }
    }

    void GameOver()
    {
        Debug.Log("💀 Game Over!");

        // ซ่อนโมเดล Player
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        if (GetComponent<Collider>()) GetComponent<Collider>().enabled = false;

        // แสดงหน้า GameOverPanel และหยุดเวลาในเกม
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        this.enabled = false;
    }

    // ฟังก์ชันสำหรับเริ่มเกมใหม่
    public void RestartGame()
    {
        Time.timeScale = 1f; // คืนค่าเวลาในเกมเป็นปกติก่อนโหลดฉากใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดฉากปัจจุบันใหม่
    }
}
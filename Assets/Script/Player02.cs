using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // นำเข้าสำหรับเช็คการกด UI
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
    public GameObject gameOverPanel;
    public Button restartButton;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);

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

        // 2. ยิงปืน (เช็คว่าไม่ได้กำลังคลิกปุ่ม UI อยู่)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            // ตรวจสอบว่าเมาส์ไม่ได้คลิกอยู่บน UI (เช่น ปุ่ม Next หน้า Intro)
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab && gunMuzzle)
        {
            Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);

            // 🔊 เล่นเสียงยิงปืน
            if (Scene02Audio.Instance != null)
            {
                Scene02Audio.Instance.PlayShooting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossBullet"))
        {
            currentHealth -= 1;
            if (currentHealth < 0) currentHealth = 0;

            // 🔊 เล่นเสียงโดนกระสุนมะเขือเทศ
            if (Scene02Audio.Instance != null)
            {
                Scene02Audio.Instance.PlayTomatoHit();
            }

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

        // 🔊 หยุดเพลง BGM และเล่นเสียงตาย
        if (Scene02Audio.Instance != null)
        {
            Scene02Audio.Instance.StopBGM();
            Scene02Audio.Instance.PlayDead();
        }

        // ซ่อนโมเดล Player
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        if (GetComponent<Collider>()) GetComponent<Collider>().enabled = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        this.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        // 🎵 กำหนดเป็น true ค้างไว้ เพื่อให้เมื่อ Restart แล้วจะข้ามหน้า Intro ทันที แต่เพลง BGM ยังเริ่มใหม่ได้
        IntroManager02.hasPlayedIntro = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
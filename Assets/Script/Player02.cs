using UnityEngine;
using UnityEngine.InputSystem;

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

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player HP: " + currentHealth);
    }

    void Update()
    {
        // 1. ระบบเดินซ้าย-ขวา
        float moveInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveInput = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveInput = 1f;
        }
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);

        // 2. ระบบยิงปืน
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

    // 3. ระบบรับดาเมจเมื่อโดนกระสุนบอส
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossBullet"))
        {
            currentHealth -= 1;
            Debug.Log("💥 ผู้เล่นโดนยิง! เลือดเหลือ: " + currentHealth);

            Destroy(other.gameObject); // ทำลายกระสุนบอสทิ้ง

            if (currentHealth <= 0)
            {
                Debug.Log("💀 ผู้เล่นพ่ายแพ้แล้ว (Game Over)!");
                gameObject.SetActive(false); // ซ่อนผู้เล่น
            }
        }
    }
}
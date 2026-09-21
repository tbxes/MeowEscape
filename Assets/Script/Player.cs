using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 8f;
    public float laneSpeed = 8f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float groundY = 1.5f;

    private Rigidbody rb;
    private float currentSpeed;

    private bool isGameOver = false;
    private bool wasTimePaused = true; // ตัวแปรเช็คสถานะการหยุดเวลา

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // ล็อกไม่ให้ตัวละครหมุนล้ม
        }
        currentSpeed = forwardSpeed;
    }

    void Update()
    {
        if (isGameOver) return;

        if (Time.timeScale == 0f)
        {
            wasTimePaused = true;
            return;
        }

        if (wasTimePaused)
        {
            wasTimePaused = false;
            return;
        }

        bool isGrounded = transform.position.y <= (groundY + 0.1f);

        // ระบบกระโดด
        if (isGrounded && Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

                // 🔊 เล่นเสียงกระโดด
                if (Scene01Audio.Instance != null)
                {
                    Scene01Audio.Instance.PlayJump();
                }
            }
        }

        float newZ = transform.position.z + (currentSpeed * Time.deltaTime);

        float horizontalInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalInput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                horizontalInput = 1f;
            }
        }

        float newX = transform.position.x + (horizontalInput * laneSpeed * Time.deltaTime);

        transform.position = new Vector3(newX, transform.position.y, newZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;

        // 🔊 หยุดเพลง BGM และเล่นเสียงโดนจับ/เกมโอเวอร์
        if (Scene01Audio.Instance != null)
        {
            Scene01Audio.Instance.StopBGM();
            Scene01Audio.Instance.PlayCaught();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.TriggerGameOver();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
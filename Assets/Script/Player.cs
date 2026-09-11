using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 8f;
    public float laneSpeed = 8f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float groundY = 1.5f;

    private Rigidbody rb;
    private float currentSpeed;
    private bool isSlowed = false;
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

        // 1. ถ้าเกมยังหยุดเวลาอยู่ (เช่น กำลังขึ้นหน้า Intro) ไม่ต้องประมวลผลใดๆ
        if (Time.timeScale == 0f)
        {
            wasTimePaused = true;
            return;
        }

        // 2. ป้องกันการกระโดดทันที: ข้าม 1 เฟรมแรกหลังจากเพิ่งเริ่มเกม เพื่อไม่ให้ปุ่ม Spacebar จากหน้า Intro หลุดมา
        if (wasTimePaused)
        {
            wasTimePaused = false;
            return;
        }

        // 3. เช็คว่ายืนอยู่บนพื้นหรือไม่
        bool isGrounded = transform.position.y <= (groundY + 0.1f);

        // 4. กด Spacebar กระโดด (ทำงานเฉพาะตอนเกมเดินแล้วเท่านั้น)
        if (isGrounded && Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }

        // 5. คำนวณการวิ่งไปข้างหน้า (แกน Z)
        float newZ = transform.position.z + (currentSpeed * Time.deltaTime);

        // 6. รับค่าการกดปุ่ม ซ้าย / ขวา (A / D)
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

        // 7. คำนวณแกน X ขยับซ้าย-ขวา
        float newX = transform.position.x + (horizontalInput * laneSpeed * Time.deltaTime);

        // 8. อัปเดตตำแหน่งจริง
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }

    // ตรวจจับการชน
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isSlowed)
        {
            StartCoroutine(SlowDownRoutine());
        }
        else if (other.CompareTag("Boss"))
        {
            GameOver();
        }
    }

    private IEnumerator SlowDownRoutine()
    {
        isSlowed = true;
        currentSpeed = forwardSpeed * 0.3f; // สโลว์ความเร็วเมื่อชน

        yield return new WaitForSeconds(1.5f);

        currentSpeed = forwardSpeed;
        isSlowed = false;
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // หยุดเวลาในเกมทันที
        Debug.Log("GAME OVER! โดนบอสจับได้แล้ว!");
    }
}
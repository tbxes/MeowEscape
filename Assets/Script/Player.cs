using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 8f;
    public float laneSpeed = 8f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;   // แรงกระโดด (ปรับเพิ่มได้ตามใจชอบ)
    public float groundY = 1.5f;   // ระดับความสูง Y ตอนยืนบนพื้นพอดี

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. เช็คง่ายๆ ว่ายืนอยู่บนพื้นหรือไม่ (ถ้า Y อยู่ใกล้เคียง 1.5 แปลว่ายืนบนพื้น)
        bool isGrounded = transform.position.y <= (groundY + 0.1f);

        // 2. กด Spacebar เพื่อกระโดด (ทำงานเฉพาะตอนยืนบนพื้น)
        if (isGrounded && Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                // ส่งแรงกระโดดขึ้นฟ้าทันที
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }

        // 3. คำนวณการวิ่งไปข้างหน้า (แกน Z)
        float newZ = transform.position.z + (forwardSpeed * Time.deltaTime);

        // 4. รับค่าการกดปุ่ม ซ้าย / ขวา (A / D)
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

        // 5. คำนวณแกน X ขยับซ้าย-ขวา
        float newX = transform.position.x + (horizontalInput * laneSpeed * Time.deltaTime);

        // 6. อัปเดตตำแหน่งจริง (ปล่อยให้แรงโน้มถ่วงจัดการแกน Y ตอนลอยตัว)
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }
}
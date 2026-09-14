using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement02 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;

    void Update()
    {
        float moveInput = 0f;

        if (Keyboard.current != null)
        {
            // กด A หรือ ลูกศรซ้าย
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f;
            }
            // กด D หรือ ลูกศรขวา
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f;
            }
        }

        // เคลื่อนที่ไปทางซ้าย-ขวาโดยไม่ล็อกตำแหน่งขอบเขต
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
    }
}
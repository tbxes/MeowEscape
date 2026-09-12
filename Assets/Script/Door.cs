using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ตรวจจับเมื่อ Player วิ่งมาสัมผัสประตู
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f; // หยุดเวลาในเกมทันทีเมื่อเข้าประตู
            Debug.Log("เคลียร์ Scene 1 เรียบร้อย!");
        }
    }
}
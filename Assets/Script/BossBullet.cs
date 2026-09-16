using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 15f;           // ความเร็วในการพุ่ง
    public float rotateSpeed = 200f;    // ความเร็วในการเลี้ยวตามผู้เล่น (ยิ่งเยอะยิ่งเลี้ยวตามเร็ว)
    public float lifetime = 6f;         // อายุของกระสุนก่อนหายไป

    private Transform playerTransform;

    void Start()
    {
        Destroy(gameObject, lifetime);

        // ค้นหาตำแหน่งผู้เล่นจาก Tag อัตโนมัติ (หรือจะส่งค่ามาก็ได้)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player02");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // 1. คำนวณทิศทางจากกระสุนไปยังผู้เล่น
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            // 2. หมุนหัวกระสุนหันไปทางผู้เล่นทีละนิดอย่างนุ่มนวล
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        // 3. พุ่งไปข้างหน้าตามทิศทางที่หันไปอยู่ตลอดเวลา
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
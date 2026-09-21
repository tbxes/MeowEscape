using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        // ทำลายกระสุนอัตโนมัติถ้ายิงไม่โดนอะไรเลยภายในเวลาที่กำหนด
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // ให้กระสุนพุ่งไปข้างหน้า
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // เช็คว่าชนบอสหรือไม่
        Boss02 boss = collision.gameObject.GetComponent<Boss02>();

        if (boss != null)
        {
            // เรียกฟังก์ชันลดเลือดและทำเอฟเฟกต์ตัวแดงของบอส
            boss.TakeDamage(damage);

            // ทำลายกระสุนทิ้งเมื่อยิงโดนบอส
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // กรณีที่ Collider ของกระสุนถูกตั้งค่าเป็น Is Trigger
        Boss02 boss = other.GetComponent<Boss02>();

        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
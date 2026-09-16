using UnityEngine;

public class Bullet02 : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("กระสุนชนวัตถุชื่อ: " + other.gameObject.name + " | Tag คือ: " + other.tag);

        if (other.CompareTag("Boss02"))
        {
            Debug.Log(">>> ชนเข้ากับ Boss02 สำเร็จแล้วจ้า! <<<");

            // สั่งเรียกใช้ฟังก์ชัน TakeDamage ในทุกสคริปต์ที่อยู่บนตัวบอสทันที
            other.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);

            Destroy(gameObject); // ทำลายกระสุน
        }
    }
}
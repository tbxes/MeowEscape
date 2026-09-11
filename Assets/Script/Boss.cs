using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Target & Distance Settings")]
    public Transform player;             // ลาก Player มาใส่ใน Inspector
    public float bossSpeed = 8f;         // ความเร็วบอส
    public float maxDistanceBehind = 10f; // ระยะห่างจาก Player
    public float recoverSpeed = 1.5f;

    private bool isGameOver = false;

    void Update()
    {
        if (player == null || isGameOver) return;

        // คำนวณระยะห่าง
        float currentDistanceZ = player.position.z - transform.position.z;
        float targetX = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * 5f);

        float newZ = transform.position.z + (bossSpeed * Time.deltaTime);

        // บอสชะลอถอยกลับระยะเดิมหาก Player วิ่งพ้น
        if (currentDistanceZ < maxDistanceBehind)
        {
            newZ = transform.position.z + ((bossSpeed - recoverSpeed) * Time.deltaTime);
        }

        transform.position = new Vector3(targetX, transform.position.y, newZ);
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text notiText;
    [SerializeField] private GameObject restartButton;

    [Header("Player Reference")]
    [SerializeField] private Player player;

    private void Awake()
    {
        // กำหนด Singleton Instance
        Instance = this;
    }

    private void Start()
    {
        // ปิดปุ่ม Restart และล้างข้อความแจ้งเตือนตอนเริ่มเกม
        ShowHideRestartButton(false);
        ShowNotiText("");
    }

    // ฟังก์ชันแสดงข้อความ UI
    public void ShowNotiText(string s)
    {
        if (notiText != null)
        {
            notiText.text = s;
        }
    }

    // ฟังก์ชันเปิด/ปิด ปุ่ม Restart
    public void ShowHideRestartButton(bool flag)
    {
        if (restartButton != null)
        {
            restartButton.SetActive(flag);
        }
    }

    // เรียกตอนโดน Boss จับได้ (Game Over)
    public void TriggerGameOver()
    {
        Time.timeScale = 0f; // หยุดเวลาในเกม
        ShowNotiText("GAME OVER!");
        ShowHideRestartButton(true);
    }

    // เรียกตอนวิ่งเข้าประตูชัย (Stage Clear)
    public void TriggerStageClear()
    {
        Time.timeScale = 0f; // หยุดเวลาในเกม
        ShowNotiText("STAGE CLEAR!");
        ShowHideRestartButton(true);
    }

    // ฟังก์ชัน Restart เกม
    public void RestartGame()
    {
        Time.timeScale = 1f; 

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
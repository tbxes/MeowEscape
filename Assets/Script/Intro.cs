using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem; // สำหรับ New Input System

public class IntroDialogue : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject introPanel;
    public TMP_Text dialogueText;      // ลากข้อความ TMP ใน IntroPanel มาใส่ช่องนี้
    public Button startButton;

    [Header("Dialogue Settings")]
    [TextArea(3, 5)]
    public string fullText = "Cats are taking over the world! \r\nHelp me escape! quick!";
    public float typingSpeed = 0.05f;  // ความเร็วในการพิมพ์ (วินาทีต่อตัวอักษร)

    private bool isTyping = false;
    private bool isGameStarted = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        // 1. หยุดเวลาในเกมชั่วคราว
        Time.timeScale = 0f;

        if (introPanel != null) introPanel.SetActive(true);

        // 2. ซ่อนปุ่มกดเริ่มต้นไว้ก่อน
        if (startButton != null)
        {
            startButton.gameObject.SetActive(false);
            startButton.onClick.AddListener(StartGame);
        }

        // 3. เริ่มแสดงข้อความพิมพ์ทีละตัว
        if (dialogueText != null)
        {
            typingCoroutine = StartCoroutine(TypeText());
        }
    }

    void Update()
    {
        // ตรวจสอบการกด Spacebar
        bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

        if (spacePressed && !isGameStarted)
        {
            if (isTyping)
            {
                // ถ้ายังพิมพ์ไม่เสร็จ แล้วผู้เล่นกด Spacebar ให้เร่งแสดงข้อความเต็มทันที (Skip)
                CompleteTyping();
            }
            else
            {
                // ถ้าพิมพ์เสร็จแล้ว กด Spacebar อีกครั้งเพื่อเริ่มเกม
                StartGame();
            }
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            dialogueText.text += letter;
            // ต้องใช้ WaitForSecondsRealtime เพราะ Time.timeScale ถูกตั้งเป็น 0
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        FinishTyping();
    }

    void CompleteTyping()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueText.text = fullText;
        FinishTyping();
    }

    void FinishTyping()
    {
        isTyping = false;
        // แสดงปุ่มกดเมื่อพิมพ์เสร็จเรียบร้อย
        if (startButton != null)
        {
            startButton.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (isGameStarted) return;

        isGameStarted = true;

        if (introPanel != null)
        {
            introPanel.SetActive(false);
        }

        // คืนค่าเวลาให้เกมวิ่งตามปกติ
        Time.timeScale = 1f;
    }
}
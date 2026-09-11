using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class IntroDialogue : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject introPanel;
    public TMP_Text dialogueText;
    public Button startButton;

    [Header("Dialogue Settings")]
    [TextArea(3, 5)]
    public string fullText = "Cats are taking over the world! \r\nHelp me escape! quick!";
    public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool isGameStarted = false;
    private Coroutine typingCoroutine;

    void Awake()
    {
        // 1. บังคับเปิด Canvas หลักของวัตถุนี้ (กรณีปิด Canvas ไว้ตอนแต่งฉาก)
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(true);
            parentCanvas.enabled = true;
        }

        // 2. บังคับเปิด IntroPanel ทันทีตั้งแต่เฟรมแรกสุดก่อนเริ่มเรนเดอร์
        if (introPanel != null)
        {
            introPanel.SetActive(true);
        }
    }

    void Start()
    {
        // หยุดเวลาในเกมชั่วคราว
        Time.timeScale = 0f;

        // ซ่อนปุ่มกดเริ่มต้นไว้ก่อน
        if (startButton != null)
        {
            startButton.gameObject.SetActive(false);
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame);
        }

        // เริ่มแสดงข้อความพิมพ์ทีละตัว
        if (dialogueText != null)
        {
            typingCoroutine = StartCoroutine(TypeText());
        }
    }

    void Update()
    {
        bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

        if (spacePressed && !isGameStarted)
        {
            if (isTyping)
            {
                CompleteTyping();
            }
            else
            {
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

        Time.timeScale = 1f;
    }
}
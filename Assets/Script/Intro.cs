using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    [Header("UI & Settings")]
    public GameObject introPanel;
    public TMP_Text dialogueText;
    public GameObject startPromptPanel; // เปลี่ยนจาก Button เป็น GameObject (Panel + TMP)
    [TextArea] public string fullText = "Cats are taking over the world! \r\nHelp me escape! quick!";
    public float typingSpeed = 0.05f;

    public static bool hasSeenIntro = false; // จำค่าว่าเคยดู Intro หรือยัง
    private bool isTyping, isGameStarted;

    void Start()
    {
        // ถ้าเคยดูแล้ว (ตอนกด Restart) ให้ข้ามเริ่มเกมทันที
        if (hasSeenIntro) { StartGame(); return; }

        Time.timeScale = 0f;
        if (introPanel) introPanel.SetActive(true);
        if (startPromptPanel) startPromptPanel.SetActive(false); // ซ่อน Panel บอกปุ่มตอนเริ่ม

        StartCoroutine(TypeText());
    }

    void Update()
    {
        if (isGameStarted) return;

        // กด Spacebar เพื่อข้ามข้อความ หรือเริ่มเกม
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isTyping) CompleteTyping();
            else StartGame();
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in fullText)
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        FinishTyping();
    }

    void CompleteTyping()
    {
        StopAllCoroutines();
        dialogueText.text = fullText;
        FinishTyping();
    }

    void FinishTyping()
    {
        isTyping = false;
        if (startPromptPanel) startPromptPanel.SetActive(true); // แสดง Panel เมื่อพิมพ์จบประโยค
    }

    void StartGame()
    {
        hasSeenIntro = true; // แก้ไขจาก hasPlayedIntro ให้ตรงกับตัวแปรที่ประกาศไว้ข้างบน
        isGameStarted = true; // บันทึกสถานะว่าเริ่มเล่นเกมแล้ว

        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f;

        // 🎵 สั่งเริ่มเล่นเพลง BGM ใน Scene01 ทันทีที่กดผ่าน Intro!
        if (Scene01Audio.Instance != null)
        {
            Scene01Audio.Instance.StartBGM();
        }

        this.enabled = false;
    }
}
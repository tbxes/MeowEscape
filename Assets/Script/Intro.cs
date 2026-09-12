using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    [Header("UI & Settings")]
    public GameObject introPanel;
    public TMP_Text dialogueText;
    public Button startButton;
    [TextArea] public string fullText = "Cats are taking over the world! \r\nHelp me escape! quick!";
    public float typingSpeed = 0.05f;

    public static bool hasSeenIntro = false; 
    private bool isTyping, isGameStarted;

    void Start()
    {
       
        if (hasSeenIntro) { StartGame(); return; }

        Time.timeScale = 0f;
        if (introPanel) introPanel.SetActive(true);
        if (startButton)
        {
            startButton.gameObject.SetActive(false);
            startButton.onClick.AddListener(StartGame);
        }
        StartCoroutine(TypeText());
    }

    void Update()
    {
        if (isGameStarted) return;

        
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
        if (startButton) startButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        isGameStarted = hasSeenIntro = true;
        StopAllCoroutines();
        if (introPanel) introPanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}
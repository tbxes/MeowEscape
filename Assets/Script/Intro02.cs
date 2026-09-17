using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroManager02 : MonoBehaviour
{
    public static bool hasPlayedIntro = false;

    [Header("UI References")]
    public GameObject introPanel;
    public TMP_Text introText;
    public Image gunImage;
    public Button nextButton;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.04f; // ความเร็วในการพิมพ์ (ยิ่งน้อยยิ่งไว)

    private int step = 0;
    private Coroutine typingCoroutine;
    private string currentTargetText = "";
    private bool isTyping = false;

    void Start()
    {
        if (hasPlayedIntro)
        {
            SkipIntro();
            return;
        }

        Time.timeScale = 0f;

        if (introPanel != null) introPanel.SetActive(true);

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextStep);
        }

        ShowCurrentStep();
    }

    void NextStep()
    {
        // ถ้าข้อความกำลังพิมพ์อยู่ แล้วผู้เล่นกด Next ➡️ ให้เร่งพิมพ์ให้เสร็จทันที
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            introText.text = currentTargetText;
            isTyping = false;
            return;
        }

        step++;
        ShowCurrentStep();
    }

    void ShowCurrentStep()
    {
        if (step == 0)
        {
            string message = "An angry Giant Cat Boss is attacking the city!\nUse your gun to defeat the angry cat!";
            StartTyping(message);
            if (gunImage != null) gunImage.gameObject.SetActive(false);
        }
        else if (step == 1)
        {
            string message = "You received a special gun!\n\nUse [A] and [D] to move and dodge.\nClick Left-Mouse to shoot!";
            StartTyping(message);
            if (gunImage != null) gunImage.gameObject.SetActive(true);
        }
        else if (step == 2)
        {
            string message = "So... Let's go and fight with the boss!";
            StartTyping(message);
            if (gunImage != null) gunImage.gameObject.SetActive(false);
        }
        else if (step >= 3)
        {
            StartGame();
        }
    }

    void StartTyping(string textToType)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        currentTargetText = textToType;
        typingCoroutine = StartCoroutine(TypeText(textToType));
    }

    // Coroutine ค่อยๆ พิมพ์ตัวอักษรทีละตัว (ใช้วิธี realtime เพื่อให้พิมพ์ได้แม้จะปรับ Time.timeScale = 0)
    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        introText.text = "";

        foreach (char c in textToType.ToCharArray())
        {
            introText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void StartGame()
    {
        hasPlayedIntro = true;
        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f;
        this.enabled = false;
    }

    void SkipIntro()
    {
        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f;
        this.enabled = false;
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager02 : MonoBehaviour
{
    // static bool จะไม่ถูกรีเซ็ตค่าแม้ฉากจะโดน LoadScene ใหม่
    public static bool hasPlayedIntro = false;

    [Header("UI References")]
    public GameObject introPanel;
    public TMP_Text introText;
    public Image gunImage;
    public Button nextButton;

    private int step = 0;

    void Start()
    {
        // ถ้าเคยเล่น Intro ไปแล้ว (กด Try Again มา) ให้ข้าม Intro ทันที!
        if (hasPlayedIntro)
        {
            SkipIntro();
            return;
        }

        // --- ถ้าเป็นการเล่นครั้งแรก ให้ทำตามขั้นตอนปกติ ---
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
        step++;
        ShowCurrentStep();
    }

    void ShowCurrentStep()
    {
        if (step == 0)
        {
            if (introText != null)
            {
                introText.text = "An angry Giant Cat Boss is attacking the city!\nUse your gun to defeat the angry cat!";
            }
            if (gunImage != null) gunImage.gameObject.SetActive(false);
        }
        else if (step == 1)
        {
            if (introText != null)
            {
                introText.text = "You received a special gun!\n\nUse [A] and [D] to move and dodge.\nClick Left-Mouse to shoot!";
            }
            if (gunImage != null) gunImage.gameObject.SetActive(true);
        }
        else if (step == 2)
        {
            if (introText != null)
            {
                introText.text = "So... Let's go and fight with the boss!";
            }
            if (gunImage != null) gunImage.gameObject.SetActive(false);
        }
        else if (step >= 3)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        hasPlayedIntro = true; // บันทึกว่าเล่น Intro ผ่านไปแล้ว

        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f;
        this.enabled = false;
    }

    // ฟังก์ชันสำหรับซ่อน Intro ทันทีเมื่อกด Try Again
    void SkipIntro()
    {
        if (introPanel != null) introPanel.SetActive(false);
        Time.timeScale = 1f; // ให้เวลาเดินปกติทันที
        this.enabled = false;
    }
}
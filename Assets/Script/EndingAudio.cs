using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    [Header("───────────── Audio Settings ─────────────")]
    [SerializeField] private AudioSource sfxSource;
    public AudioClip victorySFX;

    private void Start()
    {
        // 🔊 เล่นเสียง Victory ทันทีที่เข้าฉาก Ending
        if (sfxSource != null && victorySFX != null)
        {
            sfxSource.PlayOneShot(victorySFX);
        }
        else
        {
            Debug.LogWarning("⚠️ ลืมใส่ AudioSource หรือ Victory SFX ใน Inspector!");
        }
    }
}
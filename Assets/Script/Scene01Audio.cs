using UnityEngine;

public class Scene01Audio : MonoBehaviour
{
    public static Scene01Audio Instance;

    [Header("───────────── Audio Sources ─────────────")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("───────────── BGM ─────────────")]
    public AudioClip backgroundMusic;

    [Header("───────────── SFX ─────────────")]
    public AudioClip jumpSFX;
    public AudioClip caughtSFX;
    public AudioClip victorySFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    // 🎵 เล่น BGM
    public void StartBGM()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopBGM()
    {
        if (musicSource != null) musicSource.Stop();
    }

    // 🔊 เล่น SFX
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // ฟังก์ชันลัดสำหรับเรียกใช้ SFX ที่เหลือ
    public void PlayJump() => PlaySFX(jumpSFX);
    public void PlayCaught() => PlaySFX(caughtSFX);
    public void PlayVictory() => PlaySFX(victorySFX);
}
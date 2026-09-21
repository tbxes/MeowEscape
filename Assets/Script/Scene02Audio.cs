using UnityEngine;

public class Scene02Audio : MonoBehaviour
{
    public static Scene02Audio Instance;

    [Header("───────────── Audio Sources ─────────────")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("───────────── BGM ─────────────")]
    public AudioClip backgroundMusic;

    [Header("───────────── SFX ─────────────")]
    public AudioClip shootingSFX;
    public AudioClip tomatoSFX; // เสียงโดนกระสุนมะเขือเทศของบอส
    public AudioClip deadSFX;
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
        // ถ้าเคยผ่าน Intro ไปแล้ว (เช่น กรณีโหลดฉากซ้ำ) ให้เริ่มเพลง BGM ทันที
        if (IntroManager02.hasPlayedIntro)
        {
            StartBGM();
        }
        else if (musicSource != null)
        {
            // หยุดเพลง BGM ไว้ก่อน รอเริ่มเล่นตอนผ่าน Intro02
            musicSource.Stop();
        }
    }

    // 🎵 สั่งเริ่มเล่น BGM
    public void StartBGM()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // 🎵 สั่งหยุด BGM
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

    // ฟังก์ชันลัดสั่งเล่นเสียงต่างๆ
    public void PlayShooting() => PlaySFX(shootingSFX);
    public void PlayTomatoHit() => PlaySFX(tomatoSFX);
    public void PlayDead() => PlaySFX(deadSFX);
    public void PlayVictory() => PlaySFX(victorySFX);
}
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip click;

    private void Start()
    {
        // เล่นเพลง BGM แบบวนลูปทันทีที่เข้าหน้า MainMenu
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // ฟังก์ชันสั่งเล่นเสียงคลิก (สำหรับเอาไปผูกกับปุ่ม)
    public void PlayClickSFX()
    {
        if (sfxSource != null && click != null)
        {
            sfxSource.PlayOneShot(click);
        }
    }
}
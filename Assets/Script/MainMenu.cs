using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MAinMenu : MonoBehaviour
{
    public MainMenuAudio audioManager;

    public void PlayGame()
    {
        StartCoroutine(PlayGameWithSound());
    }

    IEnumerator PlayGameWithSound()
    {
        // เล่นเสียงคลิกก่อน
        if (audioManager != null)
        {
            audioManager.PlayClickSFX();
        }

        // รอเสียงเล่นแป๊บหนึ่ง (0.15 วินาที)
        yield return new WaitForSecondsRealtime(0.15f);

        // โหลดเปลี่ยนฉาก
        SceneManager.LoadSceneAsync("Loading");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    void Start()
    {
        // คืนค่าเวลาในเกมเป็นปกติ เผื่อมีการ Pause มาจากหน้าสู้บอส
        Time.timeScale = 1f;

        // ปลดล็อกและแสดงเคอร์เซอร์เมาส์ให้กดปุ่มได้สะดวก
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ฟังก์ชันสำหรับปุ่ม Back To Main Menu
    public void GoToMainMenu()
    {
        // รีเซ็ตเพื่อให้อนาคตเล่นใหม่แล้วมี Intro ทำงานตามปกติ
        IntroManager02.hasPlayedIntro = false;

        // โหลดกลับหน้า MainMenu (เปลี่ยนชื่อให้ตรงกับ Scene หน้าแรกของคุณ)
        SceneManager.LoadScene("MainMenu");
    }

    // ฟังก์ชันสำหรับปุ่ม Exit Game
    public void ExitGame()
    {
        Debug.Log("👋 ออกจากเกมเรียบร้อย!");
        Application.Quit();

#if UNITY_EDITOR
        // หยุดเล่นเกมเมื่อทดสอบในหน้าต่าง Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
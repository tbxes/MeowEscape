using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text notiText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextSceneButton; 

    [Header("Player Reference")]
    [SerializeField] private Player player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        ShowHideRestartButton(false);
        ShowHideNextSceneButton(false);
        ShowNotiText("");
    }

   
    public void ShowNotiText(string s)
    {
        if (notiText != null)
        {
            notiText.text = s;
        }
    }

    
    public void ShowHideRestartButton(bool flag)
    {
        if (restartButton != null)
        {
            restartButton.SetActive(flag);
        }
    }

    
    public void ShowHideNextSceneButton(bool flag)
    {
        if (nextSceneButton != null)
        {
            nextSceneButton.SetActive(flag);
        }
    }

    
    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        ShowNotiText("GAME OVER!");
        ShowHideRestartButton(true);
        ShowHideNextSceneButton(false);
    }

    
    public void TriggerStageClear()
    {
        Time.timeScale = 0f;
        ShowNotiText("STAGE CLEAR!");
        ShowHideRestartButton(false); 
        ShowHideNextSceneButton(true);  
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void GoToNextScene()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
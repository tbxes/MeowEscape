using UnityEngine;
using UnityEngine.SceneManagement;
public class MAinMenu : MonoBehaviour

{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Loading");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}







using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    [Header("Loading Speed Settings")]
    public float loadingSpeed = 0.5f;

    void Start()
    {

        LoadLevel("Scene01");
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        float currentProgress = 0f;

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, loadingSpeed * Time.deltaTime);

            slider.value = currentProgress;
            progressText.text = Mathf.RoundToInt(currentProgress * 100f) + "%";


            if (currentProgress >= 0.99f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
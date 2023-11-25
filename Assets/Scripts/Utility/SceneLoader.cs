using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingPanel;

    [SerializeField]
    private Slider _loadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        _loadingPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _loadingSlider.value = 0f;
        _loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingSlider.value = progress;

            yield return null;
        }

        _loadingPanel.SetActive(false);
    }
}

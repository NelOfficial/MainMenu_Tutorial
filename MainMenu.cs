using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image _loadingProgressbar;
    [SerializeField] GameObject _loadingProgressPanel;

    private Resolution[] resolutions;

    [SerializeField] TMP_Dropdown resolutionDropdown;


    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGame_Async());

        _loadingProgressPanel.SetActive(true);
    }

    private IEnumerator PlayGame_Async()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingProgressbar.fillAmount = progress / 100f;

            yield return null;
        }
    }

    public void ChangeQualityPreset(int qualityPresetIndex)
    {
        QualitySettings.SetQualityLevel(qualityPresetIndex);
    }

    public void ChangeTextureQuality(int texturePresetIndex)
    {
        QualitySettings.masterTextureLimit = texturePresetIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

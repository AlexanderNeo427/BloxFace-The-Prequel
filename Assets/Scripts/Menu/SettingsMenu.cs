using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Slider volumeSlider;

    public bool SettingsIsActive = false;

    public Button EnableSettingsButton;
    public Button DisableSettingsButton;

    public GameObject SettingsUI;
    public GameObject PauseMenuUI;
    public GameObject PauseButtonUI;
    public GameObject InventoryUI;

    void Start()
    {
        Button btn1 = EnableSettingsButton.GetComponent<Button>();
        Button btn2 = DisableSettingsButton.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);
        btn2.onClick.AddListener(TaskOnClick);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string sOption = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(sOption);

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

    public void Update()
    {
        SetVolume(volumeSlider.value);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log(volume);
    }

    void TaskOnClick()
    {
        if (SettingsIsActive)
        {
            EnableSettings();
        }
        else
        {
            DisableSettings();
        }
    }

    public void EnableSettings()
    {
        SettingsUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        PauseButtonUI.SetActive(false);
        InventoryUI.SetActive(false);
        //Time.timeScale = 1f;
        SettingsIsActive = true;
    }

    public void DisableSettings()
    {
        SettingsUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        PauseButtonUI.SetActive(false);
        InventoryUI.SetActive(false);
        //Time.timeScale = 0f;
        SettingsIsActive = false;
    }
}

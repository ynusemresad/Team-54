using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    //Sound
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    //Gameplay
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    //Toggle
    [SerializeField] private Toggle invertYToggle = null;

    //Graphics Settings
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness= 1;


    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;



    private int qualityLevel;
    private bool isFullScreen;
    private float brightnessLevel;



    //Confirmation
    [SerializeField] private GameObject comfirmationPrompt = null;

    //Resolution Dropdowns
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    private void Start()
    {
        resolutions=Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;

            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);

    }


    //Levels to load
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(1); // Load the game scene
    }

    public void NewGameDialogNo()
    {
        SceneManager.LoadScene(0); // Load the main menu
    }

    public void ExitButton() {
    Application.Quit();
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text=volume.ToString("0.0");
    }


    public void VolumeApply() {

        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    
    }


    public void SetControllerSen(float sensivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensivity);
        ControllerSenTextValue.text = sensivity.ToString("0");

    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY",1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }



    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }


    public void SetFullScreen(bool isFull)
    {
        isFullScreen = isFull;
    }


    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }


    public void GraphicsApply() {
        PlayerPrefs.SetFloat("masterBrightness",brightnessLevel);

        PlayerPrefs.SetInt("masterQuality",qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen",isFullScreen ? 1 : 0);
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ConfirmationBox());
    }



    public void ResetButton(string MenuType)
    {

        if (MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");


            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height,Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();

        }


        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();

        }

        if (MenuType=="Gameplay")
        {
            ControllerSenTextValue.text=defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();

        }
    }

    public IEnumerator ConfirmationBox()
    {

        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }


}

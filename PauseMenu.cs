using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool pauseMenuIsActive = false;

    public GameObject pauseMenuUI;
    public GameObject helpMenuUI;
    public GameObject preferences_Container;
    public GameObject audio_Container;

    public AudioSource outroMusic;

    private bool outroMusicMustUnpause = false;

    public string mainMenuScene = "MenuSceneBACKUP0";

    [Header("Graphics Settings")]
    private int _qualityLevel;
    [SerializeField] private Button lowQuality;
    [SerializeField] private Button mediumQuality;
    [SerializeField] private Button ultraQuality;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject audioNotAppliedPopUp = null;
    float tempVolume; // stores master volume when user clicks on audio settings

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (helpMenuUI.activeSelf == false && Time.timeScale==1f)
            {
                Pause();
            }
            /*else
            {
                Resume();
            }*/
        }
  
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuIsActive = false;
        if (outroMusicMustUnpause)
        {
            outroMusic.Play();
            outroMusicMustUnpause = false;
        }
    }

    void Pause()
    {
        if (outroMusic.isPlaying)
        {
            outroMusic.Pause();
            outroMusicMustUnpause = true;
        }
        outroMusic.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuIsActive = true;

    }

    public void QuitGame()
    {
        Debug.Log("Quiting..");
        Application.Quit();
    }

    public void ToMainMenu()
    {
        Debug.Log("To main menu..");
        Time.timeScale = 1f; // unpause
        pauseMenuIsActive = false;
        pauseMenuUI.SetActive(false); // hide panel
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ChangeGraphicsQuality(TMP_Text tmpTextObject) // changes graphics quality
    {
        if (tmpTextObject.name == "TextTMP_OP1")
        {
            Debug.Log("Low");
            _qualityLevel = 0;
        }
        else if (tmpTextObject.name == "TextTMP_OP2")
        {
            Debug.Log("Medium");
            _qualityLevel = 1;
        }
        else if (tmpTextObject.name == "TextTMP_OP3")
        {
            Debug.Log("Ultra");
            _qualityLevel = 2;
        }
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
        Debug.Log("quality level: " + QualitySettings.GetQualityLevel());
    }

    public void GraphicsSelectedQuality_onClick() // selects text of running quality, runs on Graphics click
    {
        _qualityLevel = PlayerPrefs.GetInt("masterQuality");
        if (_qualityLevel == 0)
        {
            lowQuality.Select();
        }
        else if (_qualityLevel == 1)
        {
            mediumQuality.Select();
        }
        else if (_qualityLevel == 2)
        {
            ultraQuality.Select();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0"); // change textbox value
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        tempVolume = AudioListener.volume; // update tempVolume
    }

    public void ResetVolume() // resets volume to default value 1.0
    {
        SetVolume(1.0f);
        volumeSlider.value = 1.0f; // update volume slider fill UI
        ApplyVolume();
    }

    public void AudioPreferences_OnClick()
    {
        volumeSlider.value = AudioListener.volume; // update UI according to current sound volume
        volumeTextValue.text = AudioListener.volume.ToString("0.0");

        tempVolume = AudioListener.volume;
    }

    public void AudioBackButton() // Audio Settings Back Button Functionality
    {
        if (volumeSlider.value != tempVolume) // changes were made but not applied.
        {
            Debug.Log(volumeSlider.value + " != " + tempVolume);
            audioNotAppliedPopUp.SetActive(true);
        }
        else // default operation
        {
            audio_Container.SetActive(false);
            preferences_Container.SetActive(true);
        }
    }
    public void AudioBackButtonDeny()
    {
        AudioListener.volume = tempVolume; // reset audio to previous state
    }

    public void GameRestart() 
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("DemoScene");
        Cursor.lockState = CursorLockMode.Locked; // lock cursor

    }
    public void ChangeLanguageGreek()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];

    }

    public void ChangeLanguageEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }
}

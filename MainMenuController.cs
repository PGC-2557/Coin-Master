using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
public class MainMenuController : MonoBehaviour
{
    public AudioSource backgroundMusic; // stored inside an empty object in the main menu scene.

    [SerializeField] private GameObject mainMenu_Container;
    [SerializeField] private GameObject preferences_Container;
    [SerializeField] private GameObject audio_Container;

    [Header("Start/Load Levels")]
    public string _newGameLevel;
    private string levelToLoad; // saved level
    [SerializeField] private GameObject noSaveFound_PopUp;
    [SerializeField] private GameObject loading_PopUp;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject audioNotAppliedPopUp = null;
    float tempVolume; // stores master volume when user clicks on audio settings

    [Header("Graphics Settings")]
    private int _qualityLevel;
    [SerializeField] private Button lowQuality;
    [SerializeField] private Button mediumQuality;
    [SerializeField] private Button ultraQuality;

    [Header("Difficulty")]
    [SerializeField] private GameObject difficulty_PopOut;
    private string _difficulty; // stores game difficulty
    [SerializeField] private Button easyButton;
    [SerializeField] private Button moderateButton;
    [SerializeField] private Button masterButton;

    public void Start()
    {
        backgroundMusic.Play(); // start playing main menu's background music
        loading_PopUp.SetActive(false); // if pop up is active (from previously starting/opening a scene)
    }
    public void NewGame() // "Start" button click
    {
        mainMenu_Container.SetActive(false);
        difficulty_onClick(); //sets default difficulty if none is previously used, selects it's button
        difficulty_PopOut.SetActive(true); // shows difficulty list
    }

    public void LoadGame() // "Continue" button click
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else // no save file found, show pop up
        {
            //mainMenu_Container.SetActive(false);
            noSaveFound_PopUp.SetActive(true);
        }
    }

    public void QuitGame() // quit button
    {
        Debug.Log("Quiting Game..");
        Application.Quit();
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

    /*public void TempStoreCurrentAudioLevels() // On Click: Audio Preferences 
    {
        tempVolume = AudioListener.volume;
    }*/

    public void AudioPreferences_OnClick() 
    {
        volumeSlider.value = AudioListener.volume; // update UI according to current sound volume
        volumeTextValue.text = AudioListener.volume.ToString("0.0");

        tempVolume = AudioListener.volume;
    }

    public void AudioBackButton() // Audio Settings Back Button Functionality
    {
        if(volumeSlider.value != tempVolume) // changes were made but not applied.
        {
            Debug.Log(volumeSlider.value+" != "+tempVolume);
            audioNotAppliedPopUp.SetActive(true);
        } else // default operation
        {
            audio_Container.SetActive(false);
            preferences_Container.SetActive(true);
        }
    }
    public void AudioBackButtonDeny()
    {
        AudioListener.volume = tempVolume; // reset audio to previous state
    }

    public void ChangeGraphicsQuality(TMP_Text tmpTextObject)
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
        else if(_qualityLevel == 2)
        {
            ultraQuality.Select();
        }
    }
    public void setDifficulty(TMP_Text tmpTextObject)
    {
        if (tmpTextObject.name == "TextTMP_Easy")
        {
            _difficulty = "easy";
        }
        else if (tmpTextObject.name == "TextTMP_Mod")
        {
            _difficulty = "moderate";
        }
        else if (tmpTextObject.name == "TextTMP_Master")
        {
            _difficulty = "master";
        }
        Debug.Log("Selected Difficulty: " + _difficulty);
    }

    public void difficulty_onClick() // select coresponding difficulty button
    {
        _difficulty = PlayerPrefs.GetString("difficulty");
        Debug.Log("Current difficulty: " + _difficulty);
        if (_difficulty != "easy" && _difficulty != "moderate" && _difficulty != "master")
        {
            _difficulty = "moderate"; // default
            Debug.Log("Difficulty changed to: " + _difficulty);
        }

        if(_difficulty=="easy")
        {
            easyButton.Select();
        }
        else if (_difficulty=="moderate")
        {
            moderateButton.Select();
        }
        else if (_difficulty=="master")
        {
            masterButton.Select();
        }

    }

    public void ApplyDifficulty()
    {
        PlayerPrefs.SetString("difficulty", _difficulty);
        Debug.Log("[APPLIED] Difficulty: " + _difficulty);
        SceneManager.LoadScene(_newGameLevel); // start game

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape_Press : MonoBehaviour
{
    public GameObject quitToMainMenuPopUp; // reference
    public GameObject loadingPopUp;
    public AudioSource outroMusic;

    private bool outroMusicMustUnpause = false;

    public string mainMenuScene = "MenuSceneBACKUP0"; // name of main menu scene

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // if escape is pressed
        {
            if(Time.timeScale == 1f) // if game is not paused == main menu/help menu/escapePopUp/Highscores are not being shown
            {
                if (outroMusic.isPlaying)
                {
                    outroMusic.Pause();
                    outroMusicMustUnpause = true;
                }
                Time.timeScale = 0f; // stop time
                quitToMainMenuPopUp.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined; // release cursor
            }
            else if(Time.timeScale == 0f && quitToMainMenuPopUp.activeSelf) // pres ESC while pop up is being shown
            {
                closeESCPopUp();
            }
        }
    }
    public void closeESCPopUp() // button no
    {
        if (outroMusicMustUnpause)
        {
            outroMusic.Play();
            outroMusicMustUnpause = false;
        }

        Debug.Log("Escape Cancel");
        Time.timeScale = 1f; // release time
        quitToMainMenuPopUp.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
    }

    public void ToMainMenu() // button yes, user want to quit to menu
    {
        Debug.Log("To Main Menu..");
        quitToMainMenuPopUp.SetActive(false); // hide panel
        loadingPopUp.SetActive(true);
        Time.timeScale = 1f; // unpause
        SceneManager.LoadScene(mainMenuScene);
    }
}

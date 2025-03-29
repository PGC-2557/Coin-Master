using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour
{
    public GameObject pauseMenuUI; // pause menu
    public GameObject helpScreenUI; // container
    public GameObject helpScreenMainUI; // main screen
    public GameObject helpScreenControlsUI;
    public GameObject helpScreenHowToPlayUI;

    public AudioSource outroMusic;
    private bool outroMusicMustUnpause = false;

    private void Start()
    {
        helpScreenUI.SetActive(true);
        helpScreenMainUI.SetActive(false);
        helpScreenHowToPlayUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined; // release cursor

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // when F1 (Help Screen Key) is pressed
        {
            if (Time.timeScale==1f && !helpScreenUI.activeSelf) // check if pause menu or the help screen is already being shown
            {
                ShowHelpScreen(); // if not then show help screen
            }
            else if (helpScreenUI.activeSelf) // if any of the help screens are being shown
            {
                CloseHelpScreen(); // hide them
            }
        }


    }
    void ShowHelpScreen()
    {
        if (outroMusic.isPlaying)
        {
            outroMusic.Pause();
            outroMusicMustUnpause = true;
        }
        Time.timeScale = 0f; // stop time
        Cursor.lockState = CursorLockMode.Confined; // release cursor
        helpScreenUI.SetActive(true);
        helpScreenMainUI.SetActive(true);
    }

    public void CloseHelpScreen()
    {
        if (outroMusicMustUnpause)
        {
            outroMusic.Play();
            outroMusicMustUnpause = false;
        }
        Time.timeScale = 1f; // resume time
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        helpScreenUI.SetActive(false);
        helpScreenMainUI.SetActive(false);
        helpScreenControlsUI.SetActive(false);
        helpScreenHowToPlayUI.SetActive(false);

    }
}

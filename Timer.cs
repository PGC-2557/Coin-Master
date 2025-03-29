using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float easyStartTime = 900; // start time in seconds (easy difficulty)
    //private float easyStartTime = 10;
    private float moderateStartTime = 540; // start time in seconds (moderate diff)
    private float masterStartTime = 360; // start time in seconds (master diff)
    public float timeRemaining; // time remaining in seconds
    public string _difficulty; // difficulty level

    private float minutes;
    private float seconds;
    private bool stopTimer = false;

    public TextMeshProUGUI timerText; // the ui text of the timer

    public GameObject highScoreUI; // used to acces HighScore_Controller Script

    public AudioSource outroMusic;

    private bool outroMusicHasPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1f;
        _difficulty = PlayerPrefs.GetString("difficulty"); // reads the game's difficulty level
        outroMusicHasPlayed = false;
        Debug.Log("TIMER START FUNCTION RUN");
        Debug.Log("OutroMusicHasPlayed == "+outroMusicHasPlayed);
        if (_difficulty == "easy") // assigns correspinding time remaining value according to difficulty
        {
            timeRemaining = easyStartTime;
        }
        else if (_difficulty == "moderate")
        {
            timeRemaining = moderateStartTime;
        }
        else if (_difficulty == "master")
        {
            timeRemaining = masterStartTime;
        }
        //timerRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) // time runs normal
        {
            //Debug.Log("outro music is playing? == " + outroMusic.isPlaying);
            if (_difficulty == "master" && timeRemaining < 58 && !outroMusicHasPlayed)
            {
                Debug.Log("[TIMER-INFO] Outro music starts");
                outroMusicHasPlayed = true;
                outroMusic.Play();
            }

            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);

        }
        else if(!stopTimer)// time ran out
        {

            Debug.Log("Time is Up!");
            stopTimer = true; // so else if wont loop endlessly
            //timerRun = false; // stop running
            timeRemaining = 0; // default to 0
            timerText.SetText("00:00"); //bug fix
            Time.timeScale = 0f; // stop time
            highScoreUI.GetComponent<HighScore_Controller>().printScores();
        }
        
    }

    private void DisplayTime(float time) // updates the timer textbox value
    {
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimePenaltySkull() // time penalty for collecting a skull coin
    {
        timeRemaining -= 15;
    }
}

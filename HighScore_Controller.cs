using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore_Controller : MonoBehaviour
{
    public AudioSource highscoreMusic;

    // to be able to show/hide from script
    public GameObject highScores_Container;
    public GameObject highScores_Panel;
    public GameObject highScores_Canvas;

    public AudioSource outroMusic;

    private int goldCoins; // coin count
    public int scorePerGold = 500; // how much score is one gold coin worth
    private int silverCoins;
    public int scorePerSilver = 200;
    private int bronzeCoins;
    public int scorePerBronze = 50;
    public int easyMultiplier = 1; // sum score is multiplied by corresponding difficulty value
    public int moderateMultiplier = 2;
    public int masterMultiplier = 3;
    private int multiplier = 1; // in case something goes totally wrong, default 1  from here
    private string difficulty; // game difficulty
    private int totalScore; // end score

    public TMP_Text goldScoreText;
    public TMP_Text silverScoreText;
    public TMP_Text bronzeScoreText;
    public TMP_Text multiplierText;
    public TMP_Text totalScoreText;

    private void Start()
    {
        multiplier = calculateMultiplier(); // calculate multiplier at the start of the game
    }
    private int calculateMultiplier() // reads the game's difficulty, assigns corresponding multiplier value
    {
        difficulty = PlayerPrefs.GetString("difficulty");
        if (difficulty == "easy") 
        {
        multiplier = 1;
        } 
        else if (difficulty == "moderate")
        {
            multiplier = 2;
        }
        else if (difficulty == "master")
        {
            multiplier = 3;
        }
        else // this is the totally wrong case
        {
            Debug.Log("ERROR! - HighScore_Controller: Valid difficulty Name not found!");
        }
        Debug.Log("Difficulty Multiplier: " + multiplier);
        return multiplier;
    }
    private int getGoldCoins() // gets gold coin count from CoinCollector Script
    {
        return CoinCollector.goldCoins;
    }

    private int getSilverCoins() 
    {
        return CoinCollector.silverCoins;
    }

    private int getBronzeCoins()
    {
        return CoinCollector.bronzeCoins;
    }
    private void updateCoins() // transfers the coinCollector values to local, essensialy updates the values for this script
    {
        goldCoins = getGoldCoins();
        silverCoins = getSilverCoins();
        bronzeCoins = getBronzeCoins();
    }

    public int calculateScore() // calculates total/end score
    {
        updateCoins(); // update first!
        totalScore = (goldCoins*scorePerGold+silverCoins*scorePerSilver+bronzeCoins*scorePerBronze)* multiplier;
        Debug.Log("Total Score: " + totalScore);
        return totalScore;
    }

    public void printScores() // calculates and updates textboxvalues
    {
        updateCoins();
        goldScoreText.text = goldCoins + " X " + scorePerGold;
        silverScoreText.text = silverCoins + " X " + scorePerSilver;
        bronzeScoreText.text = bronzeCoins + " X " + scorePerBronze;
        multiplierText.text = " X " + multiplier;
        totalScoreText.text = calculateScore().ToString();
        Debug.Log("Gold Score: " + goldCoins*scorePerGold);
        Debug.Log("Silver Score: " + silverCoins*scorePerSilver);
        Debug.Log("Bronze Score: " + bronzeCoins*scorePerBronze);
        Debug.Log("Multiplier|Difficulty: " + multiplier + " | " + difficulty);
        Debug.Log("Total: " + calculateScore());
        showHighScoresContainer(); // show the panel
    }

    private void showHighScoresContainer() // shows the HighScore UI
    {
        if(!outroMusic.isPlaying)
        {
            highscoreMusic.Play();
        }
        else if ( outroMusic.isPlaying && bronzeCoins + silverCoins + goldCoins != 195 )
        {
            //outroMusic.Stop();
        }
        //Time.timeScale = 0f;
        highScores_Canvas.SetActive(true);
        highScores_Panel.SetActive(true);
        highScores_Container.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined; // release the cursor
    }
}

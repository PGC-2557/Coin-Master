using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public static int coins;
    public static int goldCoins;
    public static int silverCoins;
    public static int bronzeCoins;
    public static int skullCoins;

    public int maxGoldCoins;
    public int maxSilverCoins;
    public int maxBronzeCoins;

    public TextMeshProUGUI goldScoreText;
    public TextMeshProUGUI silverScoreText;
    public TextMeshProUGUI bronzeScoreText;

    public AudioSource coinCollectSound;
    public AudioSource skullCoinCollectSound;
    public AudioSource outroMusic;

    public RawImage goldImage;
    public Animator goldWoah;

    public RawImage silverImage;
    public Animator silverWoah;

    public RawImage bronzeImage;
    public Animator bronzeWoah;

    public GameObject timerObj; // will be used to acces time penalty function

    public GameObject player; //debug


    public void Start()
    {
        resetCoinValues(); // these variables are static and need to be reset in each new game
        updateScoreUI(); // update the corresponding ui

        // -- get animators --
        goldWoah = goldImage.GetComponent<Animator>();
        silverWoah = silverImage.GetComponent<Animator>();
        bronzeWoah = bronzeImage.GetComponent<Animator>();

    }

    public void resetCoinValues() { // resets all coin values to default ( 0 )
        coins = 0;
        goldCoins = 0;
        silverCoins = 0;
        bronzeCoins = 0;
        skullCoins = 0;
    }


    public void OnTriggerEnter(Collider other) { // each time the player collides with a coin --> collect it
        Debug.Log("Coin Collide!");
        if (other.gameObject.tag == "Gold Coin")
        {
            hideCoin(other);
            goldCoins += 1;
            updateScoreUI();
            goldWoah.Play("gold_coin_woah"); // play woah animation on colleceted coin UI
            Debug.Log("Golds: "+goldCoins);
        }
        else if (other.gameObject.tag == "Silver Coin")
        {
            hideCoin(other);
            silverCoins += 1;
            updateScoreUI();
            silverWoah.Play("silver_coin_woah");
            Debug.Log("Silvers: " + silverCoins);
        }
        else if (other.gameObject.tag == "Bronze Coin")
        {
            hideCoin(other);
            bronzeCoins += 1;
            updateScoreUI();
            bronzeWoah.Play("bronze_coin_woah");
            Debug.Log("Bronzes: " + bronzeCoins);
        }
        else if (other.gameObject.tag == "Skull Coin")
        {
            if (outroMusic.isPlaying)
            {
                outroMusic.Stop();
            }
            skullCoins += 1;
            other.gameObject.SetActive(false); //hide the coin
            skullCoinCollectSound.Play(); // play skull sound
            timerObj.GetComponent<Timer>().TimePenaltySkull(); // subtract time penalty
            Debug.Log("Skulls = " + skullCoins);
        }
        else if (other.gameObject.tag == "DEBUG COIN")
        {
            other.gameObject.SetActive(false); //hide the coin
            skullCoinCollectSound.Play(); // play skull sound
            Debug.Log("DEBUG ACTIVATED");
            coins = 195;
            bronzeCoins = 0;
            silverCoins = 0;
            goldCoins = 195;
            updateScoreUI();
        }
        else if (other.gameObject.tag == "DEBUG TELEPORT")
        {
            //other.gameObject.SetActive(false); //hide the coin
            skullCoinCollectSound.Play(); // play skull sound
            Debug.Log("DEBUG ACTIVATED == TELEPORT");
            this.transform.position = new Vector3(238.27f, 33.594f, 47.57f);
        }
    }
    void hideCoin(Collider other) { // hies coin, plays collect sound
        coins += 1;
        coinCollectSound.Play();
        other.gameObject.SetActive(false);
        Debug.Log("Coins = " + coins);
    }

    void updateScoreUI() { // updates UI Score on the top left
        goldScoreText.SetText(goldCoins+"/"+maxGoldCoins);
        silverScoreText.SetText(silverCoins + "/" + maxSilverCoins);
        bronzeScoreText.SetText(bronzeCoins + "/" + maxBronzeCoins);
    }
}

// ---------------------------------------------------------------------------
// PlayerScore.cs
// 
// Calculates and displays the players score during gameplay
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour
{
    public float m_playerScore = 0.0f;
    public GameObject m_playerTarget;
    private Vector3 m_originalPosition;
    private bool m_gameStarted = false;

    private int m_previousScore;
    private float m_deathTimer = 0.0f; //The game is over when the player hasnt moved for more than one second

    public GameObject m_scoreDisplay;
    public Sprite[] m_numberSprites;

    //While the player is running, we check to see if they have passed their current highscore
    //If this happens, we play a confetti explosion particle effect on their location
    private int m_currentHighscore = 0;
    private bool m_highscoreBeaten = false;

    void Start()
    {
        //Get the players original position
        m_originalPosition = m_playerTarget.transform.position;
        //Load the current highscore if it exists
        if (PlayerPrefs.HasKey("Score"))
            m_currentHighscore = PlayerPrefs.GetInt("Score");
    }

    void Update()
    {
        //Calculate and display players score
        m_playerScore = Vector3.Distance(m_originalPosition, m_playerTarget.transform.position);
        DisplayScore((int)m_playerScore);

        //If the player hasnt beaten their highscore yet this run, and there exists a
        //high score to beat, check for when the player beats it and play a particle effect
        //when that happens
        if(!m_highscoreBeaten && m_currentHighscore != 0 && (int)m_playerScore > m_currentHighscore)
        {
            m_highscoreBeaten = true;
            m_playerTarget.transform.FindChild("ConfettiParticle").GetComponent<ParticleSystem>().Emit(10);
        }

        //If the game has started and the players score hasnt changed in the last second
        //then the game is over
        if(m_gameStarted)
        {
            if (m_previousScore == (int)m_playerScore)
            {
                m_deathTimer += Time.deltaTime;
                if(m_deathTimer >= 0.25f)
                {
                    GameOver();
                }
            }
            else
                m_deathTimer = 0.0f;

            m_previousScore = (int)m_playerScore;
        }
    }
    bool gameEnded = false;
    private void GameOver()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        //If there is no highscore saved into the player prefs file, we will
        //automatically send the player to the new highscore scene
        if(!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", (int)m_playerScore);
            Application.LoadLevel("NewHighscore");
            return;
        }

        //Compare the players score to the current highscore to see which
        //scene we will send them to
        bool l_newHighscore = m_playerScore > PlayerPrefs.GetInt("Score");
        if (l_newHighscore)
        {
            PlayerPrefs.SetInt("Score", (int)m_playerScore);
            Application.LoadLevel("NewHighscore");
        }
        else
        {
            PlayerPrefs.SetInt("BadScore", (int)m_playerScore);
            Application.LoadLevel("GameOver");
        }
    }

    private void GameStart()
    {
        Debug.Log("Game started");
        m_gameStarted = true;
    }

    public void DisplayScore(int a_score)
    {
        //Split the score into a list of single digits
        List<int> SingleDigits = new List<int>();
        while (a_score > 0)
        {
            SingleDigits.Add(a_score % 10);
            a_score /= 10;
        }
        //Use the array to update the score display
        for (int i = 0; i < SingleDigits.Count; i++)
        {
            switch (i)
            {
                //Single digit
                case (0):
                    m_scoreDisplay.transform.FindChild("ScoreBackdrop").FindChild("SingleDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Ten digit
                case (1):
                    m_scoreDisplay.transform.FindChild("ScoreBackdrop").FindChild("TenDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Hundred digit
                case (2):
                    m_scoreDisplay.transform.FindChild("ScoreBackdrop").FindChild("HundredDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Thousand digit
                case (3):
                    m_scoreDisplay.transform.FindChild("ScoreBackdrop").FindChild("ThousandDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //All other digits are ignored
                    //If the player gets a score higher than 9999 it wont be displayed, that will probably never happen
                    //unless they find a glitch to stay alive somehow
                default:
                    break;
            }
        }
    }
}
// ---------------------------------------------------------------------------
// PlayerScore.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour
{
    public float m_playerScore = 0.0f;
    public GameObject m_playerTarget;
    private Vector3 m_originalPosition;
    public GameObject m_retryScreen;
    private bool m_gameOver = false;
    public bool m_gameStarted = false;
    private float m_deathTimer = 0.0f; //The game is over when the player hasnt moved for more than one second

    void Start()
    {
        //Get the players original position
        m_originalPosition = m_playerTarget.transform.position;
        m_retryScreen.SetActive(false);
    }

    void Update()
    {
        //Each frame we calculate the players distance from their starting position to see how far they have travelled
        //This is their score
        m_playerScore = Vector3.Distance(m_originalPosition, m_playerTarget.transform.position);
        GameObject.Find("ScoreText").GetComponent<Text>().text = ("Score: " + ((int)m_playerScore));
        if (m_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f;
                Application.LoadLevel(0);
            }
        }
    }

    private void GameStart()
    {
        m_gameStarted = true;
    }

    private void GameOver()
    {
        m_gameOver = true;
        m_retryScreen.SetActive(true);
    }
}
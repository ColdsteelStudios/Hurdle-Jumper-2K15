// ---------------------------------------------------------------------------
// PlayerController.cs
// 
// Moves the player character through the scene and receives player input
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Start counter
    public float m_startCounter;
    private bool m_startCounterActive = true;
    GameObject m_countdownText;
    private bool m_gameStarted = false;

    //Player movement
    public float m_movementAcceleration;
    public float m_movementSpeedCap;
    public float m_currentMoveSpeed = 0.0f;
    private Vector3 m_moveDirection = Vector3.zero;

    //Jumping
    public float m_jumpStrength;
    public float m_gravityStrength;
    public float m_maxJumpHeight;
    private float m_yVelocity = 0.0f;
    private float m_currentJumpHeight = 0.0f;
    private float m_originalYPos;
    private bool m_canJump = true;
    private bool m_jumpStarted = false;
    private bool m_jumpApexReached = false;
    private bool m_jumpLock = false;
    private bool m_jumpEnd = false;

    //Collision
    private CollisionFlags m_collisionFlags;

    void Start()
    {
        //Init references
        m_countdownText = GameObject.Find("CountdownText");
        m_originalYPos = transform.position.y;
        m_currentMoveSpeed += m_movementAcceleration * 100 * Time.deltaTime;
    }

    void Update()
    {
        //The is a 3 second counter before the game begins
        if(m_startCounterActive)
        {
            m_startCounter -= Time.deltaTime;
            //Update the countdown text if we can find the countdown text component
            if(m_countdownText != null)
                m_countdownText.GetComponent<Text>().text = ((int)m_startCounter).ToString();
            if(m_startCounter <= 1.0f)
                StartGame();
        }

        //Once the start counter has completed we run the main game loop
        if(m_gameStarted)
        {
            //Increase move speed if we havnt hit the cap yet
            if (m_currentMoveSpeed < m_movementSpeedCap)
                m_currentMoveSpeed += m_movementAcceleration * Time.deltaTime;
            
            //Move the player forward
            m_moveDirection = Vector3.right * m_currentMoveSpeed;

            Jumping();
        }
    }

    void LateUpdate()
    {
        //Change y velocity based on jump status
        if (m_jumpStarted && !m_jumpApexReached)
            m_yVelocity += m_jumpStrength;
        else if (m_yVelocity > 0.0f)
            m_yVelocity -= m_jumpStrength;

        //Apply jumping velocity to movement vector
        m_moveDirection.y += m_yVelocity;
        //Apply gravity
        m_moveDirection.y += m_gravityStrength;

        //Apply movement and store collision flags for checking if player is grounded
        m_collisionFlags = GetComponent<CharacterController>().Move(m_moveDirection * Time.deltaTime);
    }

    private void Jumping()
    {
        //If the player is able to jump and they aren't in a jump, allow them to start jumping
        if (m_canJump && !m_jumpStarted && !m_jumpLock)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_jumpLock = true;
                m_jumpStarted = true;
            }
        }

        //If we are in the middle of a jump, continue that jump
        if (m_jumpStarted && m_canJump && !m_jumpEnd)
        {
            //Check if the player is still holding the jump key
            if (Input.GetKey(KeyCode.Space))
            {
                //Holding space indicated the player wants to continue their jump
                //We allow this as long as the player has not reached their jump apex
                if (ReachedJumpApex())
                {
                    m_jumpEnd = true;
                    m_canJump = false;
                }
            }
            //If no jumping input has been received, discontinue the jump
            else
            {
                m_jumpEnd = true;
                m_canJump = false;
                m_jumpStarted = false;
            }
        }

        //Release jumplock when player is on the ground and space is released

        //If the player cannot jump, check for them to hit the ground which will allow them to jump again
        if (!m_canJump)
        {
            m_canJump = IsGrounded() ? true : false;
        }

        //Release jump locks when no input is received and the player is on the ground
        if(IsGrounded())
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                m_jumpLock = false;
                m_jumpEnd = false;
            }
        }
    }

    private bool ReachedJumpApex()
    {
        //Calculate current jump height
        m_currentJumpHeight = transform.position.y - m_originalYPos;
        //Check if we have reached the apex of our jump
        return m_jumpApexReached = m_currentJumpHeight > m_maxJumpHeight;
    }

    private bool IsGrounded()
    {
        bool l_returnValue = (m_collisionFlags & CollisionFlags.CollidedBelow) != 0;
        GetComponent<Animator>().SetBool("InAir", !l_returnValue);
        return l_returnValue;
    }

    //Called once the start counter has reached zero
    private void StartGame()
    {
        GetComponent<Animator>().SetBool("Running", true);
        m_startCounterActive = false;
        //Deactivate the countdown timer display if we can find it
        if(m_countdownText != null)
            m_countdownText.SetActive(false);
        m_gameStarted = true;
    }
}
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
	private bool m_canJump = true;
	public float m_jumpStrength;
	public float m_gravityStrength;
	public float m_maxJumpHeight;
	private float m_yVelocity = 0.0f;
	private float m_currentJumpHeight = 0.0f;
	private float m_originalYPos;
	private bool m_jumping = false;
	private bool m_movingUp = false;
	private bool m_movingDown = false;
	public GameObject m_leftFoot;
	public GameObject m_rightFoot;

	//Collision
	private CollisionFlags m_collisionFlags;

	void Start()
	{
		//Init references
		m_countdownText = GameObject.Find("CountdownText");
		m_originalYPos = transform.position.y;
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

			//Pass the players speed to the animation controller so the run animation is played at
			//a faster speed depending on how fast the player is moving
			if(IsGrounded())
				GetComponent<Animator>().speed = m_currentMoveSpeed / 10;

			Jumping();
		}

		GameObject.Find ("Main Camera").SendMessage ("GetMovementVector", m_moveDirection);
	}

	void LateUpdate()
	{
		if(m_movingDown)
		{
			GetComponent<Animator>().SetBool("Left Jump", false);
			GetComponent<Animator>().SetBool("Right Jump", false);
		}

		//When the player starts their jump, we increase their y velocity until
		//they reach the maximum jump height
		if(m_movingUp)
		{
			GetComponent<Animator>().SetBool("Moving Down", false);
			m_yVelocity += m_jumpStrength;
			if(ReachedJumpApex())
			{
				m_movingUp = false;
				m_movingDown = true;
			}
		}
		//Once the player has reached the maximum jump height, we start decreasing their
		//y velocity until they hit the ground
		if(m_movingDown)
		{
			GetComponent<Animator>().SetBool("Moving Down", true);
			m_yVelocity -= m_jumpStrength;
			if(IsGrounded())
			{
				m_movingDown = false;
				m_canJump = true;
			}
		}

		//Apply jumping velocity to movement vector
		m_moveDirection.y += m_yVelocity;
		//Apply gravity
		m_moveDirection.y += m_gravityStrength;

		//Apply movement and store collision flags for checking if player is grounded
		m_collisionFlags = GetComponent<CharacterController>().Move(m_moveDirection * Time.deltaTime);
	}

	private void Jumping()
	{
		if (m_canJump && !m_jumping)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				m_movingDown = false;
				bool l_leftJump = m_leftFoot.transform.position.y < m_rightFoot.transform.position.y;

				if(l_leftJump)
				{
					GetComponent<Animator>().SetBool("Left Jump", true);
					GetComponent<Animator>().SetBool("Right Jump", false);
				}
				else
				{
					GetComponent<Animator>().SetBool("Left Jump", false);
					GetComponent<Animator>().SetBool("Right Jump", true);
				}

				m_canJump = false;
				m_movingUp = true;
				m_yVelocity = m_jumpStrength * 10;
			}
		}
	}

	private bool ReachedJumpApex()
	{
		//Calculate current jump height
		m_currentJumpHeight = transform.position.y - m_originalYPos;
		//Check if we have reached the apex of our jump
		return m_currentJumpHeight > m_maxJumpHeight;
	}

	private bool IsGrounded()
	{
		//Checks if the character collider is sitting on the ground
		bool l_returnValue = (m_collisionFlags & CollisionFlags.CollidedBelow) != 0;
		GetComponent<Animator>().SetBool("InAir", !l_returnValue);
		return l_returnValue;
	}

	//Called once the start counter has reached zero
	private void StartGame()
	{
		m_currentMoveSpeed += m_movementAcceleration * 20;
		//Tell the system object that the game has started so it begins
		//keep score and starts checking for when the game has ended
		GameObject.Find("System").SendMessage("GameStart");
		GetComponent<Animator>().SetBool("Running", true);
		m_startCounterActive = false;
		//Deactivate the countdown timer display if we can find it
		if(m_countdownText != null)
			m_countdownText.SetActive(false);
		m_gameStarted = true;
	}
}
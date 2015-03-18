// ---------------------------------------------------------------------------
// JumpTest.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class JumpTest : MonoBehaviour
{
    public float m_jumpStrength;
    public float m_gravityStrength;
    public float m_maxJumpHeight;
    private float m_yVelocity = 0.0f;
    private float m_currentJumpHeight = 0.0f;
    private float m_originalYPos;
    private bool m_canJump = true;
    private bool m_jumpStarted = false;
    private bool m_jumpApexReached = false;

    //Collision
    private CollisionFlags m_collisionFlags;

    void Start()
    {
        m_originalYPos = transform.position.y;
    }

    void Update()
    {
        //If the player is able to jump and they aren't in a jump, allow them to start jumping
        if (m_canJump && !m_jumpStarted)
            if (Input.GetKey(KeyCode.Space))
                m_jumpStarted = true;

        //If we are in the middle of a jump, continue that jump
        if (m_jumpStarted && m_canJump)
        {
            //Check if the player is still holding the jump key
            if (Input.GetKey(KeyCode.Space))
            {
                //Holding space indicated the player wants to continue their jump
                //We allow this as long as the player has not reached their jump apex
                if (ReachedJumpApex())
                    m_canJump = false;
            }
            //If no jumping input has been received, discontinue the jump
            else
            {
                m_canJump = false;
                m_jumpStarted = false;
            }
        }

        //If the player cannot jump, check for them to hit the ground which will allow them to jump again
        if (!m_canJump)
            m_canJump = IsGrounded() ? true : false;
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
        return (m_collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    void LateUpdate()
    {
        //Change velocity based on jump status
        if (m_jumpStarted && !m_jumpApexReached)
            m_yVelocity += m_jumpStrength;
        else if (m_yVelocity > 0.0f)
            m_yVelocity -= m_jumpStrength;

        //Create movement vector
        Vector3 l_movement = Vector3.zero;
        //Apply jumping velocity
        l_movement.y += m_yVelocity;
        //Apply gravity
        l_movement.y += m_gravityStrength;

        //Apply movement and store collision flags for checking if player is grounded
        m_collisionFlags = GetComponent<CharacterController>().Move(l_movement * Time.deltaTime);
    }
}
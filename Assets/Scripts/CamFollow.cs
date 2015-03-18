// ---------------------------------------------------------------------------
// CamFollow.cs
// 
// Has the camera follow the player character as they run along the track
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour
{
    public GameObject m_followTarget;
    private Vector3 m_desiredOffset;
    private float m_originalYElevation;

    void Start()
    {
        //The desired offset will be the difference between the targets position
        //and the cameras position at the start of the scene
        m_desiredOffset = m_followTarget.transform.position - transform.position;
        m_originalYElevation = transform.position.y;
    }

    void Update()
    {
        //Find the cameras desired position
        Vector3 l_desiredPosition = m_followTarget.transform.position - m_desiredOffset;
        //We want the cameras elevation to remain the same when the player is jumping
        l_desiredPosition.y = m_originalYElevation;
        //Get the players movement speed as we want the camera to move faster as the player speeds up
        float l_playerSpeed = m_followTarget.GetComponent<PlayerController>().m_currentMoveSpeed;
        //Lerp the camera to the desired position so it smoothly follows the target
        transform.position = Vector3.Lerp(transform.position, l_desiredPosition, l_playerSpeed * Time.deltaTime);
    }
}
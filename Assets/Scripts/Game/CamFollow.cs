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
    private float m_followSpeed = 0f;
    private float m_camSmooth = 20.0f;

    private Vector3 m_cameraPreviousPos = Vector3.zero;
    private Vector3 m_playerPreviousPos = Vector3.zero;

    void Start()
    {
        //The desired offset will be the difference between the targets position
        //and the cameras position at the start of the scene
        m_desiredOffset = m_followTarget.transform.position - transform.position;
        m_originalYElevation = transform.position.y;
        m_playerPreviousPos = m_followTarget.transform.position;
        m_cameraPreviousPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 l_desiredPosition = m_followTarget.transform.position - m_desiredOffset;
        l_desiredPosition.y = m_originalYElevation;

        transform.position = SmoothApproach(m_cameraPreviousPos, m_playerPreviousPos, l_desiredPosition, 20.0f);
        m_playerPreviousPos = l_desiredPosition;
        m_cameraPreviousPos = transform.position;
        /*
        if (m_followTarget != null)
        {
            //Find the cameras desired position
            Vector3 l_desiredPosition = m_followTarget.transform.position - m_desiredOffset;
            //We want the cameras elevation to remain the same when the player is jumping
            l_desiredPosition.y = m_originalYElevation;
            //Get the players movement speed as we want the camera to move faster as the player speeds up
            if (m_followTarget.GetComponent<PlayerController>())
                m_followSpeed = m_followTarget.GetComponent<PlayerController>().m_currentMoveSpeed;
            //Lerp the camera to the desired position so it smoothly follows the target
            transform.position = Vector3.Lerp(transform.position, l_desiredPosition, Time.deltaTime * m_followSpeed);
        }
         * */
    }

    Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
    {
        float t = (1 - Mathf.Exp(-20 * Time.deltaTime)) * speed;
        Vector3 v = (targetPosition - pastTargetPosition) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp(-t);
    }
}
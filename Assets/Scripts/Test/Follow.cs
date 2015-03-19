// ---------------------------------------------------------------------------
// Follow.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public GameObject m_followTarget;

    private Vector3 m_previousPos;
    private Vector3 m_previousTargetPos;

    private Vector3 m_camOffset;
    private float m_originalYElevation;

    void Start()
    {
        Vector3 l_desiredPosition = m_followTarget.transform.position + m_camOffset;
        l_desiredPosition.y = m_originalYElevation;
        m_previousPos = l_desiredPosition;
        m_previousTargetPos = m_followTarget.transform.position;

        m_camOffset = transform.position - m_followTarget.transform.position;
        m_originalYElevation = transform.position.y;
    }

    void FixedUpdate()
    {
        Vector3 l_desiredPosition = m_followTarget.transform.position + m_camOffset;
        l_desiredPosition.y = m_originalYElevation;

        transform.position = SuperSmoothLerp(m_previousPos, m_previousTargetPos, l_desiredPosition, Time.deltaTime, 5);
        m_previousPos = transform.position;
        m_previousTargetPos = l_desiredPosition;
    }

    Vector3 SuperSmoothLerp(Vector3 x0, Vector3 y0, Vector3 yt, float t, float k)
    {
        Vector3 f = x0 - y0 + (yt - y0) / (k * t);
        return yt - (yt - y0) / (k * t) + f * Mathf.Exp(-k * t);
    }
}
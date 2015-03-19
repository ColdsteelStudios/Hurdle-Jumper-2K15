// ---------------------------------------------------------------------------
// Slowmo.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Slowmo : MonoBehaviour
{
    public float m_gameSpeed;

    void Start ()
    {
        Time.timeScale = m_gameSpeed;
        Time.fixedDeltaTime = m_gameSpeed * 0.02f;
	}
}
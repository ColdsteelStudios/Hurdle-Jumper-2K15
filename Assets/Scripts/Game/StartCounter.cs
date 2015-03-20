// ---------------------------------------------------------------------------
// StartCounter.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCounter : MonoBehaviour
{
    public Sprite[] m_numberSprites;

    private void Display(int a_displayNumber)
    {
        GetComponent<Image>().sprite = m_numberSprites[a_displayNumber];
    }
}
// ---------------------------------------------------------------------------
// ButtonControl.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour
{
    public void TriggerPlayButton()
    {
        Application.LoadLevel("Game");
    }

    public void TriggerQuitButton()
    {
        Application.Quit();
    }
}
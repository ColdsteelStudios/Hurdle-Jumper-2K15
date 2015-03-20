// ---------------------------------------------------------------------------
// HighscoreDisplay.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighscoreDisplay : MonoBehaviour
{
    public Sprite[] m_numberSprites;

    void Start()
    {
        int l_highscore = PlayerPrefs.GetInt("Score");

        //Split the score into a list of single digits
        List<int> SingleDigits = new List<int>();
        while (l_highscore > 0)
        {
            SingleDigits.Add(l_highscore % 10);
            l_highscore /= 10;
        }
        //Use the array to update the score display
        for (int i = 0; i < SingleDigits.Count; i++)
        {
            switch (i)
            {
                //Single digit
                case (0):
                    transform.FindChild("SingleDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Ten digit
                case (1):
                    transform.FindChild("TenDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Hundred digit
                case (2):
                    transform.FindChild("HundredDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Thousand digit
                case (3):
                    transform.FindChild("ThousandDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //All other digits are ignored
                default:
                    break;
            }
        }
    }
}
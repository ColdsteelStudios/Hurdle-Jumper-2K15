// ---------------------------------------------------------------------------
// ScoreDisplay.cs
// 
// Changes the sprites for each digit to display the score that is passed in
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour
{
    public Sprite[] m_numberSprites;

    void Start()
    {
        PlayerPrefs.DeleteKey("Score");
        //Check if there is already a highscore saved
        if(PlayerPrefs.HasKey("Score"))
        {
            //Grab that score and display it
            DisplayScore(PlayerPrefs.GetInt("Score"));
        }
    }

    public void DisplayScore(int a_score)
    {
        //Split the score into a list of single digits
        List<int> SingleDigits = new List<int>();
        while(a_score > 0)
        {
            SingleDigits.Add(a_score % 10);
            a_score /= 10;
        }
        //Use the array to update the score display
        for(int i = 0; i < SingleDigits.Count; i++)
        {
            switch(i)
            {
                //Single digit
                case(0):
                    transform.FindChild("ScoreBackdrop").FindChild("SingleDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Ten digit
                case(1):
                    transform.FindChild("ScoreBackdrop").FindChild("TenDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Hundred digit
                case(2):
                    transform.FindChild("ScoreBackdrop").FindChild("HundredDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //Thousand digit
                case(3):
                    transform.FindChild("ScoreBackdrop").FindChild("ThousandDigit").GetComponent<Image>().sprite = m_numberSprites[SingleDigits[i]];
                    break;

                //All other digits are ignored
                default:
                    break;
            }
        }
    }
}
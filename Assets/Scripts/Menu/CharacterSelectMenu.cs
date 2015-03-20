// ---------------------------------------------------------------------------
// CharacterSelectMenu.cs
// 
// Play and back buttons for the character select menu
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectMenu : MonoBehaviour
{
    public void TriggerPlay()
    {
        //Get the array of available characters
        List<GameObject> l_characters = new List<GameObject>();
        foreach (Transform character in GameObject.Find("Character Wheel").transform)
            l_characters.Add(character.gameObject);
        
        //GameObject[] l_characters = GameObject.Find("Character Wheel").GetComponent<CharSelectWheel>().m_playerCharacters;
        //Get camera to compare distance
        GameObject l_camera = GameObject.Find("Main Camera");
        //Find which one is closest to the camera
        GameObject l_closestCharacter = l_characters[0];
        float l_closestDistance = Vector3.Distance(l_characters[0].transform.position, l_camera.transform.position);
        for ( int i = 1; i < l_characters.Count; i++ )
        {
            float l_compareDistance = Vector3.Distance(l_characters[i].transform.position, l_camera.transform.position);
            if ( l_compareDistance < l_closestDistance )
            {
                l_closestCharacter = l_characters[i];
                l_closestDistance = l_compareDistance;
            }
        }

            //Now we know which character was selected, take note of that and move to the gameplay screen
            PlayerPrefs.SetString("Player", l_closestCharacter.GetComponent<PlayerController>().m_name);
        Application.LoadLevel("Game");
    }

    public void TriggerBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
// ---------------------------------------------------------------------------
// PlayerSpawn.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] m_players;

    void Start()
    {
        //Load player type from prefs
        string l_playerType = PlayerPrefs.GetString("Player");
        
        for(int i = 0; i < m_players.Length; i++)
        {
            //Loop through list finding which player to spawn
            if(l_playerType == m_players[i].GetComponent<PlayerController>().m_name)
            {
                GameObject l_player = GameObject.Instantiate(m_players[i], transform.position, transform.rotation) as GameObject;
                GameObject.Find("System").GetComponent<PlayerScore>().SetPlayer(l_player);
                return;
            }
        }
    }
}
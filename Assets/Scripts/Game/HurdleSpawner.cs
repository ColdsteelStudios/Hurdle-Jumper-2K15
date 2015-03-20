// ---------------------------------------------------------------------------
// HurdleSpawner.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class HurdleSpawner : MonoBehaviour
{
    public GameObject m_hurdlePrefab;
    public Texture2D m_jumpTexture;
    public Texture2D m_runTexture;

    private void SpawnHurdle(int a_playerDistance)
    {
        //Decide whether or not to spawn a hurdle on this track section based on how far the player has travelled
        //Every 100 points the player has gives an extra 10% chance for this track for spawn a hurdle
        float l_playerScore = GameObject.Find("System").GetComponent<PlayerScore>().m_playerScore;
        l_playerScore = l_playerScore / 10;
        l_playerScore += 15;
        int l_hurdleSpawnChance = Random.Range(0, 100);
        if(l_hurdleSpawnChance <= l_playerScore)
        {
            transform.FindChild("Stadium").GetComponent<Renderer>().material.mainTexture = m_jumpTexture;
            Vector3 l_hurdleSpawnPosition = transform.FindChild("RunningTrack").position;
            float l_sectionSize = transform.FindChild("Grass").GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
            l_hurdleSpawnPosition.x += Random.Range(-l_sectionSize * 0.5f, l_sectionSize * 0.5f);
            l_hurdleSpawnPosition.y += 1.0f;
            GameObject.Instantiate(m_hurdlePrefab, l_hurdleSpawnPosition, transform.FindChild("RunningTrack").rotation);   
        }
        else
            transform.FindChild("Stadium").GetComponent<Renderer>().material.mainTexture = m_runTexture;
    }
}
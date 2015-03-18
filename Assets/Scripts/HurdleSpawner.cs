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

    private void SpawnHurdle(int a_playerDistance)
    {
        //Decide whether or not to spawn a hurdle on this track section based on how far the player has travelled

        Vector3 l_hurdleSpawnPosition = transform.FindChild("RunningTrack").position;
        float l_sectionSize = transform.FindChild("Grass").GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
        l_hurdleSpawnPosition.x += Random.Range(-l_sectionSize * 0.5f, l_sectionSize * 0.5f);
        l_hurdleSpawnPosition.y += 1.0f;
        GameObject.Instantiate(m_hurdlePrefab, l_hurdleSpawnPosition, transform.FindChild("RunningTrack").rotation);
    }
}
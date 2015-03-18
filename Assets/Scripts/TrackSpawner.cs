// ---------------------------------------------------------------------------
// TrackSpawner.cs
// 
// When told, spawns the next section of track
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class TrackSpawner : MonoBehaviour
{
    public GameObject m_trackPrefab;

    private void SpawnNextTrack()
    {
        //First we want to calculate the position where we want to spawn the next track section

        //Find the width of a track section
        float l_trackSectionWidth = transform.FindChild("Grass").GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
        //Get the position of this track and offset it by the width of a track section
        //This will give us the correct location to spawn the next section of track
        Vector3 l_spawnLocation = transform.position;
        l_spawnLocation.x += l_trackSectionWidth - 0.2f;
        //Spawn the next track section in this location
        GameObject l_newTrackPiece = GameObject.Instantiate(m_trackPrefab, l_spawnLocation, transform.rotation) as GameObject;
        //Tell the new track piece to spawn hurdles
        l_newTrackPiece.SendMessage("SpawnHurdle", 0.0f);
    }
}
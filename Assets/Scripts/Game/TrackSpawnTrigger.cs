// ---------------------------------------------------------------------------
// TrackSpawnTrigger.cs
// 
// Tells tracks to spawn the next section in front of us when we are getting close enough
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class TrackSpawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "TrackStartTrigger")
            other.transform.root.SendMessage("SpawnNextTrack");
    }
}
// ---------------------------------------------------------------------------
// TrackDespawnTrigger.cs
// 
// Detects when to despawn tracks that are behind us and no longer needed
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class TrackDespawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "TrackEndTrigger")
            GameObject.Destroy(other.transform.root.gameObject);
    }
}
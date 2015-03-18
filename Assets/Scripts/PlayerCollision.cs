// ---------------------------------------------------------------------------
// PlayerCollision.cs
// 
// Collision detection for player character
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.transform.tag == "Obstacle")
            transform.SendMessage("Ragdoll");
         * */
    }
}
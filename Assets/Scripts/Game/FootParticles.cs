// ---------------------------------------------------------------------------
// FootParticles.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class FootParticles : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            transform.FindChild("DustKickupParticle").GetComponent<ParticleSystem>().Emit(3);
        }
    }
}
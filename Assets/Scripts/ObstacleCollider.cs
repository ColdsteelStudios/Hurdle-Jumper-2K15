// ---------------------------------------------------------------------------
// ObstacleCollider.cs
// 
// Kills the player when they collide with this
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class ObstacleCollider : MonoBehaviour
{
	public AudioClip m_deathSound;

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			other.transform.root.SendMessage("Ragdoll");
			GetComponent<AudioSource>().PlayOneShot(m_deathSound);
			GetComponent<ParticleSystem>().Emit(10);
		}
	}
}
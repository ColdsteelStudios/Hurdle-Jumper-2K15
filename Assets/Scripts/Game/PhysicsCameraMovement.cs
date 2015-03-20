// ---------------------------------------------------------------------------
// PhysicsCameraMovement.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PhysicsCameraMovement : MonoBehaviour
{
	private Vector3 m_movementVector = Vector3.zero;

	public void GetMovementVector(Vector3 a_movementVector)
	{
		m_movementVector = a_movementVector;
	}

	void Update()
	{
		m_movementVector.y = 0.0f;
		GetComponent<CharacterController>().Move(m_movementVector * Time.deltaTime);
		m_movementVector = Vector3.ClampMagnitude( m_movementVector, m_movementVector.magnitude - (m_movementVector.magnitude * .01f));
	}
}
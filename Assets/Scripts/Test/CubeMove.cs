// ---------------------------------------------------------------------------
// CubeMove.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour
{
    void FixedUpdate ()
    {
        transform.position = transform.position + (Vector3.right * 1.0f);
	}
}
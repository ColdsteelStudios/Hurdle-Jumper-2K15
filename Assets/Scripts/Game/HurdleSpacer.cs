// ---------------------------------------------------------------------------
// HurdleSpacer.cs
// 
// Stops hurdles being too close together after they are spawned in
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class HurdleSpacer : MonoBehaviour
{
    void Start ()
    {
        //Shoot a ray backwards to see how close the previous hurdle is
        RaycastHit hit;
        int l_layerMask = 1 << 9;
        if (Physics.Raycast(transform.position, -Vector3.right, out hit, Mathf.Infinity, l_layerMask))
        {
            if(hit.transform.root.tag == "Obstacle")
            {
                //Find the distance between this hurdle and the one behind us
                float l_distance = Vector3.Distance(transform.position, hit.transform.root.transform.position);
                if (l_distance < 10.0f)
                    transform.position = hit.transform.root.transform.position + (Vector3.right * 10.0f);
            }
        }
    }
}
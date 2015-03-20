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
    public GameObject m_ragdollPrefab;
    private bool m_replaced = false;

    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;
    }

    private void Ragdoll()
    {
        if (m_replaced)
            return;

        m_replaced = true;

        GameObject RD = GameObject.Instantiate(m_ragdollPrefab, transform.position, transform.rotation) as GameObject;

        Transform[] ragdollJoints = RD.GetComponentsInChildren<Transform>();
        Transform[] currentJoints = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < ragdollJoints.Length; i++)
        {
            for (int j = 0; j < currentJoints.Length; j++)
            {
                if (currentJoints[j].name.CompareTo(ragdollJoints[i].name) == 0)
                {
                    ragdollJoints[i].position = currentJoints[j].position;
                    ragdollJoints[i].rotation = currentJoints[j].rotation;
                    break;
                }
            }
        }

        //Slow motion and increase physics timestep so it doesnt become choppy
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.25f * 0.02f;

        //Update score target to ragdoll
        GameObject.Find("System").GetComponent<PlayerScore>().m_playerTarget = RD.transform.FindChild("Character1_Rig").FindChild("Character1_Hips").gameObject;

        //Apply a force to the ragdoll depending on how fast the player was moving
        float l_playerSpeed = GetComponent<PlayerController>().m_currentMoveSpeed;
        Vector3 l_forceVector = Vector3.right * l_playerSpeed;
        RD.transform.FindChild("Character1_Rig").FindChild("Character1_Hips").GetComponent<Rigidbody>().AddForce(l_forceVector * 5, ForceMode.VelocityChange);

        GameObject.Destroy(transform.root.gameObject);
    }
}
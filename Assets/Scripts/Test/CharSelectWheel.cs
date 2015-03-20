// ---------------------------------------------------------------------------
// CharSelectWheel.cs
// 
// Allows player to drag the character selection wheel around
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CharSelectWheel : MonoBehaviour
{
    public int m_selectionCount;
    public GameObject m_characterPrefabPlaceholder;

    //How many degrees of rotation between each character
    private float m_rotationSegment;
    private float m_currentRotation;

    void Start()
    {
        //Figure out how much rotation there will be between each character on the wheel
        m_rotationSegment = 360f / (float)m_selectionCount;
        m_currentRotation = 0f;
        //Place each character onto the correct position on the wheel
        float l_wheelWidth = GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
        for(int i = 0; i < m_selectionCount; i++)
        {
            //Calculate the position to spawn it
            Vector3 l_spawnPos = transform.position;
            l_spawnPos.y += .75f;
            //Spawn it
            GameObject l_charSpawn = GameObject.Instantiate(m_characterPrefabPlaceholder, l_spawnPos, Quaternion.Euler(0f,i*m_rotationSegment,0f)) as GameObject;
            //Move it forward so it sits on the outside of the wheel
            l_charSpawn.transform.position += l_charSpawn.transform.forward * l_wheelWidth;
            //Parent it to the wheel so its moved around as the wheel is rotated
            l_charSpawn.transform.parent = transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            RotateLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            RotateRight();
    }

    private void RotateLeft()
    {

    }

    private void RotateRight()
    {

    }
}
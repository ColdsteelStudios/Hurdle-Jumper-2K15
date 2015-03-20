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
    public GameObject[] m_playerCharacters;

    //How many degrees of rotation between each character
    private float m_rotationSegment;
    private float m_currentRotation;

    //Changing selection
    private float m_rotationDuration = 1f; //How long it takes to rotate from one character to the next in the wheel
    private float m_rotDurRemaining;
    private bool m_canRotate; //Can the player currently change their selection?

    //Mouse/Finger drag to change selection
    private Vector3 m_dragStart;

    void Start()
    {
        //Figure out how much rotation there will be between each character on the wheel
        m_rotationSegment = 360f / (float)m_playerCharacters.Length;
        m_currentRotation = 0f;
        //Place each character onto the correct position on the wheel
        float l_wheelWidth = GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
        for (int i = 0; i < m_playerCharacters.Length; i++)
        {
            //Calculate the position to spawn it
            Vector3 l_spawnPos = transform.position;
            //Spawn it
            GameObject l_charSpawn = GameObject.Instantiate(m_playerCharacters[i], l_spawnPos, Quaternion.Euler(0f, i * m_rotationSegment, 0f)) as GameObject;
            //Stop the player from doing anything
            l_charSpawn.GetComponent<PlayerController>().m_canPlay = false;
            //Move it forward so it sits on the outside of the wheel
            l_charSpawn.transform.position += l_charSpawn.transform.forward * (l_wheelWidth*2);
            //Move it up a little
            l_charSpawn.transform.position += l_charSpawn.transform.up * .3f;
            //Parent it to the wheel so its moved around as the wheel is rotated
            l_charSpawn.transform.parent = transform;
        }
    }

    void Update()
    {
        if (m_canRotate)
        {
            //Allow player to change selection with arrow keys
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                RotateLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                RotateRight();
            //Or by click dragging the mouse of dragging finger
            if (Input.GetMouseButton(0))
                m_dragStart = Input.mousePosition;
            //End drag
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 l_dragEndPos = Input.mousePosition;
                if (l_dragEndPos.x > m_dragStart.x)
                    RotateRight();
                else
                    RotateLeft();
            }
        }
        else
        {
            //Smooth rotation between characters and
            //allow player to change selection again
            //when the rotation has finished
            m_rotDurRemaining -= Time.deltaTime;
            if (m_rotDurRemaining <= 0.0f)
            {
                m_canRotate = true;
                transform.rotation = Quaternion.Euler(0f, m_currentRotation, 0f);
            }
        }
    }

    private void RotateLeft()
    {
        //Keep track of current desired rotation
        m_currentRotation += m_rotationSegment;
        //Increase current rotation by one segment
        m_canRotate = false;
        m_rotDurRemaining = m_rotationDuration;
        StartCoroutine(RotateMe(Vector3.up * m_rotationSegment, 1f));
    }

    private void RotateRight()
    {
        m_currentRotation -= m_rotationSegment;
        //Decrease current rotation by one segment
        m_canRotate = false;
        m_rotDurRemaining = m_rotationDuration;
        StartCoroutine(RotateMe(Vector3.up * -m_rotationSegment, 1f));
    }

    //Coroutine to change rotation of selection wheel
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
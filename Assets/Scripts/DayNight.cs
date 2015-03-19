// ---------------------------------------------------------------------------
// DayNight.cs
// 
// Controls light intensity, alpha's out the sky blue for the games day night cycle
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class DayNight : MonoBehaviour
{
    public GameObject m_daySky;//The blue sky backdrop
    public GameObject m_sun;
    private GameObject m_sunLight;//Directional light attached to the sun
    public GameObject m_moon;

    private float m_sunMax = 21.59244f;//Maximum height the sun can reach (midday)
    private float m_sunOrig = -1.272257f;//Middle height the sun can reach (halfway between midday and midnight)
    private float m_sunMin = -28.62368f;//Minimum height the sun can reach (midnight)

    void Start()
    {
        m_sunLight = m_sun.transform.FindChild("Directional light").gameObject;
    }

    void Update()
    {
        //If the suns height is above the middle position, it is day time
        if (m_sun.transform.position.y > m_sunOrig)
        {
            //Keep the suns directional light active during the day
            m_sunLight.SetActive(true);

            //Find how far the sun is from its apex
            Vector3 l_sunOrig = new Vector3(0f, m_sunOrig, 0f);
            Vector3 l_sunFoo = new Vector3(0f, m_sun.transform.position.y, 0f);
            float l_sunDistance = Vector3.Distance(l_sunOrig, l_sunFoo);

            //Calculate a percentage difference between the suns default and maximum heights
            float l_dayPercentage = l_sunDistance / m_sunMax;
            l_dayPercentage *= 100;
            l_dayPercentage = l_dayPercentage > 100 ? 100 : l_dayPercentage;
            l_dayPercentage = l_dayPercentage < 0 ? 0 : l_dayPercentage;

            //Increase the intensity of the sun light to a maximum at midday and a minimum once the night starts
            m_sunLight.GetComponent<Light>().intensity = l_dayPercentage / 100f;

            //Stop the sky backdrop from being alphad out during daytime
            Color l_skyColor = m_daySky.GetComponent<Renderer>().material.color;
            l_skyColor.a = 1.0f;
            m_daySky.GetComponent<Renderer>().material.color = l_skyColor;
        }
        else if (m_moon.transform.position.y > m_sunOrig)
        {//night
            //Deactive the suns direction light when its nighttime
            m_sunLight.SetActive(false);

            //Find how far the moon is from its apex
            Vector3 l_sunOrig = new Vector3(0f, m_sunOrig, 0f);
            Vector3 l_sunFoo = new Vector3(0f, m_moon.transform.position.y, 0f);
            float l_sunDistance = Vector3.Distance(l_sunOrig, l_sunFoo);

            //Calculate a percentage difference between the moons default and maximum heights
            float l_nightPercentage = l_sunDistance / m_sunMax;
            l_nightPercentage *= 100;
            l_nightPercentage = l_nightPercentage > 100 ? 100 : l_nightPercentage;
            l_nightPercentage = l_nightPercentage < 0 ? 0 : l_nightPercentage;

            //Alpha out the sky backdrop as it gets later into the night
            Color l_skyColor = m_daySky.GetComponent<Renderer>().material.color;
            l_skyColor.a = 1f - (l_nightPercentage / 100f);
            m_daySky.GetComponent<Renderer>().material.color = l_skyColor;
        }
    }
}
// ---------------------------------------------------------------------------
// MenuControl.cs
// 
// Initially displays the national sports musuem logo
// After 3 seconds this fades out, when museum is gone the menu fades in
// When menu has finished fading in the player will be able to interact with it
// Also contains trigger functions for the menu buttons
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuControl : MonoBehaviour
{
    public GameObject MuseumSplash;
    public GameObject MenuDisplay;
    public GameObject ScoreDisplay;

    private float SplashDisplayTime = 3.0f;
    private float SplashFadeOutTime = 1.5f;
    private float SplashFadeOutTimeRemaining = 1.5f;
    private float MenuFadeInTimeRemaining = 1.5f;
    private bool MenuInteractable = false;

    void Update()
    {
        //Display splash, fade it out, then fade in menu
        if(!MenuInteractable)
            FadeMenu();
    }

    public void TriggerPlayButton()
    {
        if (MenuInteractable)
            Application.LoadLevel("Game");
    }

    public void TriggerQuitButton()
    {
        if (MenuInteractable)
            Application.Quit();
    }

    private void FadeMenu()
    {
        //Display the splash screen for x seconds
        if (SplashDisplayTime > 0.0f)
            SplashDisplayTime -= Time.deltaTime;
        else
        {
            //Fade the splash out over x seconds
            if (SplashFadeOutTimeRemaining > 0.0f)
            {
                SplashFadeOutTimeRemaining -= Time.deltaTime;
                //Calculate a new alpha amount to apply to the splash screen
                //This is a percentage of how much time it has left to fade out so
                //it goes from 100% to 0% alpha over the course of x seconds
                float FadeOutAmount = SplashFadeOutTimeRemaining / SplashFadeOutTime;
                MuseumSplash.GetComponent<CanvasRenderer>().SetAlpha(FadeOutAmount);
                
                //Check if the splash screen has finished fading out, when this happens
                //we want to active the other menu components and alpha them out so they
                //are ready for the next state to fade them in
                if(SplashFadeOutTimeRemaining <= 0.0f)
                {
                    //Active and hide child objects of the interactable part of the menu
                    MenuDisplay.SetActive(true);
                    foreach(Transform child in MenuDisplay.transform)
                    {
                        //If the child object has a canvas renderer, set its alpha value
                        if (child.GetComponent<CanvasRenderer>() != null)
                            child.GetComponent<CanvasRenderer>().SetAlpha(0f);
                    }
                    //Hide score display background
                    ScoreDisplay.transform.FindChild("ScoreBackdrop").GetComponent<CanvasRenderer>().SetAlpha(0f);
                    //Hide child objects of the score display
                    foreach(Transform child in ScoreDisplay.transform.FindChild("ScoreBackdrop").transform)
                    {
                        if (child.GetComponent<CanvasRenderer>() != null)
                            child.GetComponent<CanvasRenderer>().SetAlpha(0f);
                    }
                }
            }
            else if (MenuFadeInTimeRemaining > 0.0f)
            {
                //Fade in the main menu over x seconds, in the same way the splash
                //screen was faded out

                //Calculate the alpha value to apply this frame
                MenuFadeInTimeRemaining -= Time.deltaTime;
                float FadeInAmount = 1 - (MenuFadeInTimeRemaining / SplashFadeOutTime);
                
                //Menu interactable children
                foreach (Transform child in MenuDisplay.transform)
                {
                    //If the child object has a canvas renderer, set its alpha value
                    if (child.GetComponent<CanvasRenderer>() != null)
                        child.GetComponent<CanvasRenderer>().SetAlpha(FadeInAmount);
                }
                //Score display background
                ScoreDisplay.transform.FindChild("ScoreBackdrop").GetComponent<CanvasRenderer>().SetAlpha(FadeInAmount);
                //Score display digits
                foreach (Transform child in ScoreDisplay.transform.FindChild("ScoreBackdrop").transform)
                {
                    if (child.GetComponent<CanvasRenderer>() != null)
                        child.GetComponent<CanvasRenderer>().SetAlpha(FadeInAmount);
                }

                //When the menu has finished fading it, make it so the player is able to
                //interact with it
                if (MenuFadeInTimeRemaining <= 0.0f)
                    MenuInteractable = true;
            }
        }
    }
}
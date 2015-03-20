// ---------------------------------------------------------------------------
// ButtonImageChange.cs
// 
// Class description goes here
// 
// Original Author: Harley Laurie
// ---------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonImageChange : MonoBehaviour 
{
	public Sprite NormalSprite;
	public Sprite MouseHoverSprite;
    public Sprite MouseClickSprite;
	private Image ButtonImage;

	void Start()
	{
		ButtonImage = gameObject.GetComponent<Image> ();
	}

	public void MouseEnter()
	{
		if(MouseHoverSprite!=null)
			ButtonImage.sprite = MouseHoverSprite;
	}

	public void MouseExit()
	{
		ButtonImage.sprite = NormalSprite;
	}

    public void MouseClick()
    {
		if(MouseClickSprite!=null)
        	ButtonImage.sprite = MouseClickSprite;
    }
}
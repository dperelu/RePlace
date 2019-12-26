using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateTextPush : MonoBehaviour {

	public TextMeshProUGUI PressButton;

	public void showText(char letter)
	{
        PressButton.text = "Press '" + letter + "' to interact";


        Color buttonColor = PressButton.color;

		buttonColor = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 255);

		PressButton.color = buttonColor;
	}

	public void hideText()
	{
		Color buttonColor = PressButton.color;

		buttonColor = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0);

		PressButton.color = buttonColor;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

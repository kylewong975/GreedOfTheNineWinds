using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
The PausePlay script regulates the pausing and playing of the game in the build. Within this script,
the pause menu is shown, which contains a Main Menu button, Music On/Off button, and an Exit Game button. Once
the pause menu is shown, the game is temporarily paused, which means all activities such as player movement and
mob spawn is temporarily put on hiatus until the player presses the pause button again to resume.
*/
public class PausePlay : MonoBehaviour {

	public Button pButton;			//the Pause button
	public Image pauseBackground;	//the background Image when the pause button is pressed
	public Button mainMenu;         //the Main Menu button within the pause menu
	public Button music;            //the Music On/Off button within the pause menu

	bool play;                      //determines if the music should be played or not

	/*
	Upon instantiation, the Start function ensures that the scene is not paused upon loading.
	*/
	void Start () 
	{
		Time.timeScale = 1.0f;
		play = true;
	}

	/*
	The Update function also allows for the escape key and the "P" key to achieve the same effect as clicking
	the Pause button.
	*/
	void Update()
	{
		if (Input.GetButtonDown ("Cancel")) // || Input.GetButtonDown("PausePlay"))
		{
			Clicked ();
		}
	}

	/*
	The Clicked function pauses or plays the game.
	It shows the pause background, main menu button and music on/off button on pause.
	Another click will result in the game to resume.
	*/
	public void Clicked()
	{
		if (play)
		{
			Time.timeScale = 0.0f;
			play = false;
			pButton.GetComponentInChildren<Text>().text = "►";
			pButton.GetComponentInChildren<Text>().fontSize = 28;
			mainMenu.gameObject.SetActive(true);
			music.gameObject.SetActive(true);
			pauseBackground.gameObject.SetActive(true);

		}
		else
		{
			Time.timeScale = 1.0f;
			play = true;
			pButton.GetComponentInChildren<Text>().text = "▋▋";
			pButton.GetComponentInChildren<Text>().fontSize = 16;
			mainMenu.gameObject.SetActive(false);
			music.gameObject.SetActive(false);
			pauseBackground.gameObject.SetActive(false);
		}
	}
}
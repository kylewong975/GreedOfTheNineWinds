using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

	public void SwitchMainMenu() {
		SceneManager.LoadScene ("StartScene");
	}

	public void SwitchPlay() {
		SceneManager.LoadScene ("TestLevel0");
	}

	public void SwitchOptions() {
		SceneManager.LoadScene ("OptionsScene");
	}

	public void SwitchInstructions() {
		SceneManager.LoadScene ("InstructionsScene");
	}

	public void SwitchAbout() {
		SceneManager.LoadScene ("AboutScreen");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}

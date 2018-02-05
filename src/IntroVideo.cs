using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
The IntroVideo script runs the video on the Intro scene, which is the first scene to be opened when the
executable file is run. It requires an AudioSource component, since the video itself has audio.
*/
//[RequireComponent (typeof(AudioSource))]
public class IntroVideo : MonoBehaviour {

	//public MovieTexture movie;              //the movie texture which displays the video
	//private AudioSource audio;              //the audio component which is required as a complement to the video
	float timer;                            //the time recorded in the scene

	/*
    The Start function initializes the movie texture as a raw image and acquires the audio component within the video.
    The audio and video are played, and the time recorded in the scene is set to 0.
    */
	void Start () {
		//GetComponent<RawImage>().texture = movie as MovieTexture;
		//audio = GetComponent<AudioSource>();
		//audio.clip = movie.audioClip;
		//movie.Play();
		//audio.Play();
		timer = 0.0f;
	}

	/*
    The Update function counts the time recorded in the scene. If the time exceeds 29 seconds, the approximate length
    of the video, or if the player presses the left mouse button, the main menu screen is loaded.
    */
	void Update () {
		timer += Time.deltaTime;
		if(Input.GetAxis("Fire1")==1 || timer > 20)
		{
			SceneManager.LoadScene("StartScene");
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusicPause : MonoBehaviour {

	public GameObject music; //GameObject for which the AudioSource component is attached to
	public Text onOffText; //Text which shows whether the Audio is on or off

	// Use this for initialization
	void Start () {
		if (MusicManager.Instance.GetComponent<AudioSource> ().isPlaying)
		{
			onOffText.text = "Music: On";
		}
		else
		{
			onOffText.text = "Music: Off";
		}
	}

	public void OnOff()
	{
		if (MusicManager.Instance.GetComponent<AudioSource> ().isPlaying)
		{

			MusicManager.Instance.GetComponent<AudioSource> ().Pause ();
			onOffText.text = "Music: Off";
		}
		else
		{
			MusicManager.Instance.GetComponent<AudioSource> ().Play();
			onOffText.text = "Music: On";
		}
	}
}

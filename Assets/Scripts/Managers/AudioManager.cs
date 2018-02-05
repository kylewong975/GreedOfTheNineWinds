using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public void playSoundAtLocation(string sound, Vector3 location)
    {
        location = new Vector3(location.x, location.y, -2);
        switch (sound)
        {
            case "bang":
                AudioSource.PlayClipAtPoint(transform.Find("bang").GetComponent<AudioSource>().clip, location);
                break;
            case "pew":
                AudioSource.PlayClipAtPoint(transform.Find("pew").GetComponent<AudioSource>().clip, location);
                break;
        }
    }
}

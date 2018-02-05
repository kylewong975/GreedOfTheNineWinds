using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class Teleport : MonoBehaviour
{
	public bool goDown;

	void Start()
	{
	}

	void OnTriggerEnter2D(Collider2D col)
	{


		if (col.tag != "Player") {
			Destroy (col.gameObject);
		} else {
			if (col.transform.parent != null)
				return;
			
			//update deepest
			if (GameManager.ins.deepestAfflictionDifficulty < GameManager.ins.afflictionDifficulty) {
				GameManager.ins.deepestAfflictionDifficulty = GameManager.ins.afflictionDifficulty;
			}

			if (GameManager.ins.afflictionDifficulty != 0) {
				//write save
				string path = "Assets/Resources/Save/Scene" + GameManager.ins.afflictionDifficulty + ".txt";
				Debug.Log ("Writing! "+path);

				//create new file
				StreamWriter writer = new StreamWriter (path, false);
				Object[] allObjects = GameObject.FindObjectsOfType (typeof(GameObject));
				foreach (GameObject o in allObjects) {
					if (o.activeInHierarchy) {
						GameObject g = (GameObject)o;
						if (g.transform.parent == null &&
						    g.tag != "Player" &&
						    g.tag != "Untagged" &&
							g.tag != "DownPosition" &&
							g.tag != "UpPosition" &&
						    g.tag != "MainCamera") {
							writer.WriteLine (g.tag + " " + g.transform.position.x + " " + g.transform.position.y);
						}
					}
				}

				writer.Close ();

				//Re-import the file to update the reference in the editor
				AssetDatabase.ImportAsset (path); 
			}




			int nextLevel;
			if (goDown)
				nextLevel = GameManager.ins.afflictionDifficulty + 1;
			else 
				nextLevel = GameManager.ins.afflictionDifficulty - 1;

			//new scene
			
			LevelManager.ins.atTop = goDown;
			string sceneName = SceneManager.GetActiveScene ().name;
			if (goDown)
				nextLevel = sceneName [sceneName.Length - 1] - '0' + 1;
			else
				nextLevel = sceneName [sceneName.Length - 1] - '0' - 1;

			GameManager.ins.afflictionDifficulty = nextLevel;
			string nextScene = "TestLevel" + nextLevel;
			SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
		}
	}
}
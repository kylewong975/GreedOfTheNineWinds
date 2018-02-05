using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class TileScript : MonoBehaviour {
	public GameObject edge_block;
	public GameObject regular_block;
	public GameObject regular_enemy;
	public GameObject upExit;
	public GameObject downExit;
	public int world_width;
	public int world_height;

	void Awake (){
        GameManager.ins.level.coinsInLevel = new List<GameObject>();

		Instantiate(upExit, new Vector3 (-world_width/2+1, world_height/2, 0), Quaternion.identity);
		Instantiate(downExit, new Vector3 ( world_width/2-2, -world_height/2-1, 0), Quaternion.identity);

		if (GameManager.ins.afflictionDifficulty <= GameManager.ins.deepestAfflictionDifficulty) {

			//load save
			string path = "Save/Scene"+GameManager.ins.afflictionDifficulty;
			Debug.Log ("Searching path " + path);
			TextAsset data = (TextAsset) Resources.Load(path);
			string fs = data.text;
			string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );

			for ( int i=0; i < fLines.Length; i++ ) {
				string valueLine = fLines[i];
				if (valueLine == "" )
					continue;
				string[] values = Regex.Split ( valueLine, " " ); 
				GameObject gb = 
					(GameObject) (LootDatabase.database.ContainsKey(values[0]) ? Resources.Load("Prefabs/Loots/" + values[0]) : Resources.Load("Prefabs/" + values[0]));
				Instantiate(gb, new Vector3 (float.Parse(values[1]), float.Parse(values[2]), 0), Quaternion.identity);
			}
		} 
		else 
		{
			//randomly generate
			for (int y = -world_height/2; y < world_height/2; y++) {
				for (int x = -world_width/2; x < world_width/2; x++) {
					if (x <= -world_width/2 + 1 && y == world_height/2 - 1)
						continue;
					if (x >= +world_width/2 - 2 && y == -world_height/2)
						continue;
					if(Random.value > .75F) //bit of randomness
						Instantiate(regular_block, new Vector3 (x, y, 0), Quaternion.identity);
					else if (Random.value > .9F) //bit of randomness
						GameManager.ins.level.coinsInLevel.Add(Instantiate(LootDatabase.getRandomLoot().getPrefab(), new Vector3 (x, y, 0), Quaternion.identity));
					else if (Random.value > .95F) //bit of randomness
						Instantiate(regular_enemy, new Vector3 (x, y, 0), Quaternion.identity);
				}
			}
		}



		for (int y = -world_height/2; y < world_height/2; y++) {
			Instantiate(edge_block, new Vector3 (-world_width / 2 - 1, y, 0), Quaternion.identity);
			Instantiate(edge_block, new Vector3 (world_width / 2, y, 0), Quaternion.identity);
		}

		for (int x = -world_width/2+4; x < world_width/2+1; x++) {
            Instantiate(edge_block, new Vector3 (x, world_height / 2 , 0), Quaternion.identity);
		}

		for (int x = -world_width/2-1; x < world_width/2-4; x++) {
            Instantiate(edge_block, new Vector3 (x, -world_height / 2 - 1, 0), Quaternion.identity);
		}
	}

}


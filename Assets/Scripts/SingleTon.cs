using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon : MonoBehaviour {
	public static SingleTon s;

	void Awake () {
		if(!s) {
			s = this;
			DontDestroyOnLoad(gameObject);
		}else 
			Destroy(gameObject);
	}
}

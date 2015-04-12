using UnityEngine;

public class GameStart : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Invoke("LoadLevel", 3f);
	}
	
	void LoadLevel() {
		Application.LoadLevel("Main");
	}
}
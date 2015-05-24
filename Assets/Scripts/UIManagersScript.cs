using UnityEngine;
using System.Collections;

public class UIManagersScript : MonoBehaviour {

	public void StartGame () {
		Application.LoadLevel ("BeginStory");
	}

	public void RestartGame() {
		Application.LoadLevel ("stage_1");
	}
}
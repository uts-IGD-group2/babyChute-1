using UnityEngine;
using System.Collections;

public class UIManagersScript : MonoBehaviour {

	public void StartGame () {
		Application.LoadLevel ("StoryBegin");
	}

	public void RestartGame() {
		Application.LoadLevel ("MenuStart");
	}
}
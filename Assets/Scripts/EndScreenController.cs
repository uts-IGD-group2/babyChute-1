using UnityEngine;
using System.Collections;

public class EndScreenController : MonoBehaviour {

    public GameObject EndStory1;
    public GameObject EndStory2;
	public GameObject BabyObject;
	public GameObject RestartButton;
	
	void Start()
	{
		this.enabled = true;
        EndStory1.SetActive(false);
        EndStory2.SetActive(false);
		BabyObject.SetActive(false);
		RestartButton.SetActive(false);

		Invoke ("loadEndStory1", 0);
		Invoke ("loadEndStory2", 7);
	}

	void loadEndStory1() {
        EndStory1.SetActive(true);
		BabyObject.SetActive(true);
		iTween.MoveTo(BabyObject, new Vector3 (0, -8, 0), 50 );
	}
	
	void loadEndStory2() {
		EndStory2.SetActive(true);
        EndStory1.SetActive(false);
		BabyObject.SetActive(false);
		RestartButton.SetActive(true);
	}

	public void RestartGame() {
		Application.LoadLevel(0);
	}
}
using UnityEngine;
using System.Collections;

public class EndScreenController : MonoBehaviour {

    public GameObject EndStory1;
    public GameObject EndStory2;
    public GameObject babyObject;
	
	void Start()
	{
		this.enabled = true;
        EndStory1.SetActive(false);
        EndStory2.SetActive(false);
		babyObject.SetActive(false);

		Invoke ("loadEndStory1", 0);
		Invoke ("loadEndStory2", 8);
	}

	void loadEndStory1() {
        EndStory1.SetActive(true);
        babyObject.SetActive(true);
		iTween.MoveTo(babyObject, new Vector3 (0, -7, 0), 50 );
	}
	
	void loadEndStory2() {
        EndStory2.SetActive(true);
        EndStory1.SetActive(false);
		babyObject.SetActive(false);
	}

	public void Restart() {
		Application.LoadLevel(1);
	}
}
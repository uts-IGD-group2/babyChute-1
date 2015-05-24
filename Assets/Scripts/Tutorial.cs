using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {


	public GameObject Move;
	public GameObject Boost;
	public float moveTimer = 8.0f;
	public float boostTimer = 18.0f;
	//public float fadeTime = 1.0;
	public float fadeDelay = 0.0f; 
	public float fadeTime = 1f; 
	public bool fadeInOnStart = false; 
	public bool fadeOutOnStart = false;
	private bool logInitialFadeSequence = false; 
	private Color[] colors; 
	// Use this for initialization
	void Start () {
		FadeIn ();
		Move.GetComponent<Renderer>().enabled = true;
		Boost.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time > moveTimer) {
			Move.GetComponent<Renderer>().enabled = false;
			Boost.GetComponent<Renderer>().enabled = true;

		}
	



		if (Time.time > boostTimer) {

			FadeOut();
			Move.GetComponent<Renderer>().enabled = false;
		}
	}

	


	float MaxAlpha()
	{
		float maxAlpha = 0.0f; 
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		foreach (Renderer item in rendererObjects)
		{
			maxAlpha = Mathf.Max (maxAlpha, item.material.color.a); 
		}
		return maxAlpha; 
	}
	
	// fade sequence
	IEnumerator FadeSequence (float fadingOutTime)
	{
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 
		
		// grab all child objects
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		if (colors == null)
		{
			//create a cache of colors if necessary
			colors = new Color[rendererObjects.Length]; 
			
			// store the original colours for all child objects
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				colors[i] = rendererObjects[i].material.color; 
			}
		}
		
		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}
		
		
		// get current max alpha
		float alphaValue = MaxAlpha();  
		
		
		// This is a special case for objects that are set to fade in on start. 
		// it will treat them as alpha 0, despite them not being so. 
		if (logInitialFadeSequence && !fadingOut)
		{
			alphaValue = 0.0f; 
			logInitialFadeSequence = false; 
		}
		
		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 
			
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				rendererObjects[i].material.SetColor("_Color", newColor) ; 
			}
			
			yield return null; 
		}
		
		// turn objects off after fading out
		if (fadingOut)
		{
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				rendererObjects[i].enabled = false; 
			}
		}
		
		
		Debug.Log ("fade sequence end : " + fadingOut); 
		
	}
	
	
	void FadeIn ()
	{
		FadeIn (fadeTime); 
	}
	
	void FadeOut ()
	{
		FadeOut (fadeTime); 		
	}
	
	void FadeIn (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", newFadeTime); 
	}
	
	void FadeOut (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", -newFadeTime); 
	}
}
